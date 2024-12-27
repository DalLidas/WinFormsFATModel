using System.Windows.Forms;

namespace WinFormsFATModel
{


    public partial class Form1 : Form
    {

        private GridPanel gridPanel;
        //private Panel PlaceHolder_GridPanel;
        private NumericUpDown rowsInput;
        private NumericUpDown columnsInput;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Dynamic Grid in Panel";
            this.ClientSize = new Size(800, 600);

            this.Controls.Add(PlaceHolder_GridPanel);

            // GridPanel
            gridPanel = new GridPanel();
            PlaceHolder_GridPanel.Controls.Add(gridPanel);

            // Поля для ввода количества строк и столбцов
            rowsInput = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 50,
                Value = 10,
                Location = new Point(650, 50),
                Width = 100
            };

            this.Controls.Add(rowsInput);

            columnsInput = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 50,
                Value = 10,
                Location = new Point(650, 100),
                Width = 100
            };

            this.Controls.Add(columnsInput);

            // Кнопка для обновления сетки
            Button updateGridButton = new Button
            {
                Text = "Update Grid",
                Location = new Point(650, 150),
                Size = new Size(100, 30)
            };

            updateGridButton.Click += (s, e) =>
            {
                int rows = (int)rowsInput.Value;
                int columns = (int)columnsInput.Value;

                gridPanel.ResizeGridToFit(PlaceHolder_GridPanel.ClientSize, rows, columns);
            };

            this.Controls.Add(updateGridButton);

            // Инициализация начальной сетки
            gridPanel.ResizeGridToFit(PlaceHolder_GridPanel.ClientSize, (int)rowsInput.Value, (int)columnsInput.Value);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class GridPanel : Control
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public int CellSize { get; private set; }

        private Dictionary<(int, int), Cell> cells = new Dictionary<(int, int), Cell>();

        public GridPanel()
        {
            this.DoubleBuffered = true; // Устранение мерцания
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

                    // Заливка
                    using (Brush brush = new SolidBrush(cell.Color))
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
}
