using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DriverDownloader
{
    class FileManager
    {
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    string dirPath = getDirectory(filePath);
                    if (Directory.Exists(dirPath))
                    {
                        DirectoryInfo dir = new DirectoryInfo(dirPath);
                        if (dir.GetFiles().Length == 0 && dir.GetDirectories().Length == 0)
                            dir.Delete();
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Failed to delete: " + filePath);
                    Console.WriteLine(e.Message);
                }
            }
        }

        /*public static void DeleteDirectory(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                try
                {
                    Console.WriteLine("Deleting dir: " + dirPath);
                    Directory.Delete(dirPath, true);
                    string parentDir = getDirectory(dirPath);
                    Console.WriteLine("Deleting parent dir: " + parentDir);
                    if (Directory.Exists(parentDir))
                    {
                        DirectoryInfo dir = new DirectoryInfo(dirPath);
                        if (dir.GetFiles().Length == 0 && dir.GetDirectories().Length == 0)
                            dir.Delete();
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Failed to delete: " + dirPath);
                    Console.WriteLine(e.Message);
                }
            }
        }*/

        public static void DeleteFiles(List<string> filePaths)
        {
            foreach (string filePath in filePaths)
                DeleteFile(filePath);
        }


        static string getDirectory(string filePath)
        {
            return trimString(filePath, @"\");
        }

        public static string trimString(string s, string delimiter)
        {
            string dirPath = "";
            int end_pos = s.LastIndexOf(delimiter);
            if (end_pos != -1)
            {
                dirPath = s.Substring(0, end_pos);
            }
            //dirPath += @"\";
            //Console.WriteLine("TrimString: " + dirPath);
            return dirPath;
        }

        /**
         * avoid using this in case of finding downloader paths
         * user downloader.LocalFile instead
         */
        public static string getFullPath(string path)
        {
            string fpath;
            if (DriverDownloader.Core.Settings.Default.DownloadFolder.Equals(""))
                fpath = AppDomain.CurrentDomain.BaseDirectory + path;
            else
                fpath = DriverDownloader.Core.Settings.Default.DownloadFolder + path;
            return fpath;
        }

    }
}
