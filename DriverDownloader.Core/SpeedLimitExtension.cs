using System;
using System.Collections.Generic;
using System.Text;
using MyDownloader.Core;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
using System.ComponentModel;
using System.Windows.Forms;

namespace DriverDownloader.Core
{
    public interface ISpeedLimitParameters : IExtensionParameters
    {
        bool Enabled { get; set; }

        double MaxRate { get; set; }
    }
    public class SpeedLimitUIExtension : IUIExtension
    {
        #region IUIExtension Members

        public Control[] CreateSettingsView()
        {
            return new Control[] {  };
        }

        public void PersistSettings(Control[] settingsView)
        {
            LimitCfg lmt = (LimitCfg)settingsView[0];

            Settings.Default.MaxRate = lmt.MaxRate;
            Settings.Default.EnabledLimit = lmt.EnableLimit;

            Settings.Default.Save();
        }

        #endregion

        public void ShowSpeedLimitDialog()
        {
            using (SetSpeedLimitDialog sld = new SetSpeedLimitDialog())
            {
                if (sld.ShowDialog() == DialogResult.OK)
                {
                    PersistSettings(new Control[] { sld.limitCfg1 });
                }
            }
        }
    }
    public class SpeedLimitExtension: IExtension, IDisposable
    {
        private const int BalancerUp = 50;
        private const int BalancerDown = -75;

        private double currentWait;
        private bool enabled;
        private double maxLimit;
        private ISpeedLimitParameters parameters;

        #region IExtension Members

        public string Name
        {
            get { return "Speed Limit"; }
        }

        public IUIExtension UIExtension
        {
            get { return new SpeedLimitUIExtension(); }
        }

        #endregion

        #region Properties

        public ISpeedLimitParameters Parameters
        {
            get { return parameters; }
        }

        public bool CurrentEnabled
        {
            get { return enabled; }
        }

        public double CurrentMaxRate
        {
            get { return maxLimit; }
        }

        #endregion

        #region Methods

        public void SetMaxRateTemp(double max)
        {
            this.enabled = true;
            this.maxLimit = max;
        }

        public void RestoreMaxRateFromParameters()
        {
            ReadSettings();
        }

        private void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ReadSettings();
        }

        private void ReadSettings()
        {
            currentWait = 0;
            maxLimit = Parameters.MaxRate;
            enabled = Parameters.Enabled;
        }

        private void ProtocolProviderFactory_ResolvingProtocolProvider(object sender, ResolvingProtocolProviderEventArgs e)
        {
            e.ProtocolProvider = new ProtocolProviderProxy(e.ProtocolProvider, this);
        }

        internal void WaitFor()
        {
            if (enabled)
            {
                double totalRate = DownloadManager.Instance.TotalDownloadRate;

                if (totalRate > maxLimit)
                {
                    currentWait += BalancerUp;
                }
                else
                {
                    currentWait = Math.Max(currentWait + BalancerDown, 0);
                }

                Thread.Sleep(TimeSpan.FromMilliseconds(currentWait));

                Debug.WriteLine("TotalDownloadRate = " + totalRate);
                Debug.WriteLine("maxLimit = " + maxLimit);
                Debug.WriteLine("currentWait = " + currentWait);
            }
        }

        #endregion

        #region Constructor

        public SpeedLimitExtension()
            :
            this(new SpeedLimitParametersSettingProxy())
        {
        }

        public SpeedLimitExtension(ISpeedLimitParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            this.parameters = parameters;

            ReadSettings();

            ProtocolProviderFactory.ResolvingProtocolProvider += new EventHandler<ResolvingProtocolProviderEventArgs>(ProtocolProviderFactory_ResolvingProtocolProvider);
            this.parameters.ParameterChanged += new PropertyChangedEventHandler(Default_PropertyChanged);
        } 

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this.parameters is IDisposable)
            {
                (this.parameters as IDisposable).Dispose();
            }
        }

        #endregion
    }
    internal class SpeedLimitParametersSettingProxy : ISpeedLimitParameters, IDisposable
    {
        #region ISpeedLimitParameters Members

        public bool Enabled
        {
            get
            {
                return Settings.Default.EnabledLimit;
            }
            set
            {
                Settings.Default.EnabledLimit = value;
                OnParameterChanged("Enabled");
            }
        }

        public double MaxRate
        {
            get
            {
                return Settings.Default.MaxRate;
            }
            set
            {
                Settings.Default.MaxRate = value;
                OnParameterChanged("MaxRate");
            }
        }

        #endregion

        #region IExtensionParameters Members

        public event System.ComponentModel.PropertyChangedEventHandler ParameterChanged;

        #endregion

        #region Methods

        protected void OnParameterChanged(string propertyname)
        {
            if (ParameterChanged != null)
            {
                ParameterChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyname));
            }
        }

        void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnParameterChanged(e.PropertyName);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Settings.Default.Save();
        }

        #endregion

        public SpeedLimitParametersSettingProxy()
        {
            Settings.Default.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Default_PropertyChanged);
        }
    }
  
    public class AutoDownloadsParametersSettingsProxy : IAutoDownloadsParameters
    {
        #region IAutoDownloadsParameters Members

        public int MaxJobs
        {
            get
            {
                return Settings.Default.concurrentMaxJobs;
            }
            set
            {
                Settings.Default.concurrentMaxJobs = value;
                OnParameterChanged("MaxJobs");
            }
        }

        public bool WorkOnlyOnSpecifiedTimes
        {
            get
            {
                return Settings.Default.WorkOnlyOnSpecifiedTimes;
            }
            set
            {
                Settings.Default.WorkOnlyOnSpecifiedTimes = value;
                OnParameterChanged("WorkOnlyOnSpecifiedTimes");
            }
        }

        public string TimesToWork
        {
            get
            {
                return Settings.Default.TimesToWork;
            }
            set
            {
                Settings.Default.TimesToWork = value;
                OnParameterChanged("TimesToWork");
            }
        }

        public double MaxRateOnTime
        {
            get
            {
                return Settings.Default.MaxRateOnTime;
            }
            set
            {
                Settings.Default.MaxRateOnTime = value;
                OnParameterChanged("MaxRateOnTime");
            }
        }

        #endregion

        #region IExtensionParameters Members

        public event System.ComponentModel.PropertyChangedEventHandler ParameterChanged;

        #endregion

        #region Methods

        protected void OnParameterChanged(string propertyname)
        {
            if (ParameterChanged != null)
            {
                ParameterChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyname));
            }
        }

        void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnParameterChanged(e.PropertyName);
        }

        #endregion

        public AutoDownloadsParametersSettingsProxy()
        {
            Settings.Default.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Default_PropertyChanged);
        }
    }
}