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
//using BFS;
using System.Diagnostics;

namespace CariFile.com
{
    public partial class Form1 : Form
    {
        private String startingDirectory;//directory awal mulai pencarian
        private String fileName;//nama file yang ingin dicari
        private Boolean isSearchAllOccurence;//bernilai true jika ingin mencari semua kemunculan fileName
        private Microsoft.Msagl.GraphViewerGdi.GViewer graph;//graph yg dibuat
        private System.Diagnostics.Stopwatch stopwatch;//stopwatch untuk menghitung waktu menjalankan algoritma
        private ViewerSample.Result res;//hasil pencarian BFS
        private LinkLabel linklabel = new LinkLabel();//linklabel buat nampilin hasil pencarian
        //private 
        public Form1()
        {
            InitializeComponent();
            this.stopwatch = new Stopwatch();
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
            stopwatch.Start();
            this.fileName = fileNameTextBox.Text;
            this.isSearchAllOccurence = findAllOccurenceButton.Checked;
            timeString.Text = "0 ms";
            //reset graph
            graphOutput.Controls.Clear();
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds.ToString());
            if (BFSbutton.Checked)
            {
                //panggil yg BFS
                if (isSearchAllOccurence)
                {
                    //panggil yg cari semua occurence
                    stopwatch.Start();
                    this.res = ViewerSample.BFSAll(this.startingDirectory, this.fileName);
                    this.graph = res.graph;
                    stopwatch.Stop();
                }
                else
                {
                    stopwatch.Start();
                    //panggil yang cari 1 occurence saja
                    this.res = ViewerSample.BFSOne(this.startingDirectory, this.fileName);
                    this.graph = res.graph;
                    stopwatch.Stop();
                    //graphImage.Controls.Add()
                }
                long timeElapsed = stopwatch.ElapsedMilliseconds;
                Console.WriteLine(timeElapsed.ToString());
                string time = timeElapsed.ToString() + " ms";
                timeString.Text = time;
                graphOutput.Controls.Add(this.graph);
                string[] listPath = res.listOfPath;
                //string pathfile = "";
                this.linklabel.Text = "";
                int i = 0;
                this.linklabel.AutoSize = true;
                this.linklabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabel_LinkClicked);
                this.linklabel.LinkArea = new LinkArea(2, listPath[0].Length+1);
                foreach (string path in listPath)
                {
                    string pathlink = (i + 1).ToString() + "." + path + '\n';
                    linklabel.Text += pathlink;
                    if (i == 0)
                    {
                        this.linklabel.Links[0].LinkData = listPath[0];
                    }
                    else
                    {
                        this.linklabel.Links.Add(linklabel.Text.Length-path.Length-1, path.Length, path);
                    }
                    i++;
                }
                this.listPanel.Controls.Add(this.linklabel);
            }
            else if (DFSbutton.Checked)
            {
                stopwatch.Start();
                //panggil yg DFS
                this.graph = DepthFirstSearch.DFS(this.startingDirectory, this.fileName, isSearchAllOccurence);
                stopwatch.Stop();
                long timeElapsed = stopwatch.ElapsedMilliseconds;
                Console.WriteLine(timeElapsed.ToString());
                string time = timeElapsed.ToString() + " ms";
                timeString.Text = time;
                string[] listPath = res.listOfPath;
                graphOutput.Controls.Add(this.graph);
            }
        }

        private void linklabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            {
                this.linklabel.Links[linklabel.Links.IndexOf(e.Link)].Visited = true;
                string path = e.Link.LinkData as string;
                // Navigate to a URL.
                System.Diagnostics.Process.Start(path);
            }
        }
        public partial class Tree
        {
            //attributes
            private Node node;
            //method
            public Tree(string name, SearchStatus status)
            {
                this.node = new Node(name, status);
            }
        }
        public partial class Node
        {
            //attributes
            private string name;//name of the file/directory
            private SearchStatus status;//status node tersebut
            private List<Node> childNode;//node yang jadi child si node ini
                                         //method
            public Node(string name, SearchStatus status)
            {
                this.name = name;
                this.status = status;
                this.childNode = new List<Node>();
            }
            public void addChildNode(string name, SearchStatus status)
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
}
