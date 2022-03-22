using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;

class DepthFirstSearch
{
    public static string[] getListPath(string firstDir)
    {
        if (firstDir.Length == 0)
        {
            string[] kosong = { };
            return kosong;
        }

        else
        {
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

            string[] searchFolder = searchFolderTemp.ToArray();
            string[] searchFile = searchFileTemp.ToArray();
            Array.Sort(searchFolder, StringComparer.OrdinalIgnoreCase);
            Array.Sort(searchFile, StringComparer.OrdinalIgnoreCase);
            string[] listPath = new string[searchFolder.Length + searchFile.Length + 1];

            foreach (string folder in searchFolder)
            {
                string[] folderResult = { folder };
                listPath = listPath.Concat(folderResult).ToArray();
                listPath = listPath.Concat(getListPath(folderResult[0])).ToArray();
            }

            listPath = listPath.Concat(searchFile).ToArray();
            listPath = listPath.Where(s => s != null).ToArray();

            return listPath;
        }
    }

    public static string[][] getMatrixNodeOnce(string[] listPath, string fileName)
    {
        string[][] matrix = new string[listPath.Length][];
        List<string> listPreviousPath = new List<string>();
        int row = 0;
        foreach (string path in listPath)
        {
            string[] arrLeaf = path.Split('\\');
            string leaf = arrLeaf[arrLeaf.Length - 1];
            string previousPath = path.Substring(0, path.Length - leaf.Length - 1);
            string[] hasilGabung = { previousPath, leaf, "0" }; // Awal, warnai hitam
            matrix[row] = hasilGabung;

            row++;
        }

        row = 0;
        foreach (string path in listPath)
        {
            string[] arrLeaf = path.Split('\\');
            string leaf = arrLeaf[arrLeaf.Length - 1];
            string previousPath = path.Substring(0, path.Length - leaf.Length - 1);
            string[] hasilGabung = { previousPath, leaf, "0" }; // Awal, warnai hitam
            matrix[row] = hasilGabung;

            if (leaf == fileName)
            {
                matrix[row][2] = "1"; // Kalau ketemu, warnai biru
                listPreviousPath.Add(previousPath);
                break;
            }
            else
            {
                matrix[row][2] = "2"; // Kalau tidak ketemu, warnai merah
            }

            row++;
        }

        foreach (string[] node in matrix)
        {
            string prevPath = node[0];
            string[] arrFolder = prevPath.Split('\\');
            node[0] = arrFolder[arrFolder.Length - 1];
        }

        foreach (string path in listPreviousPath)
        {
            string[] arrPath = path.Split('\\');
            for (int i = 0; i < arrPath.Length - 1; i++)
            {
                string[] edge = { arrPath[i], arrPath[i + 1] };
                foreach (string[] node in matrix)
                {
                    if (node[0] == edge[0] && node[1] == edge[1])
                    {
                        node[2] = "1";
                        break;
                    }
                }
            }
        }

        return matrix;
    }

    public static string[][] getMatrixNodeAllOccurrence(string[] listPath, string fileName)
    {
        string[][] matrix = new string[listPath.Length][];
        List<string> listPreviousPath = new List<string>();
        int row = 0;
        foreach (string path in listPath)
        {
            string[] arrLeaf = path.Split('\\');
            string leaf = arrLeaf[arrLeaf.Length - 1];
            string previousPath = path.Substring(0, path.Length - leaf.Length - 1);
            string[] hasilGabung = {previousPath, leaf, "0"}; // Awal, warnai hitam
            matrix[row] = hasilGabung;

            if (leaf == fileName)
            {
                matrix[row][2] = "1"; // Kalau ketemu, warnai biru
                listPreviousPath.Add(previousPath);
            }
            else
            {
                matrix[row][2] = "2"; // Kalau tidak ketemu, warnai merah
            }

            row++;
        }

        foreach (string[] node in matrix)
        {
            string prevPath = node[0];
            string[] arrFolder = prevPath.Split('\\');
            node[0] = arrFolder[arrFolder.Length - 1];
        }

        foreach (string path in listPreviousPath)
        {
            string[] arrPath = path.Split('\\');
            for (int i = 0; i < arrPath.Length - 1; i++)
            {
                string[] edge = { arrPath[i], arrPath[i+1] };
                foreach (string[] node in matrix)
                {
                    if (node[0] == edge[0] && node[1] == edge[1])
                    {
                        node[2] = "1";
                        break;
                    }
                }
            }
        }

        return matrix;
    }

    public static Microsoft.Msagl.GraphViewerGdi.GViewer displayGraph(string[][] matrixNode)
    {
        //create a form 
        System.Windows.Forms.Form form = new System.Windows.Forms.Form();
        //create a viewer object 
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        //create a graph object 
        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
        //create the graph content 
        for (int i = 0; i < matrixNode.Length; i++)
        {
            if (matrixNode[i][2] == "0")
            {
                graph.AddEdge(matrixNode[i][0], matrixNode[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
            }
            else if (matrixNode[i][2] == "1")
            {
                graph.AddEdge(matrixNode[i][0], matrixNode[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
            }
            else if (matrixNode[i][2] == "2")
            {
                graph.AddEdge(matrixNode[i][0], matrixNode[i][1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
            }
        }

        //bind the graph to the viewer 
        viewer.Graph = graph;
        //associate the viewer with the form 
        form.SuspendLayout();
        viewer.Dock = System.Windows.Forms.DockStyle.Fill;
        form.Controls.Add(viewer);
        form.ResumeLayout();
        //show the form 
        form.ShowDialog();
        
        return viewer;
    }

    public static void Main()
    {
        string firstDir = "E:\\KERAMAT";
        string fileName = "test.txt";
        string[][] matrixNode;
        string[] lihatResult = getListPath(firstDir);
        matrixNode = getMatrixNodeOnce(lihatResult, fileName);
        
        // Menampilkan isi matrix
        for (int i = 0; i < matrixNode.Length; i++)
        {
            Console.WriteLine(matrixNode[i][0] + " " + matrixNode[i][1] + " " + matrixNode[i][2]);
        }
        // Menampilkan path hasil
        for (int i = 0; i < lihatResult.Length; i++)
        {
            string[] arrHasil = lihatResult[i].Split('\\');
            if (arrHasil[arrHasil.Length - 1] == fileName)
            {
                Console.WriteLine(lihatResult[i]);
            }
        }

        displayGraph(matrixNode);
    }
}