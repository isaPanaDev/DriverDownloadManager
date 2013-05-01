using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Deployment;
using DriverDownloader;
using DriverDownloader.Core;

namespace DriverDownloader
{
 
    public interface IInitializable
    {
        void Init();
    }
   
    [Serializable]
    public class App : IApp
    {
        #region Singleton

        private static App instance = new App();

        public static App Instance
        {
            get
            {
                return instance;
            }
        }

        private App()
        {
            AppManager.Instance.Initialize(this);

            uman = new UpdateManager();
            upgradeCheck();

            extensions = new List<IExtension>();

            extensions.Add(new HttpFtpProtocolExtension());
            extensions.Add(new VideoDownloadExtension());
            extensions.Add(new SpeedLimitExtension());
            extensions.Add(new AutoDownloadsExtension());
            extensions.Add(new PersistedListExtension());
        }

        #endregion

        #region Fields

        private List<IExtension> extensions;
        private SingleInstanceTracker tracker = null;
        private bool disposed = false;
        private UpdateManager uman;

        #endregion

        #region Properties

        public Form MainForm
        {
            get
            {
                return (MainForm)tracker.Enforcer;
            }
        }

        public List<IExtension> Extensions
        {
            get
            {
                return extensions;
            }
        }

        #endregion

        #region Methods

        /**
         * Check if application was upgraded. 
         * If yes, then popup a confirmation dialog to ask the user whether he would like to overide his settings
         */
        private void upgradeCheck()
        {
            if (DriverDownloader.Core.Settings.Default.CheckUpdates)
                uman.InstallUpdateSyncWithInfo();
            bool upgrade = Properties.Settings.Default.UpgradeRequired;
            string userVersion = DriverDownloader.Core.Settings.Default.CurrentVersion;
            string currentVersion = Properties.Settings.Default.Version;
            if (!currentVersion.Equals(userVersion) && upgrade)
            {
                //Console.WriteLine("user version: (debug) " + userVersion);
                //Console.WriteLine("Current MaxJobs: " + DriverDownloader.Core.Settings.Default.concurrentMaxJobs);
                if (!userVersion.Equals("")) // Do NOT show the popup message on the first time launch
                {
                    if (!Notify.keepSettings(null)) // If user does "not" want to keep settings
                    {
                        DriverDownloader.Core.Settings.Default.Reset();
                    }

                }
                DriverDownloader.Core.Settings.Default.CurrentVersion = currentVersion;
                DriverDownloader.Core.Settings.Default.Save();
                // MessageBox.Show("Updated MaxJobs: " + DriverDownloader.Core.Settings.Default.concurrentMaxJobs);
            }
        } 

        public IExtension GetExtensionByType(Type type)
        {
            for (int i = 0; i < this.extensions.Count; i++)
            {
                if (this.extensions[i].GetType() == type)
                {
                    return this.extensions[i];
                }
            }

            return null;
        }

        private ISingleInstanceEnforcer GetSingleInstanceEnforcer()
        {
            return new MainForm();
        }

        public void InitExtensions()
        {
            for (int i = 0; i < Extensions.Count; i++)
            {
                if (Extensions[i] is IInitializable)
                {
                    ((IInitializable)Extensions[i]).Init();
                }
            }

        }
        public void Dispose()
        {
            if (!disposed && TempSettings.Dispose_Resources)
            {
                //@modified
                Console.WriteLine("Disposing");
                disposed = true;
                for (int i = 0; i < Extensions.Count; i++)
                {
                    if (Extensions[i] is IDisposable)
                    {
                        try
                        {
                            ((IDisposable)Extensions[i]).Dispose();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.ToString());
                        }
                    }
                }
            }
        }

        public void Start(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Attempt to create a tracker
                tracker = new SingleInstanceTracker("SingleInstanceSample", new SingleInstanceEnforcerRetriever(GetSingleInstanceEnforcer));

                // If this is the first instance of the application, run the main form
                if (tracker.IsFirstInstance)
                {
                    try
                    {
                        MainForm form = (MainForm)tracker.Enforcer;

                        //form.downloadList1.AddDownloadURLs(ResourceLocation.FromURLArray(args), 1, null, 0);

                        if (Array.IndexOf<string>(args, "/as") >= 0)
                        {
                            form.WindowState = FormWindowState.Minimized;
                        }

                        form.Load += delegate(object sender, EventArgs e)
                        {
                            InitExtensions();

                            if (form.WindowState == FormWindowState.Minimized)
                            {
                                form.HideForm();
                            }

                            if (args.Length > 0)
                            {
                                form.OnMessageReceived(new MessageEventArgs(args));
                            }
                        };

                        form.FormClosing += delegate(object sender, FormClosingEventArgs e)
                        {
                            Dispose();
                        };

                        Application.Run(form);
                    }
                    finally
                    {
                        Dispose();
                    }
                }
                else
                {
                    // This is not the first instance of the application, so do nothing but send a message to the first instance
                    if (args.Length > 0)
                    {
                        tracker.SendMessageToFirstInstance(args);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not create a SingleInstanceTracker object:\n" + ex.Message + "\nApplication will now terminate.\n" + ex.InnerException.ToString());

                return;
            }
            finally
            {
                if (tracker != null)
                    tracker.Dispose();
            }
        }

        #endregion
    }
}

