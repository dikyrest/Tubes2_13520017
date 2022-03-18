using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace BFS
{
    class ViewerSample
    {
        public static int cekSelesai(int[] temp)
        {
            int hasil = 1;
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == 0)
                {
                    hasil = 0;
                }
            }

            return hasil;
        }

        public static string[] cariAll2(string[] antrian, string InputFile)
        {
            if (antrian.Length == 0)
            {
                string[] kosong = { };
                return kosong;
            }

            else
            {
                IList<string> searchFolderTemp = new List<string>();
                try
                {
                    Directory.GetDirectories(antrian[0] + "\\")
                    .ToList()
                    .ForEach(s => searchFolderTemp.Add(s));
                }

                catch (UnauthorizedAccessException)
                {
                    //
                }

                IList<string> searchFileTemp = new List<string>();
                try
                {
                    Directory.GetFiles(antrian[0] + "\\")
                    .ToList()
                    .ForEach(s => searchFileTemp.Add(s));
                }

                catch (UnauthorizedAccessException)
                {
                    //
                }

                string[] searchAll = { };
                string[] searchFolder = searchFolderTemp.ToArray();
                string[] searchFile = searchFileTemp.ToArray();
                int[] isChecked = new int[searchFolder.Length + searchFile.Length];
                for (int i = 0; i < isChecked.Length; i++)
                {
                    isChecked[i] = 0;
                }

                antrian = antrian.Concat(searchFolder).ToArray();
                foreach (string folder in searchFolder)
                {
                    Array.Resize(ref searchAll, searchAll.Length + 1);
                    searchAll[searchAll.Length - 1] = folder;
                    string[] folderArr = folder.Split('\\');
                    if (folderArr[folderArr.Length - 1] == InputFile)
                    {
                        return searchAll;
                    }
                }

                foreach (string file in searchFile)
                {
                    Array.Resize(ref searchAll, searchAll.Length + 1);
                    searchAll[searchAll.Length - 1] = file;
                    string[] fileArr = file.Split('\\');
                    if (fileArr[fileArr.Length - 1] == InputFile)
                    {
                        return searchAll;
                    }
                }

                antrian = antrian.Where((source, index) => index != 0).ToArray();

                for (int i = 0; i < antrian.Length; i++)
                {
                    Console.Write(antrian[i]);
                    Console.Write(" ");
                }

                Console.WriteLine();

                return searchAll.Concat(cariAll2(antrian, InputFile)).ToArray();
            }

        }

        public static string[][] cariAll3(string[] antrian, string InputFile)
        {
            if (antrian.Length == 0)
            {
                string[][] kosong = { };
                return kosong;
            }

            else
            {
                IList<string> searchFolderTemp = new List<string>();
                try
                {
                    Directory.GetDirectories(antrian[0] + "\\")
                    .ToList()
                    .ForEach(s => searchFolderTemp.Add(s));
                }

                catch (UnauthorizedAccessException)
                {
                    //
                }

                IList<string> searchFileTemp = new List<string>();
                try
                {
                    Directory.GetFiles(antrian[0] + "\\")
                    .ToList()
                    .ForEach(s => searchFileTemp.Add(s));
                }

                catch (UnauthorizedAccessException)
                {
                    //
                }


                string[] searchFolder = searchFolderTemp.ToArray();
                string[] searchFile = searchFileTemp.ToArray();
                int[] isChecked = new int[searchFolder.Length + searchFile.Length];
                string[][] searchAll = new string[searchFolder.Length + searchFile.Length + 1][];
                for (int i = 0; i < isChecked.Length; i++)
                {
                    isChecked[i] = 0;
                }

                antrian = antrian.Concat(searchFolder).ToArray();
                int angka = 0;
                foreach (string folder in searchFolder)
                {
                    string[] pertamaArr = antrian[0].Split('\\');
                    if (pertamaArr.Length == 0)
                    {
                        string[] pertamaTemp = { antrian[0] };
                        pertamaArr = pertamaTemp;
                    }

                    string hasilpertama = pertamaArr[pertamaArr.Length - 1];

                    string[] keduaArr = folder.Split('\\');
                    if (keduaArr.Length == 0)
                    {
                        string[] keduaTemp = { folder };
                        keduaArr = keduaTemp;
                    }

                    string hasilkedua = keduaArr[keduaArr.Length - 1];
                    string[] hasilgabung = { hasilpertama, hasilkedua, "0" };
                    searchAll[angka] = hasilgabung;

                    if (hasilkedua == InputFile)
                    {
                        searchAll[angka][2] = "1";
                        angka++;
                        string[] hasilCari = { folder };
                        searchAll[angka] = hasilCari;
                        return searchAll;
                    }

                    angka++;
                }

                foreach (string file in searchFile)
                {
                    string[] pertamaArr = antrian[0].Split('\\');
                    if (pertamaArr.Length == 0)
                    {
                        string[] pertamaTemp = { antrian[0] };
                        pertamaArr = pertamaTemp;
                    }

                    string hasilpertama = pertamaArr[pertamaArr.Length - 1];

                    string[] keduaArr = file.Split('\\');
                    if (keduaArr.Length == 0)
                    {
                        string[] keduaTemp = { file };
                        keduaArr = keduaTemp;
                    }

                    string hasilkedua = keduaArr[keduaArr.Length - 1];
                    string[] hasilgabung = { hasilpertama, hasilkedua, "0" };
                    searchAll[angka] = hasilgabung;

                    if (hasilkedua == InputFile)
                    {
                        searchAll[angka][2] = "1";
                        angka++;
                        string[] hasilCari = { file };
                        searchAll[angka] = hasilCari;
                        return searchAll;
                    }
                    angka++;
                }

                antrian = antrian.Where((source, index) => index != 0).ToArray();

                for (int i = 0; i < antrian.Length; i++)
                {
                    Console.Write(antrian[i]);
                    Console.Write(" ");
                }

                Console.WriteLine();

                return searchAll.Concat(cariAll3(antrian, InputFile)).ToArray();
            }

        }

        public static string[][] cariAll4(string[] antrian, string InputFile)
        {
            if (antrian.Length == 0)
            {
                string[][] kosong = { };
                return kosong;
            }

            else
            {
                IList<string> searchFolderTemp = new List<string>();
                try
                {
                    Directory.GetDirectories(antrian[0] + "\\")
                    .ToList()
                    .ForEach(s => searchFolderTemp.Add(s));
                }

                catch (UnauthorizedAccessException)
                {
                    //
                }

                IList<string> searchFileTemp = new List<string>();
                try
                {
                    Directory.GetFiles(antrian[0] + "\\")
                    .ToList()
                    .ForEach(s => searchFileTemp.Add(s));
                }

                catch (UnauthorizedAccessException)
                {
                    //
                }


                string[] searchFolder = searchFolderTemp.ToArray();
                string[] searchFile = searchFileTemp.ToArray();
                int[] isChecked = new int[searchFolder.Length + searchFile.Length];
                string[][] searchAll = new string[searchFolder.Length + searchFile.Length + 1][];
                for (int i = 0; i < isChecked.Length; i++)
                {
                    isChecked[i] = 0;
                }

                antrian = antrian.Concat(searchFolder).ToArray();
                int angka = 0;
                foreach (string folder in searchFolder)
                {
                    string[] pertamaArr = antrian[0].Split('\\');
                    if (pertamaArr.Length == 0)
                    {
                        string[] pertamaTemp = { antrian[0] };
                        pertamaArr = pertamaTemp;
                    }

                    string hasilpertama = pertamaArr[pertamaArr.Length - 1];

                    string[] keduaArr = folder.Split('\\');
                    if (keduaArr.Length == 0)
                    {
                        string[] keduaTemp = { folder };
                        keduaArr = keduaTemp;
                    }

                    string hasilkedua = keduaArr[keduaArr.Length - 1];
                    string[] hasilgabung = { antrian[0], folder, "0" };
                    searchAll[angka] = hasilgabung;

                    if (hasilkedua == InputFile)
                    {
                        searchAll[angka][2] = "1";
                        angka++;
                        string[] hasilCari = { folder };
                        searchAll[angka] = hasilCari;
                    }

                    angka++;
                }

                foreach (string file in searchFile)
                {
                    string[] pertamaArr = antrian[0].Split('\\');
                    if (pertamaArr.Length == 0)
                    {
                        string[] pertamaTemp = { antrian[0] };
                        pertamaArr = pertamaTemp;
                    }

                    string hasilpertama = pertamaArr[pertamaArr.Length - 1];

                    string[] keduaArr = file.Split('\\');
                    if (keduaArr.Length == 0)
                    {
                        string[] keduaTemp = { file };
                        keduaArr = keduaTemp;
                    }

                    string hasilkedua = keduaArr[keduaArr.Length - 1];
                    string[] hasilgabung = { antrian[0], file, "0" };
                    searchAll[angka] = hasilgabung;

                    if (hasilkedua == InputFile)
                    {
                        searchAll[angka][2] = "1";
                        angka++;
                        string[] hasilCari = { file };
                        searchAll[angka] = hasilCari;
                    }
                    angka++;
                }

                antrian = antrian.Where((source, index) => index != 0).ToArray();

                return searchAll.Concat(cariAll4(antrian, InputFile)).ToArray();
            }

        }

        //public static void BFS(string InputFolderUtama, string InputFile)
        public static Microsoft.Msagl.GraphViewerGdi.GViewer BFS(string InputFolderUtama, string InputFile)
        {
            string[] firstSearch = { InputFolderUtama };

            string[][] hasilSearch2 = cariAll3(firstSearch, InputFile);
            string hasilCari = "*";
            string[] hasilCariArr = { };
            for (int i = 0; i < hasilSearch2.Length; i++)
            {
                if (hasilSearch2[i] != null)
                {
                    if (hasilSearch2[i].Length == 1)
                    {
                        hasilCari = hasilSearch2[i][0];
                        hasilSearch2 = hasilSearch2.Where((source, index) => index != i).ToArray();
                        hasilCariArr = hasilCari.Split('\\');
                    }
                }
            }

            for (int i = 0; i < hasilSearch2.Length; i++)
            {
                if (hasilSearch2[i] != null)
                {
                    int isPart = 0;
                    if (hasilCariArr.Length > 0)
                    {
                        for (int j = 0; j < hasilCariArr.Length - 1; j++)
                        {
                            if (hasilCariArr[j] == hasilSearch2[i][0] && hasilCariArr[j + 1] == hasilSearch2[i][1])
                            {
                                isPart = 1;
                            }
                        }
                    }

                    if (isPart == 1)
                    {
                        hasilSearch2[i][2] = "1";
                    }
                }
            }

            Console.WriteLine(hasilSearch2.Length);
            for (int i = 0; i < hasilSearch2.Length; i++)
            {
                if (hasilSearch2[i] != null)
                {
                    for (int j = 0; j < hasilSearch2[i].Length; j++)
                    {
                        Console.Write(hasilSearch2[i][j]);
                        Console.Write(" ");
                    }
                    Console.WriteLine();

                }
            }

            //create a form 
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            //create the graph content 
            for (int i = 0; i < hasilSearch2.Length; i++)
            {
                if (hasilSearch2[i] != null)
                {
                    if (hasilSearch2[i][2] == "0")
                    {
                        graph.AddEdge(hasilSearch2[i][0], hasilSearch2[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    }

                    else
                    {
                        graph.AddEdge(hasilSearch2[i][0], hasilSearch2[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                    }
                }
            }

            //bind the graph to the viewer 
            viewer.Graph = graph;
            //associate the viewer with the form 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;/*
            form.Controls.Add(viewer);
            form.ResumeLayout();
            //show the form 
            form.ShowDialog();*/
            return viewer;
        }

        //public static void BFS2(string InputFolderUtama, string InputFile)
        public static Microsoft.Msagl.GraphViewerGdi.GViewer BFS2(string InputFolderUtama, string InputFile)
        {
            string[] firstSearch = { InputFolderUtama };
            string[][] hasilSearch = cariAll4(firstSearch, InputFile);
            int countSearch = 0;
            for (int i = 0; i < hasilSearch.Length; i++)
            {
                if (hasilSearch[i] != null)
                {
                    if (hasilSearch[i].Length == 1)
                    {
                        countSearch++;
                    }

                }
            }

            string[] lokasiCari = new string[countSearch];
            int rowLocation = 0;
            for (int i = 0; i < hasilSearch.Length; i++)
            {
                if (hasilSearch[i] != null)
                {
                    if (hasilSearch[i].Length == 1)
                    {
                        lokasiCari[rowLocation] = hasilSearch[i][0];
                        rowLocation++;
                    }

                }
            }

            for (int i = 0; i < hasilSearch.Length; i++)
            {
                if (hasilSearch[i] != null && hasilSearch[i].Length != 1)
                {
                    int isPart = 0;
                    for (int j = 0; j < lokasiCari.Length; j++)
                    {
                        if (lokasiCari[j] != null)
                        {
                            if (lokasiCari[j].Contains(hasilSearch[i][1]))
                            {
                                isPart = 1;
                            }
                        }
                    }

                    if (isPart == 1)
                    {
                        hasilSearch[i][2] = "1";
                    }
                }
            }

            Console.WriteLine(hasilSearch.Length);
            for (int i = 0; i < hasilSearch.Length; i++)
            {
                if (hasilSearch[i] != null)
                {
                    for (int j = 0; j < hasilSearch[i].Length; j++)
                    {
                        Console.Write(hasilSearch[i][j]);
                        Console.Write(" ");
                    }
                    Console.WriteLine();

                }
            }

            //create a form 
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            //create the graph content 
            for (int i = 0; i < hasilSearch.Length; i++)
            {
                if (hasilSearch[i] != null && hasilSearch[i].Length != 1)
                {
                    if (hasilSearch[i][2] == "0")
                    {
                        graph.AddEdge(hasilSearch[i][0], hasilSearch[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    }

                    else
                    {
                        graph.AddEdge(hasilSearch[i][0], hasilSearch[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                    }
                }
            }

            //bind the graph to the viewer 
            viewer.Graph = graph;
            //associate the viewer with the form 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            /*
            form.ResumeLayout();
            //show the form 
            form.ShowDialog();
            */
            return viewer;
        }
        /*
        public static void Main()
        {
            BFS("F:", "lima");
            BFS2("F:", "lima");
        }*/
    }
}