using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace DriverDownloader
{
    class SevenZipInterface
    {
        bool filesExist;
        string WorkingDirectory;
        string BaseDirectory;
        string ExtractorName;

        public SevenZipInterface()
        {
            init();
        }

        private void init()
        {
            BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            WorkingDirectory = BaseDirectory + @"7zip\";
            ExtractorName = "7z.exe";
            checkResources();
        }


        void checkResources()
        {
            filesExist = true;
            string exe = WorkingDirectory + ExtractorName;
            string dll = WorkingDirectory + "7z.dll";
            if (!File.Exists(exe))
            {
                Console.WriteLine("7z.exe not found in " + WorkingDirectory);
                filesExist = false;
            }
            if (!File.Exists(dll))
            {
                Console.WriteLine("7z.dll not found in " + WorkingDirectory);
                filesExist = false;
            }
        }

        public bool extractorExists()
        {
            return filesExist;
        }

        public bool Extract(DriverDownloader.Core.Downloader downloader)
        {
            bool success = false;
            if (filesExist)
            {
                string archivePath = downloader.LocalFile;
                string outputdir = getDefaultOutputDir(archivePath);
                if (File.Exists(archivePath))
                {
                    try
                    {
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.WindowStyle = ProcessWindowStyle.Hidden;
                        psi.WorkingDirectory = WorkingDirectory;
                        psi.FileName = ExtractorName;
                        psi.Arguments = generateArguments(archivePath,outputdir);
                        Process p = Process.Start(psi);
                        p.WaitForExit();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        success = false;
                    }
                }
                else
                {
                    Console.WriteLine("Couldn't find archive in " + archivePath);
                    success = false;
                }
                /*//DEBUG
                Console.WriteLine("Working Directory " + WorkingDirectory);
                Console.WriteLine("Archive Path " + archivePath);
                Console.WriteLine("Output Path " + outputdir);
                Console.WriteLine("Arguments " + generateArguments(archivePath,outputdir));
                //*/
            }
            else
            {
                Console.WriteLine("Cannot extract files without 7z.exe and its dependecies");
                success = false;
            }
            return success;
        }

        string getDefaultOutputDir(string archivePath)
        {
            string toreturn = FileManager.trimString(archivePath,@".exe");
            //return toreturn+"_extracted";
            return toreturn;
        }

        string generateArguments(string archive, string output)
        {
            return "x " + "\""+archive+"\"" + " -aoa -o" + "\""+output+"\"";
        }
    }
}
