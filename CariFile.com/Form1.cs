using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace CariFile.com
{
    public partial class Form1 : Form
    {
        private String startingDirectory;//directory awal mulai pencarian
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                selectedFolderLabel.Text = dialog.FileName;
                this.startingDirectory = dialog.FileName;
            }
        }

        private void BFSbutton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void findAllOccurenceButton_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
