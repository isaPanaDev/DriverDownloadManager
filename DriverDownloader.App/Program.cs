using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DriverDownloader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            App.Instance.Start(args);
        }
    }
}
