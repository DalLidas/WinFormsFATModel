﻿namespace WinFormsFATModel
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
            cToolStripMenuItem = new ToolStripMenuItem();
            справкаToolStripMenuItem = new ToolStripMenuItem();
            обАвтореToolStripMenuItem = new ToolStripMenuItem();
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
            FileSpace_label = new Label();
            Directory_label = new Label();
            FileSpaceSize_label = new Label();
            RemoveLostClusters_button = new Button();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)row_numericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)col_numericUpDown).BeginInit();
            SuspendLayout();
            // 
            // PlaceHolder_GridPanel
            // 
            PlaceHolder_GridPanel.Location = new Point(176, 58);
            PlaceHolder_GridPanel.Name = "PlaceHolder_GridPanel";
            PlaceHolder_GridPanel.Size = new Size(377, 469);
            PlaceHolder_GridPanel.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { cToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(904, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // cToolStripMenuItem
            // 
            cToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { справкаToolStripMenuItem, обАвтореToolStripMenuItem });
            cToolStripMenuItem.Name = "cToolStripMenuItem";
            cToolStripMenuItem.Size = new Size(53, 20);
            cToolStripMenuItem.Text = "Меню";
            // 
            // справкаToolStripMenuItem
            // 
            справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            справкаToolStripMenuItem.Size = new Size(180, 22);
            справкаToolStripMenuItem.Text = "Справка";
            справкаToolStripMenuItem.Click += справкаToolStripMenuItem_Click;
            // 
            // обАвтореToolStripMenuItem
            // 
            обАвтореToolStripMenuItem.Name = "обАвтореToolStripMenuItem";
            обАвтореToolStripMenuItem.Size = new Size(180, 22);
            обАвтореToolStripMenuItem.Text = "Об авторе";
            обАвтореToolStripMenuItem.Click += обАвтореToolStripMenuItem_Click;
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
            addFile_button.Location = new Point(599, 240);
            addFile_button.Name = "addFile_button";
            addFile_button.Size = new Size(268, 23);
            addFile_button.TabIndex = 9;
            addFile_button.Text = "Добавить файл";
            addFile_button.UseVisualStyleBackColor = true;
            addFile_button.Click += addFile_button_Click;
            // 
            // updateGridPanel_button
            // 
            updateGridPanel_button.Location = new Point(599, 139);
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
            directory_ListBox.Location = new Point(12, 58);
            directory_ListBox.Name = "directory_ListBox";
            directory_ListBox.Size = new Size(151, 469);
            directory_ListBox.TabIndex = 14;
            directory_ListBox.SelectedIndexChanged += directory_ListBox_SelectedIndexChanged_1;
            // 
            // deleteAll_button
            // 
            deleteAll_button.Location = new Point(599, 168);
            deleteAll_button.Name = "deleteAll_button";
            deleteAll_button.Size = new Size(268, 23);
            deleteAll_button.TabIndex = 15;
            deleteAll_button.Text = "Очистить всё";
            deleteAll_button.UseVisualStyleBackColor = true;
            deleteAll_button.Click += deleteAll_button_Click;
            // 
            // addCluster_button
            // 
            addCluster_button.Location = new Point(599, 269);
            addCluster_button.Name = "addCluster_button";
            addCluster_button.Size = new Size(268, 23);
            addCluster_button.TabIndex = 16;
            addCluster_button.Text = "Добавить кластер";
            addCluster_button.UseVisualStyleBackColor = true;
            addCluster_button.Click += addCluster_button_Click;
            // 
            // fragmentationAnalize_button
            // 
            fragmentationAnalize_button.Location = new Point(599, 371);
            fragmentationAnalize_button.Name = "fragmentationAnalize_button";
            fragmentationAnalize_button.Size = new Size(268, 23);
            fragmentationAnalize_button.TabIndex = 17;
            fragmentationAnalize_button.Text = "Анализ фрагментации";
            fragmentationAnalize_button.UseVisualStyleBackColor = true;
            fragmentationAnalize_button.Click += fragmentationAnalize_button_Click;
            // 
            // simpleDefragmentation_button
            // 
            simpleDefragmentation_button.Location = new Point(599, 475);
            simpleDefragmentation_button.Name = "simpleDefragmentation_button";
            simpleDefragmentation_button.Size = new Size(268, 23);
            simpleDefragmentation_button.TabIndex = 18;
            simpleDefragmentation_button.Text = "Простая дефрагментации";
            simpleDefragmentation_button.UseVisualStyleBackColor = true;
            simpleDefragmentation_button.Click += simpleDefragmentation_button_Click;
            // 
            // fullDefragmentation_button
            // 
            fullDefragmentation_button.Location = new Point(599, 504);
            fullDefragmentation_button.Name = "fullDefragmentation_button";
            fullDefragmentation_button.Size = new Size(268, 23);
            fullDefragmentation_button.TabIndex = 19;
            fullDefragmentation_button.Text = "Полная дефрагментации";
            fullDefragmentation_button.UseVisualStyleBackColor = true;
            fullDefragmentation_button.Click += fullDefragmentation_button_Click;
            // 
            // freeSpace_button
            // 
            freeSpace_button.Location = new Point(599, 342);
            freeSpace_button.Name = "freeSpace_button";
            freeSpace_button.Size = new Size(268, 23);
            freeSpace_button.TabIndex = 20;
            freeSpace_button.Text = "Анализ свободного места";
            freeSpace_button.UseVisualStyleBackColor = true;
            freeSpace_button.Click += freeSpace_button_Click;
            // 
            // FileSpace_label
            // 
            FileSpace_label.AutoSize = true;
            FileSpace_label.Font = new Font("Segoe UI", 10F);
            FileSpace_label.Location = new Point(297, 37);
            FileSpace_label.Name = "FileSpace_label";
            FileSpace_label.Size = new Size(161, 19);
            FileSpace_label.TabIndex = 21;
            FileSpace_label.Text = "Файловое пространство";
            // 
            // Directory_label
            // 
            Directory_label.AutoSize = true;
            Directory_label.Font = new Font("Segoe UI", 10F);
            Directory_label.Location = new Point(40, 37);
            Directory_label.Name = "Directory_label";
            Directory_label.Size = new Size(86, 19);
            Directory_label.TabIndex = 22;
            Directory_label.Text = "Дериктория";
            // 
            // FileSpaceSize_label
            // 
            FileSpaceSize_label.AutoSize = true;
            FileSpaceSize_label.Location = new Point(615, 111);
            FileSpaceSize_label.Name = "FileSpaceSize_label";
            FileSpaceSize_label.Size = new Size(195, 15);
            FileSpaceSize_label.TabIndex = 23;
            FileSpaceSize_label.Text = "Размер файлового пространства: ";
            // 
            // RemoveLostClusters_button
            // 
            RemoveLostClusters_button.Location = new Point(599, 446);
            RemoveLostClusters_button.Name = "RemoveLostClusters_button";
            RemoveLostClusters_button.Size = new Size(268, 23);
            RemoveLostClusters_button.TabIndex = 24;
            RemoveLostClusters_button.Text = "Удаление потеряных файлов";
            RemoveLostClusters_button.UseVisualStyleBackColor = true;
            RemoveLostClusters_button.Click += RemoveLostClusters_button_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(904, 561);
            Controls.Add(RemoveLostClusters_button);
            Controls.Add(FileSpaceSize_label);
            Controls.Add(Directory_label);
            Controls.Add(FileSpace_label);
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
            MaximumSize = new Size(920, 600);
            MinimumSize = new Size(920, 600);
            Name = "Form1";
            Text = "FATModel";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
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
        private Label FileSpace_label;
        private Label Directory_label;
        private Label FileSpaceSize_label;
        private Button RemoveLostClusters_button;
        private ToolStripMenuItem cToolStripMenuItem;
        private ToolStripMenuItem справкаToolStripMenuItem;
        private ToolStripMenuItem обАвтореToolStripMenuItem;
    }
}
