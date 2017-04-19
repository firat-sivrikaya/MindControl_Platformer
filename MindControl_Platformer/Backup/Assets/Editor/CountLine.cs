
using UnityEngine;
using UnityEditor;
using System.Collections;

public class CountLine : MonoBehaviour {
   
    struct File
    {
        public string   name;
        public int   nbLines;
       
        public File(string name, int nbLines)
        {
            this.name     = name;
            this.nbLines    = nbLines;
        }
    }   
   
    [MenuItem("Tools/Count Lines")]
    static void CountLines()
    {      
        string strDir = System.IO.Directory.GetCurrentDirectory();
        strDir += @"/Assets";
        int iLengthOfRootPath = strDir.Length;
        ArrayList stats = new ArrayList(); 
        ProcessDirectory(stats, strDir);   
       
        int iTotalNbLines = 0;
        foreach(File f in stats)
        {
            iTotalNbLines += f.nbLines;
        }
       
        System.Text.StringBuilder strStats = new System.Text.StringBuilder();
        strStats.Append("Number of Files: " + stats.Count + "\n");   
        strStats.Append("Number of Lines: " + iTotalNbLines + "\n");   
        strStats.Append("================\n"); 
       
        foreach(File f in stats)
        {
            strStats.Append(f.name.Substring(iLengthOfRootPath+1, f.name.Length-iLengthOfRootPath-1) + " --> " + f.nbLines + "\n");
        }      
       
        EditorUtility.DisplayDialog("Statistics", strStats.ToString(), "Ok");
    }
   
    static void ProcessDirectory(ArrayList stats, string dir)
    {   
        string[] strArrFiles = System.IO.Directory.GetFiles(dir, "*.cs");
        foreach (string strFileName in strArrFiles)
            ProcessFile(stats, strFileName);

        strArrFiles = System.IO.Directory.GetFiles(dir, "*.js");
        foreach (string strFileName in strArrFiles)
            ProcessFile(stats, strFileName);

        string[] strArrSubDir = System.IO.Directory.GetDirectories(dir);
        foreach (string strSubDir in strArrSubDir)
            ProcessDirectory(stats, strSubDir);
    }
   
    static void ProcessFile(ArrayList stats, string filename)
    {
        System.IO.StreamReader reader = System.IO.File.OpenText(filename);
        int iLineCount = 0;
        while (reader.Peek() >= 0)
        {
            reader.ReadLine();
            ++iLineCount;
        }
        stats.Add(new File(filename, iLineCount));
        reader.Close();   
    }   
}