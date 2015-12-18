using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;

namespace iSlack
{
    public partial class App : Application
    {
        private TaskbarIcon notifyIcon;
        public string ConfigFile
        {
            get
            {
                var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var dir      = Path.GetDirectoryName(location);
                var baseName = Path.GetFileNameWithoutExtension(location);
                return Path.Combine(dir, baseName + ".ini");
            }
        }

        public iSlackConfig Config { get; set; }

        private void LoadConfiguration()
        {
            Config = Configuration.Load(ConfigFile);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.LoadConfiguration();
            notifyIcon = (TaskbarIcon)FindResource("iSlackNotifyIcon");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose();
            base.OnExit(e);
        }
    }
}
