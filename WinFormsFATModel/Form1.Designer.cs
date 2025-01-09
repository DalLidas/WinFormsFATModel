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
            addFile_button = new Button();
            updateGridPanel_button = new Button();
            directory_ListBox = new ListBox();
            deleteAll_button = new Button();
            addCluster_button = new Button();
            fragmentationAnalize_button = new Button();
            simpleDefragmentation_button = new Button();
            fullDefragmentation_button = new Button();
            freeSpace_button = new Button();
            ((System.ComponentModel.ISupportInitialize)row_numericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)col_numericUpDown).BeginInit();
            SuspendLayout();
            // 
            // PlaceHolder_GridPanel
            // 
            PlaceHolder_GridPanel.Location = new Point(176, 36);
            PlaceHolder_GridPanel.Name = "PlaceHolder_GridPanel";
            PlaceHolder_GridPanel.Size = new Size(377, 391);
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
            // addFile_button
            // 
            addFile_button.Location = new Point(599, 219);
            addFile_button.Name = "addFile_button";
            addFile_button.Size = new Size(268, 23);
            addFile_button.TabIndex = 9;
            addFile_button.Text = "Добавить файл";
            addFile_button.UseVisualStyleBackColor = true;
            addFile_button.Click += addFile_button_Click;
            // 
            // updateGridPanel_button
            // 
            updateGridPanel_button.Location = new Point(599, 116);
            updateGridPanel_button.Name = "updateGridPanel_button";
            updateGridPanel_button.Size = new Size(268, 23);
            updateGridPanel_button.TabIndex = 10;
            updateGridPanel_button.Text = "Обновить таблицу";
            updateGridPanel_button.UseVisualStyleBackColor = true;
            updateGridPanel_button.Click += updateGridPanel_button_Click;
            // 
            // directory_ListBox
            // 
            directory_ListBox.FormattingEnabled = true;
            directory_ListBox.ItemHeight = 15;
            directory_ListBox.Location = new Point(12, 36);
            directory_ListBox.Name = "directory_ListBox";
            directory_ListBox.Size = new Size(151, 394);
            directory_ListBox.TabIndex = 14;
            directory_ListBox.SelectedIndexChanged += directory_ListBox_SelectedIndexChanged_1;
            // 
            // deleteAll_button
            // 
            deleteAll_button.Location = new Point(599, 145);
            deleteAll_button.Name = "deleteAll_button";
            deleteAll_button.Size = new Size(268, 23);
            deleteAll_button.TabIndex = 15;
            deleteAll_button.Text = "Очистить всё";
            deleteAll_button.UseVisualStyleBackColor = true;
            deleteAll_button.Click += deleteAll_button_Click;
            // 
            // addCluster_button
            // 
            addCluster_button.Location = new Point(599, 248);
            addCluster_button.Name = "addCluster_button";
            addCluster_button.Size = new Size(268, 23);
            addCluster_button.TabIndex = 16;
            addCluster_button.Text = "Добавить кластер";
            addCluster_button.UseVisualStyleBackColor = true;
            addCluster_button.Click += addCluster_button_Click;
            // 
            // fragmentationAnalize_button
            // 
            fragmentationAnalize_button.Location = new Point(599, 347);
            fragmentationAnalize_button.Name = "fragmentationAnalize_button";
            fragmentationAnalize_button.Size = new Size(268, 23);
            fragmentationAnalize_button.TabIndex = 17;
            fragmentationAnalize_button.Text = "Анализ фрагментации";
            fragmentationAnalize_button.UseVisualStyleBackColor = true;
            fragmentationAnalize_button.Click += fragmentationAnalize_button_Click;
            // 
            // simpleDefragmentation_button
            // 
            simpleDefragmentation_button.Location = new Point(599, 376);
            simpleDefragmentation_button.Name = "simpleDefragmentation_button";
            simpleDefragmentation_button.Size = new Size(268, 23);
            simpleDefragmentation_button.TabIndex = 18;
            simpleDefragmentation_button.Text = "Простая дефрагментации";
            simpleDefragmentation_button.UseVisualStyleBackColor = true;
            simpleDefragmentation_button.Click += simpleDefragmentation_button_Click;
            // 
            // fullDefragmentation_button
            // 
            fullDefragmentation_button.Location = new Point(599, 405);
            fullDefragmentation_button.Name = "fullDefragmentation_button";
            fullDefragmentation_button.Size = new Size(268, 23);
            fullDefragmentation_button.TabIndex = 19;
            fullDefragmentation_button.Text = "Полная дефрагментации";
            fullDefragmentation_button.UseVisualStyleBackColor = true;
            fullDefragmentation_button.Click += fullDefragmentation_button_Click;
            // 
            // freeSpace_button
            // 
            freeSpace_button.Location = new Point(599, 318);
            freeSpace_button.Name = "freeSpace_button";
            freeSpace_button.Size = new Size(268, 23);
            freeSpace_button.TabIndex = 20;
            freeSpace_button.Text = "Анализ свободного места";
            freeSpace_button.UseVisualStyleBackColor = true;
            freeSpace_button.Click += freeSpace_button_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(904, 461);
            Controls.Add(freeSpace_button);
            Controls.Add(fullDefragmentation_button);
            Controls.Add(simpleDefragmentation_button);
            Controls.Add(fragmentationAnalize_button);
            Controls.Add(addCluster_button);
            Controls.Add(deleteAll_button);
            Controls.Add(directory_ListBox);
            Controls.Add(updateGridPanel_button);
            Controls.Add(addFile_button);
            Controls.Add(main_label);
            Controls.Add(col_label);
            Controls.Add(row_label);
            Controls.Add(col_numericUpDown);
            Controls.Add(row_numericUpDown);
            Controls.Add(PlaceHolder_GridPanel);
            Controls.Add(menuStrip1);
            ForeColor = Color.Black;
            MainMenuStrip = menuStrip1;
            MaximumSize = new Size(920, 500);
            MinimumSize = new Size(920, 500);
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
        private Button addFile_button;
        private Button updateGridPanel_button;
        private ListBox directory_ListBox;
        private Button deleteAll_button;
        private Button addCluster_button;
        private Button fragmentationAnalize_button;
        private Button simpleDefragmentation_button;
        private Button fullDefragmentation_button;
        private Button freeSpace_button;
    }
}
