using FAT_FILE_SYSTEM_MODEL_DLL;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinFormsFATModel
{


    public partial class Form1 : Form
    {
        int fileSpaceSize = 20;
        int maxFragmentationCount = 3;
        double minSequenceRatio = 0.7;

        int rowCount = 5;
        int colCount = 4;
        int minFileSpaceSize = 10;

        FATModelClass FAT;

        private GridPanel gridPanel;
        //private Panel PlaceHolder_GridPanel;


        public Form1()
        {
            InitializeComponent();

            this.FAT = new FATModelClass(fileSpaceSize, maxFragmentationCount, minSequenceRatio);

            // GridPanel
            gridPanel = new GridPanel();
            PlaceHolder_GridPanel.Controls.Add(gridPanel);

            // Поля для ввода количества строк
            row_numericUpDown.Minimum = 1;
            row_numericUpDown.Maximum = 100;
            row_numericUpDown.Value = rowCount;

            // Поля для ввода количества столбцов
            col_numericUpDown.Minimum = 1;
            col_numericUpDown.Maximum = 100;
            col_numericUpDown.Value = colCount;

            // Инициализация начальной сетки
            gridPanel.ResizeGridToFit(PlaceHolder_GridPanel.ClientSize, (int)row_numericUpDown.Value, (int)col_numericUpDown.Value);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void addFile_button_Click(object sender, EventArgs e)
        {
            using (var inputForm = new FileInputForm())
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    string name = inputForm.FileName;
                    int[] clusters = inputForm.Clusters;

                    // Пример вызова CreateFile
                    var result = FAT.CreateFile(name, clusters);
                    if (string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("Файл успешно создан.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(result, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            UpdateData();
        }


        private void updateGridPanel_button_Click(object sender, EventArgs e)
        {
            int buff_row = (int)row_numericUpDown.Value;
            int buf_col = (int)col_numericUpDown.Value;

            // Выход если система меньше оговорённой
            if (buff_row * buf_col < minFileSpaceSize)
            {
                MessageBox.Show("Ошибка: минимальный размер файловой системы задан " + minFileSpaceSize.ToString());
                return;
            }

            // Если размер файловой системы уменьшается, показываем предупреждение
            if (rowCount * colCount > buff_row * buf_col)
            {
                DialogResult result = MessageBox.Show(
                    "Внимание! Размер файловой системы будет уменьшен. Все данные, которые не влезут в новый размер, будут удалены. Вы уверены, что хотите продолжить?",
                    "Предупреждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return; // Прекращаем выполнение, если пользователь нажал "Нет"
                }
            }

            // Обновляем количество строк и столбцов
            rowCount = buff_row;
            colCount = buf_col;

            // Изменяем размер файловой системы
            FAT.Resize(rowCount * colCount);

            // Перерисовываем панель
            gridPanel.ResizeGridToFit(PlaceHolder_GridPanel.ClientSize, rowCount, colCount);

            // Обновляем данные (например, обновляем отображение)
            UpdateData();
        }


        private void UpdateData()
        {
            UpdateGridPanel();
            UpdateDirectoryListBox();
        }


        private void UpdateGridPanel()
        {
            var clusters = FAT.GetFileSpace();

            // Обновляем все клетки сетки
            foreach (var cluster in clusters.Values)
            {
                // Выбираем цвет для клетки
                Color cellColor = cluster.IsBad ? Color.Red :
                                  cluster.IsEOF ? Color.Green :
                                  cluster.NextClusterID.HasValue ? Color.Blue : Color.AliceBlue;

                // Символ для клетки
                string symbol = $"{cluster.ClusterID}:";
                if (cluster.IsEOF) symbol += "EOF";
                else if (cluster.IsBad) symbol += "Bad";
                else if (cluster.NextClusterID.HasValue) symbol += cluster.NextClusterID.ToString() ?? "null";
                else symbol += "Err";

                // Правильный расчет строк и столбцов
                int row = cluster.ClusterID / colCount;  // строка (делим на количество столбцов)
                int col = cluster.ClusterID % colCount;  // столбец (остаток от деления)

                // Убедимся, что индексы не выходят за пределы допустимых значений
                if (row < rowCount && col < colCount)
                {
                    gridPanel.SetCellColor(row, col, cellColor); // Устанавливаем цвет клетки
                    gridPanel.SetCellSymbol(row, col, symbol);    // Устанавливаем символ в клетку
                }
            }
        }


        private void UpdateDirectoryListBox()
        {
            // Очищаем текущие элементы ListBox
            directory_ListBox.Items.Clear();

            // Добавляем элементы из коллекции directory
            foreach (var file in FAT.GetDirectory())
            {
                // Формируем строку с данными и добавляем в ListBox
                string fileInfo = $"Имя: {file.Name}, ID кластера: {file.StartClusterID}";
                directory_ListBox.Items.Add(fileInfo);
            }
        }


        private void directory_ListBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Регулярное выражение для извлечения StartClusterID
            const string pattern = @"ID кластера: (\d+)";

            if (directory_ListBox.SelectedIndex != -1)
            {
                // Получаем строку из ListBox по индексу
                string selectedFileString = directory_ListBox.SelectedItem?.ToString() ?? "";

                // Применяем регулярное выражение
                var match = Regex.Match(selectedFileString, pattern);

                if (match.Success)
                {
                    // Извлекаем ID первого кластера
                    int startClusterID = int.Parse(match.Groups[1].Value);

                    // Очистить предыдущие выделения
                    gridPanel.ClearSelection();

                    // Подсветить кластеры, связанные с файлом (предполагаем, что кластеры идут подряд)
                    var (EOFFlag, clusterChain) = FAT.BuildClusterChain(startClusterID);

                    foreach (int clusterID in clusterChain)
                    {
                        // Рассчитываем позицию ячейки
                        int row = clusterID / colCount;
                        int col = clusterID % colCount;

                        // Подсвечиваем клетку
                        gridPanel.SelectCell(row, col);
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось найти StartClusterID.");
                }
            }
            else
            {
                // Очистить предыдущие выделения
                gridPanel.ClearSelection();
            }
        }

        private void deleteAll_button_Click(object sender, EventArgs e)
        {
            FAT.DeleteAll();
        }

        private void addCluster_button_Click(object sender, EventArgs e)
        {
            // Открываем форму ввода данных для кластера
            var inputForm = new ClusterInputForm();
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                // Получаем введенные данные
                int clusterID = inputForm.ClusterID;
                int nextClusterID = inputForm.NextClusterID;
                bool EOFFlag = inputForm.EOFFlag;
                bool badFlag = inputForm.BadFlag;
                bool forceModeFlag = inputForm.ForceModeFlag;

                // Вызов функции для создания кластера
                string result =  FAT.CreateCluster(clusterID, nextClusterID, EOFFlag, badFlag, forceModeFlag);
                if (!string.IsNullOrEmpty(result))
                {
                    MessageBox.Show(result);
                }
                else
                {
                    MessageBox.Show("Кластер успешно создан!");
                }
            }

            UpdateData();
        }
    }


    public class GridPanel : Control
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public int CellSize { get; private set; }

        private Dictionary<(int, int), Cell> cells = new Dictionary<(int, int), Cell>();
        private HashSet<(int, int)> selectedCells = new HashSet<(int, int)>(); // Для хранения выбранных ячеек

        public GridPanel()
        {
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Рисуем сетку
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    var cell = GetCell(row, col);

                    // Цвет ячейки
                    Color cellColor = cell.Color;
                    if (selectedCells.Contains((row, col)))
                    {
                        cellColor = Color.Yellow; // Подсветка выбранных 
                    }

                    using (Brush brush = new SolidBrush(cellColor))
                    {
                        g.FillRectangle(brush, col * CellSize, row * CellSize, CellSize, CellSize);
                    }

                    // Граница
                    g.DrawRectangle(Pens.Black, col * CellSize, row * CellSize, CellSize, CellSize);

                    // Текст
                    if (!string.IsNullOrEmpty(cell.Symbol))
                    {
                        SizeF textSize = g.MeasureString(cell.Symbol, this.Font);
                        float textX = col * CellSize + (CellSize - textSize.Width) / 2;
                        float textY = row * CellSize + (CellSize - textSize.Height) / 2;
                        g.DrawString(cell.Symbol, this.Font, Brushes.Black, textX, textY);
                    }
                }
            }
        }

        public void InitializeGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            cells.Clear(); // Очищаем старые ячейки

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    cells[(row, col)] = new Cell();
                }
            }

            Invalidate(); // Перерисовка
        }

        public void ResizeGridToFit(Size panelSize, int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            // Рассчитываем размер ячейки, чтобы сетка вписалась в размеры панели
            int maxCellWidth = panelSize.Width / Columns;
            int maxCellHeight = panelSize.Height / Rows;
            CellSize = Math.Min(maxCellWidth, maxCellHeight);

            this.Size = new Size(Columns * CellSize, Rows * CellSize); // Размер самого `GridPanel`

            InitializeGrid(Rows, Columns);
        }

        public void SetCellColor(int row, int col, Color color)
        {
            var cell = GetCell(row, col);
            cell.Color = color;
            Invalidate();
        }

        public void SetCellSymbol(int row, int col, string symbol)
        {
            var cell = GetCell(row, col);
            cell.Symbol = symbol;
            Invalidate();
        }

        // Метод для выделения ячеек
        public void SelectCell(int row, int col)
        {
            selectedCells.Add((row, col));
            Invalidate(); // Перерисовываем панель
        }

        // Метод для отмены выделения ячеек
        public void DeselectCell(int row, int col)
        {
            selectedCells.Remove((row, col));
            Invalidate(); // Перерисовываем панель
        }

        // Метод для очистки всех выделений
        public void ClearSelection()
        {
            selectedCells.Clear();
            Invalidate(); // Перерисовываем панель
        }

        private Cell GetCell(int row, int col)
        {
            if (!cells.TryGetValue((row, col), out var cell))
            {
                cell = new Cell();
                cells[(row, col)] = cell;
            }
            return cell;
        }

        private class Cell
        {
            public Color Color { get; set; } = Color.White;
            public string Symbol { get; set; } = string.Empty;
        }
    }

    public class FileInputForm : Form
    {
        private TextBox fileNameTextBox;
        private TextBox clustersTextBox;
        private Button okButton;
        private Button cancelButton;

        public string FileName { get; private set; }
        public int[] Clusters { get; private set; }


        public FileInputForm()
        {
            this.Text = "Ввод файла";
            this.ClientSize = new Size(400, 180);
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Запрет изменения размеров
            this.MaximizeBox = false; // Запрет на кнопку максимизации

            // Поле ввода имени файла
            var fileNameLabel = new Label
            {
                Text = "Имя файла:",
                Location = new Point(20, 20),
                AutoSize = true
            };

            fileNameTextBox = new TextBox
            {
                Location = new Point(150, 18),
                Width = 220
            };

            this.Controls.Add(fileNameLabel);
            this.Controls.Add(fileNameTextBox);

            // Поле ввода кластеров
            var clustersLabel = new Label
            {
                Text = "ID кластеров\n (через пробел):",
                Location = new Point(20, 60),
                AutoSize = true
            };

            clustersTextBox = new TextBox
            {
                Location = new Point(150, 58),
                Width = 220
            };

            this.Controls.Add(clustersLabel);
            this.Controls.Add(clustersTextBox);

            // Кнопка "OK"
            okButton = new Button
            {
                Text = "OK",
                Location = new Point(90, 120),
                Size = new Size(90, 30),
                DialogResult = DialogResult.OK
            };

            okButton.Click += OkButton_Click;

            // Кнопка "Cancel"
            cancelButton = new Button
            {
                Text = "Cancel",
                Location = new Point(200, 120),
                Size = new Size(90, 30),
                DialogResult = DialogResult.Cancel
            };

            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            FileName = fileNameTextBox.Text.Trim();
            Clusters = clustersTextBox.Text
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }
    }

    public class ClusterInputForm : Form
    {
        private NumericUpDown clusterIDNumericUpDown;
        private NumericUpDown nextClusterIDNumericUpDown;
        private CheckBox EOFFlagCheckBox;
        private CheckBox badFlagCheckBox;
        private CheckBox forceModeFlagCheckBox;
        private Button okButton;
        private Button cancelButton;

        public int ClusterID { get; private set; }
        public int NextClusterID { get; private set; }
        public bool EOFFlag { get; private set; }
        public bool BadFlag { get; private set; }
        public bool ForceModeFlag { get; private set; }

        public ClusterInputForm()
        {
            this.Text = "Ввод данных для кластера";
            this.ClientSize = new Size(300, 200);

            // Поле ввода ID кластера
            var clusterIDLabel = new Label
            {
                Text = "ID кластера:",
                Location = new Point(20, 20),
                AutoSize = true
            };
            clusterIDNumericUpDown = new NumericUpDown
            {
                Location = new Point(120, 20),
                Width = 150,
                Minimum = 0
            };

            // Поле ввода ID следующего кластера
            var nextClusterIDLabel = new Label
            {
                Text = "ID следующего\n кластера:",
                Location = new Point(20, 60),
                AutoSize = true
            };
            nextClusterIDNumericUpDown = new NumericUpDown
            {
                Location = new Point(120, 60),
                Width = 150,
                Minimum = 0
            };

            // Чекбокс для флага EOF
            EOFFlagCheckBox = new CheckBox
            {
                Text = "EOF флаг",
                Location = new Point(20, 100),
                AutoSize = true
            };

            // Чекбокс для флага плохого кластера
            badFlagCheckBox = new CheckBox
            {
                Text = "Плохой кластер",
                Location = new Point(120, 100),
                AutoSize = true
            };

            // Чекбокс для флага forceMode
            forceModeFlagCheckBox = new CheckBox
            {
                Text = "Режим принудительного создания",
                Location = new Point(20, 140),
                AutoSize = true
            };

            // Кнопка OK
            okButton = new Button
            {
                Text = "OK",
                Location = new Point(50, 170),
                DialogResult = DialogResult.OK
            };
            okButton.Click += OkButton_Click;

            // Кнопка Cancel
            cancelButton = new Button
            {
                Text = "Cancel",
                Location = new Point(150, 170),
                DialogResult = DialogResult.Cancel
            };

            // Добавляем элементы на форму
            this.Controls.Add(clusterIDLabel);
            this.Controls.Add(clusterIDNumericUpDown);
            this.Controls.Add(nextClusterIDLabel);
            this.Controls.Add(nextClusterIDNumericUpDown);
            this.Controls.Add(EOFFlagCheckBox);
            this.Controls.Add(badFlagCheckBox);
            this.Controls.Add(forceModeFlagCheckBox);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            // Собираем данные из полей ввода
            ClusterID = (int)clusterIDNumericUpDown.Value;
            NextClusterID = (int)nextClusterIDNumericUpDown.Value;
            EOFFlag = EOFFlagCheckBox.Checked;
            BadFlag = badFlagCheckBox.Checked;
            ForceModeFlag = forceModeFlagCheckBox.Checked;
        }
    }
}
