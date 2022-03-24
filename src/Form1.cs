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
        private Result res;//hasil pencarian BFS
        private LinkLabel linklabel = new LinkLabel();//linklabel buat nampilin hasil pencarian
        private Label notFoundLabel = new Label();//label yang menunjukkan file yang dicari tidak ditemukan
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
            this.listPanel.Controls.Clear();
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
                this.linklabel.Text = "";
                if (listPath.Length > 0)
                {
                    int i = 0;
                    this.linklabel.AutoSize = true;
                    this.linklabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabel_LinkClicked);
                    this.linklabel.LinkArea = new LinkArea(2, listPath[0].Length + fileName.Length + 1);
                    foreach (string path in listPath)
                    {
                        string pathlink = (i + 1).ToString() + "." + path + '\\' + fileName + '\n';
                        linklabel.Text += pathlink;
                        if (i == 0)
                        {
                            this.linklabel.Links[0].LinkData = listPath[0];
                        }
                        else
                        {
                            this.linklabel.Links.Add(linklabel.Text.Length - path.Length - fileName.Length - 2, path.Length + fileName.Length + 1, path);
                        }
                        i++;
                    }
                    this.listPanel.Controls.Add(this.linklabel);
                }
                else
                {
                    this.notFoundLabel.Text = "Tidak ada path";
                    this.listPanel.Controls.Add(this.notFoundLabel);
                }
            }
            else if (DFSbutton.Checked)
            {
                stopwatch.Start();
                //panggil yg DFS
                this.res = DepthFirstSearch.DFS(this.startingDirectory, this.fileName, isSearchAllOccurence);
                this.graph = res.graph;
                stopwatch.Stop();
                long timeElapsed = stopwatch.ElapsedMilliseconds;
                Console.WriteLine(timeElapsed.ToString());
                string time = timeElapsed.ToString() + " ms";
                timeString.Text = time;
                graphOutput.Controls.Add(this.graph);
                string[] listPath = res.listOfPath;
                this.linklabel.Text = "";
                int i = 0;
                if (listPath.Length > 0)
                {
                    this.linklabel.AutoSize = true;
                    this.linklabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabel_LinkClicked);
                    this.linklabel.LinkArea = new LinkArea(2, listPath[0].Length + fileName.Length + 1);
                    foreach (string path in listPath)
                    {
                        string pathlink = (i + 1).ToString() + "." + path + '\\' + fileName + '\n';
                        linklabel.Text += pathlink;
                        if (i == 0)
                        {
                            this.linklabel.Links[0].LinkData = listPath[0];
                        }
                        else
                        {
                            this.linklabel.Links.Add(linklabel.Text.Length - path.Length - fileName.Length - 2, path.Length + fileName.Length + 1, path);
                        }
                        i++;
                    }
                }
                else
                {
                    this.notFoundLabel.Text = "Tidak ada path";
                    this.listPanel.Controls.Add(this.notFoundLabel);
                }
                this.listPanel.Controls.Add(this.linklabel);
                listPanel.AutoScroll = false;
                listPanel.HorizontalScroll.Enabled = false;
                listPanel.HorizontalScroll.Visible = false;
                listPanel.HorizontalScroll.Maximum = 0;
                listPanel.AutoScroll = true;
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

        private void graphOutput_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    public class Result
    {
        public string[] listOfPath { get; set; }
        public Microsoft.Msagl.GraphViewerGdi.GViewer graph { get; set; }
    }
}
