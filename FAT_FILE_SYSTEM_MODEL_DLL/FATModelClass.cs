using System;
using System.Collections.Generic;
using System.Linq;


namespace FAT_FILE_SYSTEM_MODEL_DLL
{
    /// <summary>
    /// Структура для описания файла в дериктории
    /// </summary>
    struct FileDеscriptor
    {
        public int ID = -1;
        public int StartClusterID = -1;
        public string Name = "";


        public FileDеscriptor(int ID, int startClusterID, string name = "")
        {
            this.ID = ID;
            this.StartClusterID = startClusterID;
            this.Name = name;
        }
    }


    /// <summary>
    /// Структура для описания кластера в файловом пространстве
    /// </summary>
    struct Cluster
    {
        public int ClusterID = -1;
        public int? NextClusterID = -1;
        public bool IsEOF = true;
        public bool IsBad = true;


        public Cluster(int ClusterID, int? NextClusterID, bool IsEOF = false, bool IsBad = false)
        {
            this.ClusterID = ClusterID;
            this.NextClusterID = NextClusterID;
            this.IsEOF = IsEOF;
            this.IsBad = IsBad;
        }
    }


    /// <summary>
    /// Модель, реализующая файловую систему FAT, а также методы анализа и взаимодействия с ней.
    /// </summary>
    internal class FATModelClass
    {
        //Размер файлового пространства
        private readonly int fileSpaceSize;

        //Колекция для файловых дискрипторов
        private List<FileDеscriptor> directory { get; }

        //Колекция для кластеров в файловом пространстве
        private Dictionary<int, Cluster> fileSpace { get; }


        //Последний результат по анализу дефрагментации файлового пространства
        private List<(string FileName, bool defragmentationNeededFlag, int MaxSequenceLength, int FragmentationCount, List<(int start, int end)> FragmentedBlocks)> lastDefragmentationAnalizeResult;


        //Параметры для функции рекомендаций
        private readonly int maxFragmentationCount = 3;
        private readonly double minSequenceRatio = 0.7;


        public FATModelClass(int fileSpaceSize, int maxFragmentationCount = 3, double minSequenceRatio = 0.7)
        {
            if (fileSpaceSize <= 0) throw new ArgumentException("Размер файлового пространства доджен быть больше 0.");

            this.fileSpaceSize = fileSpaceSize;
            this.directory = new List<FileDеscriptor>();
            this.fileSpace = new Dictionary<int, Cluster>();

            this.lastDefragmentationAnalizeResult = new List<(string FileName, bool defragmentationNeededFlag, int MaxSequenceLength, int FragmentationCount, List<(int start, int end)> FragmentedBlocks)>();

            //Параметры для функции рекомендаций
            this.maxFragmentationCount = 3;
            this.minSequenceRatio = 0.7;
        }


        #region CRUD functions

        #region Create

        /// <summary>
        /// Запись файла
        /// </summary>
        /// <param name="name"></param>
        /// <param name="clusters"></param>
        /// <returns></returns>
        public string CreateFile(string name, int[] clusters)
        {
            string errMSG = "";
            if (string.IsNullOrEmpty(name)) errMSG += "Название файла не задано или некорректно. \n";
            if (clusters.Length == 0) errMSG += "Не задано файловое пространства для записи файла. \n";

            if (directory.Any(f => f.Name == name)) errMSG += "Имя файла занято. \n";

            //Вывод сообщений об ошибках 
            if (errMSG.Length != 0) return errMSG;

            //Проверка на уже занятые кластеры
            foreach (var clusterID in clusters) errMSG += CheckClaster(clusterID);

            //Вывод сообщений об ошибках 
            if (errMSG.Length != 0) return errMSG;

            int startCluster = clusters[0];

            for (int i = 0; i < clusters.Length - 1; i++)
            {
                fileSpace[clusters[i]] = new Cluster(clusters[i], clusters[i + 1]);
            }

            //Последний кластер как EOF
            fileSpace[clusters[^1]] = new Cluster(clusters[^1], null, true);

            directory.Add(new FileDеscriptor(directory.Count + 1, startCluster, name));

            return errMSG;
        }

