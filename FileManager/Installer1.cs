using System.ComponentModel;
using System.ServiceProcess;
using System;
using System.IO;
using System.Configuration.Install;

namespace MyWatcher
{
    [RunInstaller(true)]
    public partial class Installer1 : Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;

        public Installer1()
        {
            InitializeComponent();
            using (StreamWriter writer = new StreamWriter("D:\\templog.txt", true))
            {
                writer.WriteLine(String.Format("{0} Installer1!!",
                DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")));
            }
            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "Service1";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}