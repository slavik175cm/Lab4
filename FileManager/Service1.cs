using System;
using System.ServiceProcess;
using System.IO;
using System.Threading;

namespace MyWatcher
{
    public partial class Service1 : ServiceBase
    {

        Watcher watcher;
        public Service1()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            watcher = new Watcher();
            Thread loggerThread = new Thread(new ThreadStart(watcher.Start));
            loggerThread.Start();
        }

        protected override void OnStop()
        {
            watcher.Stop();
            Thread.Sleep(1000);
        }
    }
}