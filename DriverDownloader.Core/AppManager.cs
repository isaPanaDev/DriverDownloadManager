using System;
using System.Collections.Generic;
using System.Text;
using DriverDownloader;
using System.Windows.Forms;

namespace DriverDownloader.Core
{
    public interface IApp : IDisposable
    {
        Form MainForm { get; }

        List<IExtension> Extensions { get; }

        IExtension GetExtensionByType(Type type);

        void Start(string[] args);
    }
    public class AppManager
    {
        private AppManager()
        {
        }

        private static AppManager instance = new AppManager();

        public static AppManager Instance
        {
            get { return instance; }
        }

        private IApp application;

        public IApp Application
        {
            get { return application; }
        }

        public void Initialize(IApp app)
        {
            this.application = app;
        }
    }
}
