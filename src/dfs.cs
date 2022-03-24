using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using CariFile.com;
class DepthFirstSearch
{
    public static string[] getListPath(string firstDir)
    {   // Menghasilkan daftar path yang mungkin dari sebuah direktori
        if (firstDir.Length == 0)
        {
            string[] kosong = { };
            return kosong;
        }
        else
        {   // Mengambil direktori di bawah firstDir
            IList<string> searchFolderTemp = new List<string>();
            try
            {
                Directory.GetDirectories(firstDir + "\\")
                .ToList()
                .ForEach(s => searchFolderTemp.Add(s));
            }

            catch (UnauthorizedAccessException)
            {
                //
            }
            // Mengambil file di bawah firstDir
            IList<string> searchFileTemp = new List<string>();
            try
            {
                Directory.GetFiles(firstDir + "\\")
                .ToList()
                .ForEach(s => searchFileTemp.Add(s));
            }

            catch (UnauthorizedAccessException)
            {
                //
            }
            // Urutkan sesuai abjad
            string[] searchFolder = searchFolderTemp.ToArray();
            string[] searchFile = searchFileTemp.ToArray();
            Array.Sort(searchFolder, StringComparer.OrdinalIgnoreCase);
            Array.Sort(searchFile, StringComparer.OrdinalIgnoreCase);
            string[] listPath = new string[searchFolder.Length + searchFile.Length + 1];
            // Rekursi untuk mendapatkan semua file dan folder
            foreach (string folder in searchFolder)
            {
                string[] folderResult = { folder };
                listPath = listPath.Concat(folderResult).ToArray();
                listPath = listPath.Concat(getListPath(folderResult[0])).ToArray();
            }
            // Menghapus semua reference null
            listPath = listPath.Concat(searchFile).ToArray();
            listPath = listPath.Where(s => s != null).ToArray();

            return listPath;
        }
    }
    public static string[][] getPathId(string[] listPath, string firstDir)
    {   // Mendapatkan id unik dari sebuah path
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
    public static string[][] getMatrixNodeOnce(string[] listPath, string firstDir, string fileName)
    {   // Mendapatkan matrix node untuk kemunculan fileName yang pertama
        string[][] matrix = new string[listPath.Length][];
        List<string> listPreviousPath = new List<string>();
        string[][] pathReference = getPathId(listPath, firstDir);
        int row = 0;
        
        foreach (string path in listPath)
        {
            string[] arrLeaf = path.Split('\\');
            string leaf = arrLeaf[arrLeaf.Length - 1];
            string previousPath = path.Substring(0, path.Length - leaf.Length - 1);
            string previousId = "";
            foreach (string[] pathId in pathReference)
            {
                if (pathId[0] == previousPath)
                {
                    previousId = pathId[1];
                    break;
                }
            }
            string leafId = "";
            foreach (string[] pathId in pathReference)
            {
                if (pathId[0] == path)
                {
                    leafId = pathId[1];
                    break;
                }
            }
            string[] hasilGabung = { previousId, leafId, previousPath, leaf, "0" }; // Awal, warnai hitam
            matrix[row] = hasilGabung;
            row++;
        }

        row = 0;
        foreach (string path in listPath)
        {
            string[] arrLeaf = path.Split('\\');
            string leaf = arrLeaf[arrLeaf.Length - 1];
            string previousPath = path.Substring(0, path.Length - leaf.Length - 1);
            string previousId = "";
            foreach (string[] pathId in pathReference)
            {
                if (pathId[0] == previousPath)
                {
                    previousId = pathId[1];
                    break;
                }
            }
            string leafId = "";
            foreach (string[] pathId in pathReference)
            {
                if (pathId[0] == path)
                {
                    leafId = pathId[1];
                    break;
                }
            }
            string[] hasilGabung = { previousId, leafId, previousPath, leaf, "0" }; // Awal, warnai hitam
            matrix[row] = hasilGabung;

            if (leaf == fileName)
            {
                matrix[row][4] = "1"; // Kalau ketemu, warnai biru
                listPreviousPath.Add(previousPath);
                break;
            }
            else
            {
                matrix[row][4] = "2"; // Kalau tidak ketemu, warnai merah
            }

            row++;
        }
        // Mengubah path menjadi nama file/folder
        foreach (string[] node in matrix)
        {
            string prevPath = node[2];
            string[] arrFolder = prevPath.Split('\\');
            node[2] = arrFolder[arrFolder.Length - 1];
        }
        // Mengiterasi semua folder di atas file yang ditemukan
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

    public static string[][] getMatrixNodeAllOccurrence(string[] listPath, string firstDir, string fileName)
    {   // Menghasilkan matrix node untuk semua kemunculan file
        string[][] matrix = new string[listPath.Length][];
        List<string> listPreviousPath = new List<string>();
        string[][] pathReference = getPathId(listPath, firstDir);
        int row = 0;
        foreach (string path in listPath)
        {
            string[] arrLeaf = path.Split('\\');
            string leaf = arrLeaf[arrLeaf.Length - 1];
            string previousPath = path.Substring(0, path.Length - leaf.Length - 1);
            string previousId = "";
            foreach (string[] pathId in pathReference)
            {
                if (pathId[0] == previousPath)
                {
                    previousId = pathId[1];
                    break;
                }
            }
            string leafId = "";
            foreach (string[] pathId in pathReference)
            {
                if (pathId[0] == path)
                {
                    leafId = pathId[1];
                    break;
                }
            }
            string[] hasilGabung = { previousId, leafId, previousPath, leaf, "0" }; // Awal, warnai hitam
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
        // Mengubah path menjadi nama file/folder
        foreach (string[] node in matrix)
        {
            string prevPath = node[2];
            string[] arrFolder = prevPath.Split('\\');
            node[2] = arrFolder[arrFolder.Length - 1];
        }
        // Mengiterasi semua folder di atas file yang ditemukan
        foreach (string path in listPreviousPath)
        {
            string[] arrPath = path.Split('\\');
            for (int i = 0; i < arrPath.Length - 1; i++)
            {
                string[] edge = { arrPath[i], arrPath[i+1] };
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

    public static Microsoft.Msagl.GraphViewerGdi.GViewer displayGraph(string[][] pathReference, string[][] matrixNode)
    {
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
        foreach (string[] node in matrixNode)
        {
            if (node[4] == "0")
            {
                graph.AddEdge(node[0], node[1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Black; // Belum dikunjungi
            }
            else if (node[4] == "1")
            {
                graph.AddEdge(node[0], node[1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;  // Ditemukan
                graph.FindNode(node[0]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                graph.FindNode(node[0]).Label.FontColor = Microsoft.Msagl.Drawing.Color.Blue;
                graph.FindNode(node[1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                graph.FindNode(node[1]).Label.FontColor = Microsoft.Msagl.Drawing.Color.Blue;
            }
            else if (node[4] == "2")
            {
                graph.AddEdge(node[0], node[1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;   // Tidak ditemukan
                if (graph.FindNode(node[0]).Attr.Color != Microsoft.Msagl.Drawing.Color.Blue)
                {
                    graph.FindNode(node[0]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    graph.FindNode(node[0]).Label.FontColor = Microsoft.Msagl.Drawing.Color.Red;
                }
                graph.FindNode(node[1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                graph.FindNode(node[1]).Label.FontColor = Microsoft.Msagl.Drawing.Color.Red;
            }
            graph.FindNode(node[0]).Attr.LabelMargin = 50;
            graph.FindNode(node[1]).Attr.LabelMargin = 50;

            graph.FindNode(node[0]).Attr.Padding = 20;
            graph.FindNode(node[1]).Attr.Padding = 20;

            graph.FindNode(node[0]).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Plaintext;
            graph.FindNode(node[1]).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Plaintext;
        }
        // Memberi label untuk setiap node dengan nama file/folder
        foreach (string[] pathId in pathReference)
        {
            string[] leafLabelArr = pathId[0].Split('\\');
            string leafLabel = leafLabelArr[leafLabelArr.Length - 1];

            graph.FindNode(pathId[1]).Label.Text = leafLabel;
        }

        //bind the graph to the viewer 
        viewer.Graph = graph;
        viewer.Dock = System.Windows.Forms.DockStyle.Fill;
        
        return viewer;
    }

    public static Result DFS(string firstDir, string fileName,bool isSearchAll)
    {   // Fungsi utama
        string[] listPath = getListPath(firstDir);
        string[][] pathReference = getPathId(listPath, firstDir);
        Result res = new Result();
        if (isSearchAll)
        {
            string[][] matrixNodeAll = getMatrixNodeAllOccurrence(listPath, firstDir, fileName); // Semua kemunculan
            res.graph =  displayGraph(pathReference, matrixNodeAll);
            res.listOfPath = getDirectory(listPath, fileName);
        }
        else
        {
            string[][] matrixNodeOnce = getMatrixNodeOnce(listPath, firstDir, fileName);         // Kemunculan pertama
            res.graph = displayGraph(pathReference, matrixNodeOnce);
            res.listOfPath = getDirectoryOneFound(listPath, fileName);
        }
        return res;
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
}