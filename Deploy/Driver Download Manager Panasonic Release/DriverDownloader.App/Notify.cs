using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DriverDownloader
{
    class Notify
    {

        public static bool proceedPause(IWin32Window parent, string filename)
        {
            string message = "The following file '" + filename + "' cannot be resumed.\n(all downloaded data will be lost)\n"+
                               "Click yes to proceed.";
            return showConfirmDialog(parent, message);
        }

        public static bool proceedClearAll(IWin32Window parent)
        {
            string message = "Are you sure that you want to remove all downloads?";
            return showConfirmDialog(parent, message);
        }

        public static bool proceedClearAllCompleted(IWin32Window parent)
        {
            string message = "Are you sure, you want to remove these files?";
            return showConfirmDialog(parent, message);
        }

        public static bool proceedPause(IWin32Window parent, List<string> filenames)
        {
            if (filenames.Count == 1)
            {
                string filename = filenames[0];
                return proceedPause(parent, filename);
            }
            else
            {
                string message = filenames.Count + " files cannot be resumed";
                /*foreach(string filename in filenames)
                    message += ("\n" + filename);*/
                message += "\n(all downloaded data will be lost)";
                message += "\nClick yes to proceed.";
                return showConfirmDialog(parent, message);
            }
        }

        public static bool keepSettings(IWin32Window parent)
        {
            string message = "Application upgraded to Driver Downloader " + Properties.Settings.Default.Version +
                                "\n Do you want to keep your previous settings? ";
            return showConfirmDialog(parent, message);
        }

        static bool showConfirmDialog(IWin32Window parent, string message)
        {
            string caption = "Warning";
            DialogResult result = MessageBox.Show(parent,
                                                    message,
                                                    caption,
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Warning,
                                                    MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }

        /**
         * Pops a notifcation when current value set for concurrent jobs
         * is less than current active jobs
         * @paran parent - parent window
         * @param val - max concurrent threads the user wants to set
         * @param activeJobs - current active jobs
         */ 
        public static void invalidConcurrentThreadValue(IWin32Window parent, int val, int activeJobs)
        {
            string message = "The number of currently active downloads" +
                                "\nare more than the value set for MaxJobs." +
                                "\nPlease pause all downloads and then change the respective" +
                                "\nsettings.";
            showInfoDialog(parent, message);
        }

        public static void maxJobsReached(IWin32Window parent, int maxJobs)
        {
            string message = "Only " + maxJobs + " simultaneous downloads allowed.";
            showInfoDialog(parent, message);
        }

        static void showInfoDialog(IWin32Window parent, string message)
        {
            string caption = "Info";
            DialogResult result = MessageBox.Show(parent,
                                                    message,
                                                    caption,
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information,
                                                    MessageBoxDefaultButton.Button1);
        }

    }
}
