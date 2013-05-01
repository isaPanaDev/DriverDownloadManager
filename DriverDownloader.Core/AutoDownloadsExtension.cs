using System;
using System.Collections.Generic;
using System.Text;
using MyDownloader.Core;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;

namespace DriverDownloader.Core
{
    public class AutoDownloadsExtension : IExtension, IDisposable
    {
        DayHourMatrix matrix;
        private bool active;
        private bool needToRestore;
        private System.Threading.Timer timer;
        private IAutoDownloadsParameters parameters;

        #region IExtension Members

        public string Name
        {
            get { return "Auto-Downloads"; }
        }

        public IUIExtension UIExtension
        {
            get { return new AutoDownloadsUIExtension(); }
        }

        #endregion

        #region Properties

        public bool Active
        {
            get { return active; }
            set
            {
                active = value;
                if (active)
                {
                    StartJobsIfNeeded();
                }
                else
                {
                    RestoreIfNecessary();
                }
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            RestoreIfNecessary();

            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }

        #endregion

        #region Methods

        private void PersistList(object state)
        {
            StartJobsIfNeeded();
        }

        private void LoadTimes()
        {
            matrix = new DayHourMatrix(parameters.TimesToWork);
        }

        void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TimesToWork")
            {
                LoadTimes();
            }
            else if (e.PropertyName == "concurrentMaxJobs")
            {
                StartJobsIfNeeded();
            }
        }

        void Instance_DownloadRemoved(object sender, DownloaderEventArgs e)
        {
            StartJobsIfNeeded();
        }

        void Instance_DownloadEnded(object sender, DownloaderEventArgs e)
        {
            StartJobsIfNeeded();
        }

        void Instance_EndAddBatchDownloads(object sender, EventArgs e)
        {
            StartJobsIfNeeded();
        }

        void Instance_DownloadAdded(object sender, DownloaderEventArgs e)
        {
            if (!e.WillStart)
            {
                StartJobsIfNeeded();

                e.Downloader.StateChanged += delegate(object s, EventArgs ea)
                {
                    if (((Downloader)s).State == DownloaderState.WaitingForReconnect)
                    {
                        StartJobsIfNeeded();
                    }
                };
            }
        }

        //@note - you can directly return the value, returned
        //        DownloadManager.GetActiveJobsCount() also.
        private int GetActiveJobsCount()
        {
            if (DownloadManager.Instance != null)
                return DownloadManager.Instance.GetActiveJobsCount();
            else
                return -1;
            /*int count = 0;

            for (int i = 0; i < DownloadManager.Instance.Downloads.Count; i++)
            {
                if (DownloadManager.Instance.Downloads[i].IsWorking() &&
                    DownloadManager.Instance.Downloads[i].State != DownloaderState.WaitingForReconnect)
                {
                    count++;
                }
            }

            return count;*/
        }

        private void StartJobsIfNeeded()
        {
            //Console.WriteLine("In StartJobsIfNeeded...");
            if (!Active || DownloadManager.Instance.AddBatchCount > 0)
            {
                Debug.WriteLine("Leaving StartJobsIfNeeded");
                return;
            }

            DateTime now = DateTime.Now;

            EnableMode em = matrix[now.DayOfWeek, now.Hour];

            if (parameters.WorkOnlyOnSpecifiedTimes && em == EnableMode.Disabled)
            {
                RestoreIfNecessary();
                return;
            }

            if (em == EnableMode.ActiveWithLimit)
            {
                SpeedLimitExtension limit = (SpeedLimitExtension)AppManager.Instance.Application.GetExtensionByType(typeof(SpeedLimitExtension));

                if (limit.Parameters.MaxRate != parameters.MaxRateOnTime)
                {
                    //limit.Parameters.Enabled = true;
                    //limit.Parameters.MaxRate = parameters.MaxRateOnTime;
                    limit.SetMaxRateTemp(parameters.MaxRateOnTime);
                    needToRestore = true;
                }
            }
            else
            {
                RestoreIfNecessary();
            }

         //   int maxJobs = parameters.MaxJobs;
            int maxJobs = Settings.Default.concurrentMaxJobs;
            //MessageBox.Show("Setting MaxJobs: " + maxJobs);

            using (DownloadManager.Instance.LockDownloadList(false))
            {
                int count = GetActiveJobsCount();
               // MessageBox.Show("ActiveJobsCount: " + count);
                if (count < maxJobs)
                {
                    for (int i = 0; (count < maxJobs) && i < DownloadManager.Instance.Downloads.Count; i++)
                    {
                        Downloader d = DownloadManager.Instance.Downloads[i];
                        Console.WriteLine("StartJobs: Current State: " + d.State);
                        if (d.State != DownloaderState.Ended && !d.IsWorking() && 
                            d.State != DownloaderState.Paused &&
                            d.State != DownloaderState.Pausing &&
                            d.State != DownloaderState.EndedWithError)
                        //if (d.State==DownloaderState.NeedToPrepare)
                        {
                            DownloadManager.Instance.Downloads[i].Start();
                            count++;
                        }
                    }
                }
            }
        }

        private void RestoreIfNecessary()
        {
            if (needToRestore)
            {
                SpeedLimitExtension limit = (SpeedLimitExtension)AppManager.Instance.Application.GetExtensionByType(typeof(SpeedLimitExtension));

                limit.RestoreMaxRateFromParameters();
                needToRestore = false;
            }
        }

        #endregion

        #region Constructor

        public AutoDownloadsExtension() :
            this(new AutoDownloadsParametersSettingsProxy())
        {
        }

        public AutoDownloadsExtension(IAutoDownloadsParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            this.parameters = parameters;

            parameters.ParameterChanged += new PropertyChangedEventHandler(Default_PropertyChanged);

            DownloadManager.Instance.EndAddBatchDownloads += new EventHandler(Instance_EndAddBatchDownloads);
            DownloadManager.Instance.DownloadAdded += new EventHandler<DownloaderEventArgs>(Instance_DownloadAdded);
            DownloadManager.Instance.DownloadEnded += new EventHandler<DownloaderEventArgs>(Instance_DownloadEnded);
            //DownloadManager.Instance.DownloadRemoved += new EventHandler<DownloaderEventArgs>(Instance_DownloadRemoved);

            LoadTimes();

            TimerCallback refreshCallBack = new TimerCallback(PersistList);
            TimeSpan refreshInterval = TimeSpan.FromMinutes(1);
           // timer = new Timer(refreshCallBack, null, new TimeSpan(-1), refreshInterval);

            this.Active = true;
        }

        #endregion
    }
}