        /// <summary>
        /// Создать кластер  
        /// </summary>
        /// <param name="clusterID"></param>
        /// <param name="nextClusterID"></param>
        /// <param name="EOFFlag"></param>
        /// <param name="badFlag"></param>
        /// <returns></returns>
        public string CreateCluster(int clusterID, int nextClusterID, bool EOFFlag, bool badFlag, bool forceModeFlag = false)
        {
            if (!forceModeFlag)
            {
                // Проверка на границы обращения к файловому пространству, а также на существование кластера
                string errMSG = CheckClaster(clusterID);
                if (errMSG.Length != 0) return errMSG;
            }

            fileSpace[clusterID] = new Cluster(clusterID, nextClusterID, EOFFlag, badFlag);
            return "";
        }


        /// <summary>
        /// Создание конца файла
        /// </summary>
        /// <param name="clusterID"></param>
        /// <returns></returns>
        public string CreateEOFCluster(int clusterID, bool forceModeFlag = false)
        {
            if (!forceModeFlag)
            {
                // Проверка на границы обращения к файловому пространству, а также на существование кластера
                string errMSG = CheckClaster(clusterID);
                if (errMSG.Length != 0) return errMSG;
            }

            fileSpace[clusterID] = new Cluster(clusterID, null, true, false);
            return "";
        }


        /// <summary>
        /// Создание пустого кластера
        /// </summary>
        /// <param name="clusterID"></param>
        /// <returns></returns>
        public string CreateBabCluster(int clusterID, bool forceModeFlag = false)
        {
            if (!forceModeFlag)
            {
                // Проверка на границы обращения к файловому пространству, а также на существование кластера
                string errMSG = CheckClaster(clusterID);
                if (errMSG.Length != 0) return errMSG;
            }


            fileSpace[clusterID] = new Cluster(clusterID, null, false, true);
            return "";
        }

        #endregion Create

        #region Read


        #endregion Read

        #region Update

