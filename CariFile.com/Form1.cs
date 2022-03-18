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
using Microsoft.Msagl;
using BFS;

namespace CariFile.com
{
    public partial class Form1 : Form
    {
        private String startingDirectory;//directory awal mulai pencarian
        private String fileName;//nama file yang ingin dicari
        private Boolean isSearchAllOccurence;//bernilai true jika ingin mencari semua kemunculan fileName
        private Microsoft.Msagl.GraphViewerGdi.GViewer graph;//graph yg dibuat
        //private 
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

        private void startSearchButton_Click(object sender, EventArgs e)
        {
            this.fileName = fileNameTextBox.Text;
            this.isSearchAllOccurence = findAllOccurenceButton.Checked;
            //graphImage.Image = Image.FromFile("test.png");
            if (BFSbutton.Checked)
            {
                //panggil yg BFS
                if (isSearchAllOccurence)
                {
                    //panggil yg cari semua occurence
                    this.graph = BFS.ViewerSample.BFS2(this.startingDirectory, this.fileName);
                }
                else
                {
                    //panggil yang cari 1 occurence saja
                    this.graph = BFS.ViewerSample.BFS(this.startingDirectory, this.fileName);
                    //graphImage.Controls.Add()
                }
                graphOutput.Controls.Add(this.graph);
            }
            else if (DFSbutton.Checked)
            {
                //panggil yg DFS
            }
        }
    }
    public partial class Tree
    {
        //attributes
        private Node node;
        //method
        public Tree(string name,SearchStatus status){
            this.node = new Node(name,status);
        }
    }
    public partial class Node
    {
        //attributes
        private string name;//name of the file/directory
        private SearchStatus status;//status node tersebut
        private List<Node> childNode;//node yang jadi child si node ini
        //method
        public Node(string name,SearchStatus status)
        {
            this.name = name;
            this.status = status;
            this.childNode = new List<Node>();
        }
        public void addChildNode(string name,SearchStatus status)
        {
            Node node = new Node(name, status);
            this.childNode.Add(node);
        }
        public string getName()
        {
            return this.name;
        }
        public SearchStatus getStatus()
        {
            return this.status;
        }
        public List<Node> getChildNode()
        {
            return this.childNode;
        }

    }
    public enum SearchStatus
    {
        FoundPath,//path ke arah file yang ditemukan
        NotFoundPath,//file/folder yang tidak ditemukan
        UnsearchedPath//file atau folder yang belum dicari
    }
}
