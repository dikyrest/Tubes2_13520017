namespace CariFile.com
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBrowse = new System.Windows.Forms.Button();
            this.title = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.outputPanel = new System.Windows.Forms.Panel();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.DFSbutton = new System.Windows.Forms.RadioButton();
            this.BFSbutton = new System.Windows.Forms.RadioButton();
            this.chooseMethodLabel = new System.Windows.Forms.Label();
            this.findAllOccurenceButton = new System.Windows.Forms.CheckBox();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.chooseFileLabel = new System.Windows.Forms.Label();
            this.selectedFolderLabel = new System.Windows.Forms.Label();
            this.chooseDirectoryLabel = new System.Windows.Forms.Label();
            this.inputLabel = new System.Windows.Forms.Label();
            this.startSearchButton = new System.Windows.Forms.Button();
            this.outputLabel = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            this.outputPanel.SuspendLayout();
            this.inputPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(3, 74);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(135, 32);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Pilih Folder...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(244, 13);
            this.title.Name = "title";
            this.title.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.title.Size = new System.Drawing.Size(231, 40);
            this.title.TabIndex = 1;
            this.title.Text = "CariFile.com";
            this.title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.outputPanel);
            this.mainPanel.Controls.Add(this.inputPanel);
            this.mainPanel.Location = new System.Drawing.Point(2, 64);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(669, 337);
            this.mainPanel.TabIndex = 2;
            // 
            // outputPanel
            // 
            this.outputPanel.Controls.Add(this.outputLabel);
            this.outputPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.outputPanel.Location = new System.Drawing.Point(328, 0);
            this.outputPanel.Name = "outputPanel";
            this.outputPanel.Size = new System.Drawing.Size(341, 337);
            this.outputPanel.TabIndex = 1;
            // 
            // inputPanel
            // 
            this.inputPanel.Controls.Add(this.startSearchButton);
            this.inputPanel.Controls.Add(this.DFSbutton);
            this.inputPanel.Controls.Add(this.BFSbutton);
            this.inputPanel.Controls.Add(this.chooseMethodLabel);
            this.inputPanel.Controls.Add(this.findAllOccurenceButton);
            this.inputPanel.Controls.Add(this.fileNameTextBox);
            this.inputPanel.Controls.Add(this.chooseFileLabel);
            this.inputPanel.Controls.Add(this.selectedFolderLabel);
            this.inputPanel.Controls.Add(this.btnBrowse);
            this.inputPanel.Controls.Add(this.chooseDirectoryLabel);
            this.inputPanel.Controls.Add(this.inputLabel);
            this.inputPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.inputPanel.Location = new System.Drawing.Point(0, 0);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(329, 337);
            this.inputPanel.TabIndex = 0;
            // 
            // DFSbutton
            // 
            this.DFSbutton.AutoSize = true;
            this.DFSbutton.Location = new System.Drawing.Point(12, 263);
            this.DFSbutton.Name = "DFSbutton";
            this.DFSbutton.Size = new System.Drawing.Size(67, 24);
            this.DFSbutton.TabIndex = 8;
            this.DFSbutton.TabStop = true;
            this.DFSbutton.Text = "DFS";
            this.DFSbutton.UseVisualStyleBackColor = true;
            // 
            // BFSbutton
            // 
            this.BFSbutton.AutoSize = true;
            this.BFSbutton.Location = new System.Drawing.Point(12, 233);
            this.BFSbutton.Name = "BFSbutton";
            this.BFSbutton.Size = new System.Drawing.Size(66, 24);
            this.BFSbutton.TabIndex = 7;
            this.BFSbutton.TabStop = true;
            this.BFSbutton.Text = "BFS";
            this.BFSbutton.UseVisualStyleBackColor = true;
            // 
            // chooseMethodLabel
            // 
            this.chooseMethodLabel.AutoSize = true;
            this.chooseMethodLabel.Location = new System.Drawing.Point(3, 198);
            this.chooseMethodLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.chooseMethodLabel.Name = "chooseMethodLabel";
            this.chooseMethodLabel.Padding = new System.Windows.Forms.Padding(10);
            this.chooseMethodLabel.Size = new System.Drawing.Size(203, 40);
            this.chooseMethodLabel.TabIndex = 6;
            this.chooseMethodLabel.Text = "Pilih Metode Pencaraian:";
            // 
            // findAllOccurenceButton
            // 
            this.findAllOccurenceButton.AutoSize = true;
            this.findAllOccurenceButton.Location = new System.Drawing.Point(7, 174);
            this.findAllOccurenceButton.Name = "findAllOccurenceButton";
            this.findAllOccurenceButton.Size = new System.Drawing.Size(248, 24);
            this.findAllOccurenceButton.TabIndex = 5;
            this.findAllOccurenceButton.Text = "Temukan Semua Kemunculan";
            this.findAllOccurenceButton.UseVisualStyleBackColor = true;
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileNameTextBox.Location = new System.Drawing.Point(7, 145);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(200, 23);
            this.fileNameTextBox.TabIndex = 4;
            // 
            // chooseFileLabel
            // 
            this.chooseFileLabel.AutoSize = true;
            this.chooseFileLabel.Location = new System.Drawing.Point(-1, 110);
            this.chooseFileLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.chooseFileLabel.Name = "chooseFileLabel";
            this.chooseFileLabel.Padding = new System.Windows.Forms.Padding(10);
            this.chooseFileLabel.Size = new System.Drawing.Size(302, 40);
            this.chooseFileLabel.TabIndex = 3;
            this.chooseFileLabel.Text = "Masukkan Nama File yang Ingin Dicari:";
            // 
            // selectedFolderLabel
            // 
            this.selectedFolderLabel.AutoSize = true;
            this.selectedFolderLabel.Location = new System.Drawing.Point(144, 80);
            this.selectedFolderLabel.Name = "selectedFolderLabel";
            this.selectedFolderLabel.Size = new System.Drawing.Size(129, 20);
            this.selectedFolderLabel.TabIndex = 2;
            this.selectedFolderLabel.Text = "Tidak Ada Folder";
            // 
            // chooseDirectoryLabel
            // 
            this.chooseDirectoryLabel.AutoSize = true;
            this.chooseDirectoryLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.chooseDirectoryLabel.Location = new System.Drawing.Point(0, 37);
            this.chooseDirectoryLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.chooseDirectoryLabel.Name = "chooseDirectoryLabel";
            this.chooseDirectoryLabel.Padding = new System.Windows.Forms.Padding(10);
            this.chooseDirectoryLabel.Size = new System.Drawing.Size(162, 40);
            this.chooseDirectoryLabel.TabIndex = 1;
            this.chooseDirectoryLabel.Text = "Pilih Direktori Awal:";
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.inputLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputLabel.Location = new System.Drawing.Point(0, 0);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(93, 37);
            this.inputLabel.TabIndex = 0;
            this.inputLabel.Text = "Input";
            // 
            // startSearchButton
            // 
            this.startSearchButton.Location = new System.Drawing.Point(81, 291);
            this.startSearchButton.Name = "startSearchButton";
            this.startSearchButton.Size = new System.Drawing.Size(148, 35);
            this.startSearchButton.TabIndex = 9;
            this.startSearchButton.Text = "MULAI CARI";
            this.startSearchButton.UseVisualStyleBackColor = true;
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.outputLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputLabel.Location = new System.Drawing.Point(0, 0);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(120, 37);
            this.outputLabel.TabIndex = 10;
            this.outputLabel.Text = "Output";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(683, 402);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.title);
            this.Name = "Form1";
            this.mainPanel.ResumeLayout(false);
            this.outputPanel.ResumeLayout(false);
            this.outputPanel.PerformLayout();
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel outputPanel;
        private System.Windows.Forms.Panel inputPanel;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.Label chooseDirectoryLabel;
        private System.Windows.Forms.Label selectedFolderLabel;
        private System.Windows.Forms.Label chooseFileLabel;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.CheckBox findAllOccurenceButton;
        private System.Windows.Forms.Label chooseMethodLabel;
        private System.Windows.Forms.RadioButton BFSbutton;
        private System.Windows.Forms.RadioButton DFSbutton;
        private System.Windows.Forms.Button startSearchButton;
        private System.Windows.Forms.Label outputLabel;
    }
}