        /// <summary>
        /// Начинает записывать кластер по новому ID и обновляет ссылку на него в цепочке файла.
        /// </summary>
        /// <param name="cluster">Текущий кластер.</param>
        /// <param name="newClusterID">ID нового места кластера.</param>
        public void ReplocateClasterUpdateConnection(Cluster cluster, int newClusterID)
        {
            //Проверка на перезаписывание на одном месте
            if (cluster.ClusterID == newClusterID) return;

            // Переместить данные из занятого кластера в новый
            fileSpace[newClusterID] = new Cluster(newClusterID, cluster.NextClusterID, cluster.IsEOF, cluster.IsBad);

            // Проверить, если кластер является начальным для какого-либо файла
            for (int i = 0; i < directory.Count; i++)
            {
                if (directory[i].StartClusterID == cluster.ClusterID)
                {
                    // Обновляем ссылку на начало файла
                    directory[i] = new FileDеscriptor(directory[i].ID, newClusterID, directory[i].Name);
                    break;
                }
            }

            // Обновляем связь в предыдущем кластере
            for (int i = 0; i < fileSpaceSize; ++i)
            {
                if (fileSpace.ContainsKey(i) && fileSpace[i].NextClusterID == cluster.ClusterID)
                {
                    Cluster editedCluster = fileSpace[i];
                    fileSpace[i] = new Cluster(i, newClusterID, editedCluster.IsEOF, editedCluster.IsBad);
                    break;
                }
            }

            // Удаляем старый кластер
            fileSpace.Remove(cluster.ClusterID);
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string DeleteFile(string name)
        {
            var file = directory.Find(f => f.Name == name);
            if (file.ID == -1) return "Файл с таким именем не существует";

            int clusterID = file.StartClusterID;
            while (clusterID != -1)
            {
                int? nextCluster = fileSpace[clusterID].NextClusterID;
                fileSpace.Remove(clusterID);
                clusterID = nextCluster ?? -1;
            }

            directory.Remove(file);
            return "";
        }

        #endregion Delete

        #endregion CRUD functions


        #region Utilities

        /// <summary>
        /// Проверка на границы файлового пространства и на не занятость кластера (то что он свободен)
        /// </summary>
        /// <param name="clusterID"></param>
        /// <returns></returns>
        private string CheckClaster(int clusterID)
        {
            string errMSG = "";

            if (clusterID < 0) errMSG += "Кластер не может иметь отрицательный индекс. \n";
            if (clusterID >= fileSpaceSize) errMSG += $"Кластер {clusterID} выходит за пределы файлового пространства. \n";
            if (fileSpace.ContainsKey(clusterID)) errMSG += $"Данный кластер {clusterID} уже занят другим файлом. \n";

            return errMSG;
        }

        /// <summary>
        /// Возващает список кластеров связанных между собой (ссылающиеся на друг друга, цепочкой)
        /// </summary>
        /// <param name="startingClusterID"></param>
        /// <returns></returns>
        private (bool CorrectEOF, List<int> clasters) BuildClusterChain(int startingClusterID)
        {
            var chain = new List<int>();
            bool correctEOF = false;
            int? currentClusterID = startingClusterID;

            while (currentClusterID.HasValue && fileSpace.ContainsKey(currentClusterID.Value))
            {
                chain.Add(currentClusterID.Value);

                if (fileSpace[currentClusterID.Value].IsEOF)
                {
                    correctEOF = true;
                    break;
                }
                if (fileSpace[currentClusterID.Value].IsBad)
                {
                    correctEOF = false;
                    break;
                }

                currentClusterID = fileSpace[currentClusterID.Value].NextClusterID;
            }

            return (correctEOF, chain);
        }


        /// <summary>
        /// Проверяет, является ли цепочка кластеров цельной (последовательной).
        /// </summary>
        /// <param name="clusterChain">Цепочка кластеров.</param>
        /// <returns>True, если цепочка цельная, иначе False.</returns>
        private bool IsChainContiguous(List<int> clusterChain)
        {
            for (int i = 0; i < clusterChain.Count - 1; i++)
            {
                if (clusterChain[i] + 1 != clusterChain[i + 1])
                    return false;
            }
            return true;
        }


        /// <summary>
        /// Находит первый свободный кластер начиная с конца файлового пространства.
        /// </summary>
        /// <returns>Индекс первого свободного кластера или -1, если свободные кластеры отсутствуют.</returns>
        private int FindFirstFreeClusterFromEnd()
        {
            for (int i = fileSpaceSize - 1; i >= 0; i--)
            {
                if (!fileSpace.ContainsKey(i))
                {
                    return i;
                }
            }
            return -1;
        }

        #endregion Utilities


        #region Analize

        /// <summary>
        /// Возвращает данные о фрагментации файлового пространства для указанного файла.
        /// </summary>
        /// <param name="file">Файл, для которого выполняется анализ.</param>
        /// <returns>Данные о фрагментации, включающие длину последовательностей, количество разрывов и фрагментированные блоки.</returns>
        private (bool CorrectEOF, int MaxSequenceLength, int FragmentationCount, List<(int start, int end)> FragmentedBlocks) AnalyzeFileFragmentation(FileDеscriptor file)
        {
            var (correctEOF, clusterChain) = BuildClusterChain(file.StartClusterID);

            if (clusterChain.Count == 0)
            {
                return (false, 0, 0, new List<(int start, int end)>());
            }

            int maxSequenceLength = 0;
            int currentSequenceLength = 1;
            int fragmentationCount = 0;

            List<(int start, int end)> fragmentedBlocks = new List<(int start, int end)>();
            int fragmentStart = clusterChain[0];

            for (int i = 1; i < clusterChain.Count; i++)
            {
                if (clusterChain[i] == clusterChain[i - 1] + 1)
                {
                    currentSequenceLength++;
                }
                else
                {
                    // Разрыв в последовательности
                    fragmentedBlocks.Add((fragmentStart, clusterChain[i - 1]));
                    fragmentationCount++;
                    maxSequenceLength = Math.Max(maxSequenceLength, currentSequenceLength);

                    // Начало нового блока
                    fragmentStart = clusterChain[i];
                    currentSequenceLength = 1;
                }
            }

            // Завершающий блок
            fragmentedBlocks.Add((fragmentStart, clusterChain[^1]));
            maxSequenceLength = Math.Max(maxSequenceLength, currentSequenceLength);

            return (correctEOF, maxSequenceLength, fragmentationCount, fragmentedBlocks);
        }

        /// <summary>
        /// Анализ фрагментации диска
        /// </summary>
        public List<(string FileName, bool defragmentationNeededFlag, int MaxSequenceLength, int FragmentationCount, List<(int start, int end)> FragmentedBlocks)> AnalyzeFragmentation()
        {
            lastDefragmentationAnalizeResult.Clear();

            foreach (var file in directory)
            {
                var (correctEOF, maxSequenceLength, fragmentationCount, fragmentedBlocks) = AnalyzeFileFragmentation(file);

                if (correctEOF)
                {
                    bool defragmentationNeededFlag = IsFileNeedsDefragmentation((maxSequenceLength, fragmentationCount, fragmentedBlocks));
                    lastDefragmentationAnalizeResult.Add((file.Name, defragmentationNeededFlag, maxSequenceLength, fragmentationCount, fragmentedBlocks));
                }
            }

            return lastDefragmentationAnalizeResult;
        }

        /// <summary>
        /// Определяет, требуется ли дефрагментация файла.
        /// </summary>
        /// <param name="fragmentationData">Данные о фрагментации.</param>
        /// <returns>True, если дефрагментация необходима.</returns>
        private bool IsFileNeedsDefragmentation((int MaxSequenceLength, int FragmentationCount, List<(int start, int end)> FragmentedBlocks) fragmentationData)
        {
            var (maxSequenceLength, fragmentationCount, fragmentedBlocks) = fragmentationData;

            // Общая длина файла
            int totalClusters = fragmentedBlocks.Sum(block => block.end - block.start + 1);

            // Условия для дефрагментации (есть ограничение по количесву ошибок и )
            return fragmentationCount > maxFragmentationCount || maxSequenceLength < totalClusters * minSequenceRatio;
        }

        /// <summary>
        /// Обнаружение повреждённых кластеров
        /// </summary>
        public List<Cluster> AnalizeBadClasters()
        {
            return fileSpace.Values.Where(c => c.IsBad).ToList();
        }

        /// <summary>
        /// Вывод свободного места в файловой системе
        /// </summary>
        /// <returns></returns>
        public int GetFreeSpaceCount()
        {
            int usedClusters = fileSpace.Count;
            return fileSpaceSize - usedClusters;
        }

        /// <summary>
        /// Получение пустых кластеров
        /// </summary>
        /// <returns></returns>
        public List<Cluster> GetFreeSpaceClasters()
        {
            List<Cluster> emptyClasters = new List<Cluster>();

            for (int i = 0; i < fileSpaceSize; ++i)
            {
                if (!fileSpace.ContainsKey(i))
                {
                    emptyClasters.Add(fileSpace[i]);
                }
            }

            return emptyClasters;
        }

        #endregion Analize


        #region Action

        public void foo()
        {
            Console.WriteLine(FragmentFile(directory.Last(), 4));
        }


        /// <summary>
        /// Дефрагментирует все файлы в файловой системе, пытаясь создать цельные цепочки кластеров.
        /// </summary>
        public string FullDefragmentationFiles()
        {
            string msg = "Начало полной дефрагментации...\n";

            int currentCluster = 0; // Текущий кластер для начала записи
            int nextFileId; // ID следующего файла для дефрагментации

            List<int> needToDefragFilesIDs = new List<int>();
            foreach (var file in directory) needToDefragFilesIDs.Add(file.ID);

            // Пока есть файлы для дефрагментации
            while (needToDefragFilesIDs.Count != 0)
            {
                nextFileId = GetNextFileToDefragment(currentCluster, needToDefragFilesIDs);

                // Получаем файл по ID
                var file = directory.FirstOrDefault(f => f.ID == nextFileId);
                if (file.ID == -1)
                {
                    msg += $"Файл с ID {nextFileId} не найден.\n";
                    continue;
                }

                // Пытаемся переместить файл на текущую позицию
                string result = FragmentFile(file, currentCluster);
                if (string.IsNullOrEmpty(result))
                {
                    msg += $"Файл {file.Name} успешно дефрагментирован начиная с кластера {currentCluster}.\n";

                    var (EOF, clusterChain) = BuildClusterChain(file.StartClusterID);

                    // Если успешное перемещение, увеличиваем указатель
                    currentCluster += clusterChain.Count;

                }
                else
                {
                    msg += $"Не удалось дефрагментировать файл {file.Name}: {result}\n";
                }
                needToDefragFilesIDs.Remove(nextFileId);
            }

            msg += "Полная дефрагментация завершена.\n";

            return msg;
        }


        /// <summary>
        /// Выбирает следующий файл для дефрагментации, чтобы минимизировать количество перестановок.
        /// </summary>
        /// <returns>ID файла, который следует дефрагментировать следующим.</returns>
        public int GetNextFileToDefragment(int startClusterID, List<int> needToDefragFilesIDs)
        {
            int nextFileId = -1;
            int maxEfficiency = -1;

            foreach (var file in directory)
            {
                if (!needToDefragFilesIDs.Contains(file.ID)) continue;

                int fileId = file.ID;
                var (EOFFlag, clusterChain) = BuildClusterChain(file.StartClusterID);

                // Подсчет кластеров, находящихся на "правильных" местах
                int correctClusters = 0;
                for (int i = startClusterID; i < startClusterID + clusterChain.Count; i++)
                {
                    if (i == clusterChain[i - startClusterID])
                        correctClusters++;
                }

                // Поиск файла с максимальной эффективностью
                if (correctClusters > maxEfficiency)
                {
                    maxEfficiency = correctClusters;
                    nextFileId = fileId;
                }
            }

            return nextFileId;
        }


        /// <summary>
        /// Фрагментирует файл, записывая его начиная с заданного адреса.
        /// Если на указанном адресе уже есть данные, они перезаписываются в первую свободную область с конца.
        /// </summary>
        /// <param name="file">Файл, который нужно фрагментировать.</param>
        /// <param name="startAddress">Начальный адрес для записи.</param>
        public string FragmentFile(FileDеscriptor file, int startAddress)
        {
            var (EOFFlag, fileClusters) = BuildClusterChain(file.StartClusterID);
            if (!EOFFlag) return "Файл \"{file.Name}\" повреждён.\n"; ;
            if (fileClusters.Count == 0)
            {
                return $"Файл {file.Name} не содержит данных.\n";
            }

            if (startAddress < 0) return $"индекс стартого кластер {startAddress} не может быть отрицательным\n";
            if (startAddress + fileClusters.Count + 1 >= fileSpaceSize) return $"Файл {file.Name} не вмещается в файловое пространство.\n";

            int currentAddress = startAddress;
            for (int i = 0; i < fileClusters.Count; i++)
            {
                // Если текущий адрес уже занят, переместим данные в первую свободную область с конца
                if (currentAddress != fileClusters[i] && fileSpace.ContainsKey(currentAddress))
                {
                    int freeCluster = FindFirstFreeClusterFromEnd();
                    if (freeCluster == -1)
                    {
                        return "Недостаточно места для фрагментации файла.\n";
                    }

                    // Переместить данные из занятого кластера в свободный + Обновление цепочки
                    ReplocateClasterUpdateConnection(fileSpace[currentAddress], freeCluster);
                }

                // Записать текущий кластер файла на указанный адрес + Обновление цепочки
                ReplocateClasterUpdateConnection(fileSpace[fileClusters[i]], currentAddress);

                //Проверка на самоперезапись
                if (fileClusters.Contains(currentAddress))
                {
                    (EOFFlag, fileClusters) = BuildClusterChain(file.StartClusterID);
                }

                currentAddress += 1;
            }

            // Обновление начального кластеров для файла
            directory[directory.FindIndex(x => x.ID == file.ID)] = new FileDеscriptor(file.ID, startAddress, file.Name);

            return "";
        }


        /// <summary>
        /// Дефрагментирует все файлы в файловой системе, пытаясь создать цельные цепочки кластеров.
        /// </summary>
        public void SimpleDefragmentationFiles()
        {
            for (int i = 0; i < directory.Count; ++i)
            {
                SimpleDefragmentationFile(directory[i]);
            }
        }

        /// <summary>
        /// Простой алгоритм дефрагментации файловой системы FAT.
        /// </summary>
        public string SimpleDefragmentationFile(FileDеscriptor file)
        {
            // Поиск файла в директории
            if (file.ID == -1)
            {
                return $"Файл \"{file.Name}\" не найден.\n";
            }

            // Получение цепочки кластеров файла
            var (EOFFlag, clusterChain) = BuildClusterChain(file.StartClusterID);
            if (!EOFFlag) return "Файл \"{file.Name}\" повреждён.\n";
            if (clusterChain.Count == 0)
            {
                return $"Файл \"{file.Name}\" не имеет доступных кластеров.\n";
            }

            // Проверка, является ли файл уже непрерывным
            bool isContinuous = IsChainContiguous(clusterChain);

            if (isContinuous)
            {
                return $"Файл \"{file.Name}\" уже находится в непрерывной области.\n";
            }

            // Поиск подходящего места для записи файла
            int freeSpaceStart = -1;
            int freeSpaceLength = 0;
            for (int i = 0; i < fileSpaceSize; i++)
            {
                if (clusterChain.Contains(i) || !fileSpace.ContainsKey(i))
                {
                    if (freeSpaceLength == 0)
                        freeSpaceStart = i;

                    freeSpaceLength++;

                    if (freeSpaceLength >= clusterChain.Count)
                        break;
                }
                else
                {
                    freeSpaceStart = -1;
                    freeSpaceLength = 0;
                }
            }

            // Проверка, найдено ли место
            if (freeSpaceLength < clusterChain.Count)
            {
                return $"Недостаточно свободного места для дефрагментации файла \"{file.Name}\".\n";
            }

            // Перемещение файла в новое место
            int previousClusterID = -1;
            for (int i = 0; i < clusterChain.Count; i++)
            {
                int newClusterID = freeSpaceStart + i;

                // Копирование данных в новый кластер
                fileSpace[newClusterID] = new Cluster(newClusterID, i < clusterChain.Count - 1 ? newClusterID + 1 : (int?)null, i == clusterChain.Count - 1, false);

                // Удаление старого кластера
                if (newClusterID != clusterChain[i] && fileSpace.ContainsKey(clusterChain[i]))
                {
                    fileSpace.Remove(clusterChain[i]);
                }

                // Обновление цепочки
                if (previousClusterID != -1)
                {
                    Cluster editedCluster = fileSpace[previousClusterID];
                    fileSpace[previousClusterID] = new Cluster(editedCluster.ClusterID, newClusterID, editedCluster.IsEOF, editedCluster.IsBad);
                }

                previousClusterID = newClusterID;
            }

            // Обновление информации о файле в директории
            file.StartClusterID = freeSpaceStart;
            directory[directory.FindIndex(f => f.ID == file.ID)] = file;

            return $"Файл \"{file.Name}\" успешно дефрагментирован.\n";
        }

        #endregion Action


        #region FormOutput

        /// <summary>
        /// Выводит данные о фрагментации файлового пространства.
        /// </summary>
        /// <param name="fragmentationResults"></param>
        public string PrintFragmentationAnalyze(List<(string FileName, bool defragmentationNeededFlag, int MaxSequenceLength, int FragmentationCount, List<(int start, int end)> FragmentedBlocks)> fragmentationResults)
        {
            string msg = "";

            if (fragmentationResults == null || fragmentationResults.Count == 0)
            {
                return "Неподходящие данные данные\n";
            }

            foreach (var (fileName, defragmentationNeededFlag, maxSequenceLength, fragmentationCount, fragmentedBlocks) in fragmentationResults)
            {
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    msg += ("Не указано имя файла.\n");
                    continue;
                }

                msg += ($"Файл: {fileName}\n");
                msg += ($"  Максимальная длина последовательных кластеров: {maxSequenceLength}\n");
                msg += ($"  Количество разрывов: {fragmentationCount}\n");

                if (fragmentationCount > 0)
                {
                    if (fragmentedBlocks == null || fragmentedBlocks.Count == 0)
                    {
                        msg += ("  Информация о фрагментированных блоках отсутствует.\n");
                    }
                    else
                    {
                        msg += ("  Фрагментированные блоки:\n");
                        foreach (var block in fragmentedBlocks)
                        {
                            msg += ($"    От {block.start} до {block.end}, длиной {block.end - block.start + 1};\n");
                        }
                    }
                }
                else
                {
                    msg += ("  Файл не фрагментирован.\n");
                }
            }

            return msg;
        }

        /// <summary>
        /// Вывод информации о файловой системе
        /// </summary>
        public string PrintFileSystem()
        {
            string msg = "Файловая система:\n";

            foreach (var file in directory)
            {
                var (correctEOF, clusters) = BuildClusterChain(file.StartClusterID);

                if (correctEOF) msg += ($"Файл: {file.Name}, размещён в кластерах: {string.Join(", ", clusters)};\n");
                else msg += ($"Файл: {file.Name}, (Повреждён) размещён в кластерах: {string.Join(", ", clusters)};\n");
            }

            return msg;
        }

        #endregion FormOutput
    }
}
