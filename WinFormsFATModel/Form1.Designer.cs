namespace WinFormsFATModel
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PlaceHolder_GridPanel = new Panel();
            menuStrip1 = new MenuStrip();
            row_numericUpDown = new NumericUpDown();
            col_numericUpDown = new NumericUpDown();
            row_label = new Label();
            col_label = new Label();
            main_label = new Label();
            ((System.ComponentModel.ISupportInitialize)row_numericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)col_numericUpDown).BeginInit();
            SuspendLayout();
            // 
            // PlaceHolder_GridPanel
            // 
            PlaceHolder_GridPanel.Location = new Point(12, 36);
            PlaceHolder_GridPanel.Name = "PlaceHolder_GridPanel";
            PlaceHolder_GridPanel.Size = new Size(525, 350);
            PlaceHolder_GridPanel.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(904, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // row_numericUpDown
            // 
            row_numericUpDown.Location = new Point(577, 76);
            row_numericUpDown.Name = "row_numericUpDown";
            row_numericUpDown.Size = new Size(120, 23);
            row_numericUpDown.TabIndex = 4;
            // 
            // col_numericUpDown
            // 
            col_numericUpDown.Location = new Point(758, 76);
            col_numericUpDown.Name = "col_numericUpDown";
            col_numericUpDown.Size = new Size(120, 23);
            col_numericUpDown.TabIndex = 5;
            // 
            // row_label
            // 
            row_label.AutoSize = true;
            row_label.Location = new Point(559, 58);
            row_label.Name = "row_label";
            row_label.Size = new Size(157, 15);
            row_label.TabIndex = 6;
            row_label.Text = "Кол-во элементов в строке";
            // 
            // col_label
            // 
            col_label.AutoSize = true;
            col_label.Location = new Point(739, 58);
            col_label.Name = "col_label";
            col_label.Size = new Size(165, 15);
            col_label.TabIndex = 7;
            col_label.Text = "Кол-во элементов в столбце";
            // 
            // main_label
            // 
            main_label.AutoSize = true;
            main_label.Font = new Font("Segoe UI", 16F);
            main_label.Location = new Point(668, 26);
            main_label.Name = "main_label";
            main_label.Size = new Size(122, 30);
            main_label.TabIndex = 8;
            main_label.Text = "Меню FAT ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(904, 450);
            Controls.Add(main_label);
            Controls.Add(col_label);
            Controls.Add(row_label);
            Controls.Add(col_numericUpDown);
            Controls.Add(row_numericUpDown);
            Controls.Add(PlaceHolder_GridPanel);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "FATModel";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)row_numericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)col_numericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel PlaceHolder_GridPanel;
        private MenuStrip menuStrip1;
        private NumericUpDown row_numericUpDown;
        private NumericUpDown col_numericUpDown;
        private Label row_label;
        private Label col_label;
        private Label main_label;
    }
}
