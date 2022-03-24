using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using CariFile.com;

class ViewerSample
{
    public static string[] cariAll(string[] antrian)
    {   //mencari semua folder dan file dari sebuah folder
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
            Array.Sort(searchFolder, StringComparer.OrdinalIgnoreCase);
            Array.Sort(searchFile, StringComparer.OrdinalIgnoreCase);
            antrian = antrian.Concat(searchFolder).ToArray();
            searchAll = searchAll.Concat(searchFolder).ToArray();
            searchAll = searchAll.Concat(searchFile).ToArray();  

            antrian = antrian.Where((source, index) => index != 0).ToArray();

            return searchAll.Concat(cariAll(antrian)).ToArray();
        }

    }

    public static string[][] getPathId(string[] listPath, string firstDir)
    {   // mengeluarkan id untuk setiap file dan folder yang akan diperiksa
        string[][] pathId = new string[listPath.Length + 1][];
        int leafId = 1;

        string[] firstDirId = { firstDir, "a0" };
        pathId[0] = firstDirId;

        foreach (string path in listPath)
        {
            string pathName = path;
            string ID = "a" + leafId;
            string[] pathIdLine = { pathName, ID };
            pathId[leafId] = pathIdLine;
            leafId++;
        }

        return pathId;
    }

    public static string[] getDirectory(string[] listPath, string fileName)
    {   // mengeluarkan folder dimana file / folder dapat ditemukan
        List<string> listResult = new List<string>();
        foreach (string path in listPath)
        {
            
            string[] arrLeaf = path.Split('\\');
            string leaf = arrLeaf[arrLeaf.Length - 1];
            string previousPath = path.Substring(0, path.Length - leaf.Length - 1);

            if (leaf == fileName)
            {
                listResult.Add(previousPath);
            }
        }

        return listResult.ToArray();
    }

    public static string[] getDirectoryOneFound(string[] listPath, string fileName)
    {   // mengeluarkan folder pertama dimana file / folder dapat ditemukan
        List<string> listResult = new List<string>();
        int isFound = 0;
        foreach (string path in listPath)
        {

            string[] arrLeaf = path.Split('\\');
            string leaf = arrLeaf[arrLeaf.Length - 1];
            string previousPath = path.Substring(0, path.Length - leaf.Length - 1);

            if (leaf == fileName && isFound == 0)
            {
                listResult.Add(previousPath);
                isFound = 1;
            }
        }

        return listResult.ToArray();
    }


    public static string[][] getMatrixNode(string[] listPath, string fileName, string firstDir)
    {   // mengeluarkan matrix berisi id untuk node, nama node, dan warna dari setiap path
        string[][] matrix = new string[listPath.Length][];
        List<string> listPreviousPath = new List<string>();
        int row = 0;

        string[][] pathId = new string[listPath.Length+1][];
        int leafId = 1;

        pathId = getPathId(listPath, firstDir);

        foreach (string path in listPath)
        {
            string[] arrLeaf = path.Split('\\');
            string leaf = arrLeaf[arrLeaf.Length - 1];
            string previousPath = path.Substring(0, path.Length - leaf.Length - 1);
            string previousId = "";
            for (int i = 0; i < pathId.Length; i++)
            {
                if (previousPath == pathId[i][0])
                {
                    previousId = pathId[i][1];
                }
            } //mencari id untuk path menuju file / folder

            string currentId = "";
            for (int i = 0; i < pathId.Length; i++)
            {
                if (path == pathId[i][0])
                {
                    currentId = pathId[i][1];
                }
            } //mencari id untuk path file / folder

            string[] hasilGabung = {previousId, currentId, previousPath, leaf, "0" }; // Awal, warnai hitam

            leafId++;
            matrix[row] = hasilGabung;

            if (leaf == fileName)
            {
                matrix[row][4] = "1"; // Kalau ketemu, warnai biru
                listPreviousPath.Add(previousPath);
            }
            else
            {
                matrix[row][4] = "2"; // Kalau tidak ketemu, warnai merah
            }

            row++;
        }

        foreach (string[] node in matrix)
        {
            string prevPath = node[2];
            string[] arrFolder = prevPath.Split('\\');
            node[2] = arrFolder[arrFolder.Length - 1];
        }

        foreach (string path in listPreviousPath)
        {
            string[] arrPath = path.Split('\\');
            for (int i = 0; i < arrPath.Length - 1; i++)
            {
                string[] edge = { arrPath[i], arrPath[i + 1] };
                foreach (string[] node in matrix)
                {
                    if (node[2] == edge[0] && node[3] == edge[1])
                    {
                        node[4] = "1";
                        break;
                    }
                }
            }
        }

        return matrix;
    }

    public static string[][] getMatrixNodeOneFound(string[] listPath, string fileName, string firstDir)
    {   // mengeluarkan matrix berisi id untuk node, nama node, dan warna dari setiap path
        string[][] matrix = new string[listPath.Length][];
        List<string> listPreviousPath = new List<string>();
        int row = 0;

        string[][] pathId = new string[listPath.Length + 1][];
        int leafId = 1;

        pathId = getPathId(listPath, firstDir);

        int isFound = 0;
        foreach (string path in listPath)
        {
            string[] arrLeaf = path.Split('\\');
            string leaf = arrLeaf[arrLeaf.Length - 1];
            string previousPath = path.Substring(0, path.Length - leaf.Length - 1);
            string previousId = "";
            for (int i = 0; i < pathId.Length; i++)
            {
                if (previousPath == pathId[i][0])
                {
                    previousId = pathId[i][1];
                }
            } //mencari id untuk path menuju file / folder

            string currentId = "";
            for (int i = 0; i < pathId.Length; i++)
            {
                if (path == pathId[i][0])
                {
                    currentId = pathId[i][1];
                }
            } //mencari id untuk path file / folder

            string[] hasilGabung = { previousId, currentId, previousPath, leaf, "0" }; // Awal, warnai hitam

            leafId++;
            matrix[row] = hasilGabung;

            if (leaf == fileName && isFound == 0)
            {
                matrix[row][4] = "1"; // Kalau ketemu, warnai biru
                listPreviousPath.Add(previousPath);
                isFound = 1;
            }
            else if (leaf != fileName && isFound == 0)
            {
                matrix[row][4] = "2"; // Kalau tidak ketemu, warnai merah
            }

            row++;
        }

        foreach (string[] node in matrix)
        {
            string prevPath = node[2];
            string[] arrFolder = prevPath.Split('\\');
            node[2] = arrFolder[arrFolder.Length - 1];
        }

        foreach (string path in listPreviousPath)
        {
            string[] arrPath = path.Split('\\');
            for (int i = 0; i < arrPath.Length - 1; i++)
            {
                string[] edge = { arrPath[i], arrPath[i + 1] };
                foreach (string[] node in matrix)
                {
                    if (node[2] == edge[0] && node[3] == edge[1])
                    {
                        node[4] = "1";
                        break;
                    }
                }
            }
        }

        return matrix;
    }

    public static Result BFSAll(string inputDisk, string inputSearch)
    {
        string[] inputDiskArr = { inputDisk };
        string[] hasilBFS = cariAll(inputDiskArr);

        string[][] pathReference = getPathId(hasilBFS, inputDisk);
        string[][] matrixBFS = getMatrixNode(hasilBFS, inputSearch, inputDisk);


        System.Windows.Forms.Form form = new System.Windows.Forms.Form();
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
        for (int i = 0; i < matrixBFS.Length; i++)
        {   // membuat graf dari id path yang didapatkan
            
            if (matrixBFS[i][4] == "2")
            {
                graph.AddEdge(matrixBFS[i][0], matrixBFS[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                if (graph.FindNode(matrixBFS[i][0]).Attr.Color != Microsoft.Msagl.Drawing.Color.Blue){
                    graph.FindNode(matrixBFS[i][0]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    graph.FindNode(matrixBFS[i][0]).Label.FontColor = Microsoft.Msagl.Drawing.Color.Red;
                }
                graph.FindNode(matrixBFS[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                graph.FindNode(matrixBFS[i][1]).Label.FontColor = Microsoft.Msagl.Drawing.Color.Red;
            }

            else if (matrixBFS[i][4] == "1")
            {
                graph.AddEdge(matrixBFS[i][0], matrixBFS[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                graph.FindNode(matrixBFS[i][0]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                graph.FindNode(matrixBFS[i][0]).Label.FontColor = Microsoft.Msagl.Drawing.Color.Blue;
                graph.FindNode(matrixBFS[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                graph.FindNode(matrixBFS[i][1]).Label.FontColor = Microsoft.Msagl.Drawing.Color.Blue;
            }

            else if (matrixBFS[i][4] == "0")
            {
                graph.AddEdge(matrixBFS[i][0], matrixBFS[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
            }
        }

        
        
        for (int i = 0; i < pathReference.Length; i++) 
        {   // mengubah id dalam graf menjadi nilai file / folder yang benar
                        
            string[] leafLabelArr = pathReference[i][0].Split('\\');
            string leafLabel = "";
            if (leafLabelArr.Length == 0)
            {
                leafLabel = pathReference[i][0];
            }

            else
            {
                leafLabel = leafLabelArr[leafLabelArr.Length - 1];
            }

            graph.FindNode(pathReference[i][1]).Label.Text = leafLabel;
        }
        
        viewer.Graph = graph;
        viewer.Dock = System.Windows.Forms.DockStyle.Fill;

        string[] listResult = getDirectory(hasilBFS, inputSearch);
        Result res = new Result();
        res.listOfPath = listResult;
        res.graph = viewer;
        return res;
    }

    public static Result BFSOne(string inputDisk, string inputSearch)
    {
        string[] inputDiskArr = { inputDisk };
        string[] hasilBFS = cariAll(inputDiskArr);

        string[][] pathReference = getPathId(hasilBFS, inputDisk);
        string[][] matrixBFS = getMatrixNodeOneFound(hasilBFS, inputSearch, inputDisk);

        System.Windows.Forms.Form form = new System.Windows.Forms.Form();
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer(); 
        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
        for (int i = 0; i < matrixBFS.Length; i++)
        {   // membuat graf dari id path yang didapatkan

            if (matrixBFS[i][4] == "2")
            {
                graph.AddEdge(matrixBFS[i][0], matrixBFS[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                if (graph.FindNode(matrixBFS[i][0]).Attr.Color != Microsoft.Msagl.Drawing.Color.Blue)
                {
                    graph.FindNode(matrixBFS[i][0]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    graph.FindNode(matrixBFS[i][0]).Label.FontColor = Microsoft.Msagl.Drawing.Color.Red;
                }
                graph.FindNode(matrixBFS[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                graph.FindNode(matrixBFS[i][1]).Label.FontColor = Microsoft.Msagl.Drawing.Color.Red;
            }

            else if (matrixBFS[i][4] == "1")
            {
                graph.AddEdge(matrixBFS[i][0], matrixBFS[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                graph.FindNode(matrixBFS[i][0]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                graph.FindNode(matrixBFS[i][0]).Label.FontColor = Microsoft.Msagl.Drawing.Color.Blue;
                graph.FindNode(matrixBFS[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                graph.FindNode(matrixBFS[i][1]).Label.FontColor = Microsoft.Msagl.Drawing.Color.Blue;
            }

            else if (matrixBFS[i][4] == "0")
            {
                graph.AddEdge(matrixBFS[i][0], matrixBFS[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
            }
        }



        for (int i = 0; i < pathReference.Length; i++)
        {   // mengubah id dalam graf menjadi nilai file / folder yang benar

            string[] leafLabelArr = pathReference[i][0].Split('\\');
            string leafLabel = "";
            if (leafLabelArr.Length == 0)
            {
                leafLabel = pathReference[i][0];
            }

            else
            {
                leafLabel = leafLabelArr[leafLabelArr.Length - 1];
            }

            graph.FindNode(pathReference[i][1]).Label.Text = leafLabel;
        }

        viewer.Graph = graph;
        //form.SuspendLayout();
        viewer.Dock = System.Windows.Forms.DockStyle.Fill;
        //form.Controls.Add(viewer);
        //form.ResumeLayout();
        //form.ShowDialog();

        string[] listResult = getDirectoryOneFound(hasilBFS, inputSearch);
        Result res = new Result();
        res.listOfPath = listResult;
        res.graph = viewer;
        return res;
    }
}
