using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.IO;
using System.Threading;
using System.Security.Cryptography;

namespace MyWatcher
{
    class Watcher
    {
        private string configPath = @"d:\C#_2sem\lab3\lab3";
        private string sourceDirectory;
        private string targetDirectory;
        private string archiveDirectory;
        private string logPath;
        private bool enableArhivation;
        private bool enableEncoding;
        private bool enableLogging;
        private int compressionLevel;

        private FileSystemWatcher watcher;
        private Logger logger;
        public bool enabled = true;

        public Watcher()
        {
            ConfigLoader configLoader = new ConfigLoader();
            ConfigModel configModel = configLoader.GetConfigModel(@"d:\C#_2sem\lab3\lab3");

            sourceDirectory = configModel.watcherOptions.sourseDirectoryPath;
            targetDirectory = configModel.watcherOptions.targetDirectoryPath;
            archiveDirectory = configModel.archiveOptions.archiveDirectoryPath;
            logPath = configModel.loggerOptions.logFilePath;
            enableArhivation = configModel.archiveOptions.enableArchivation;
            enableEncoding = configModel.encodingOptions.enableEncoding;
            enableLogging = configModel.loggerOptions.enableLogging;
            compressionLevel = configModel.archiveOptions.compressionLevel;

            logger = new Logger(logPath, enableLogging);
            logger.AddRecord(configLoader.logMessage);

            watcher = new FileSystemWatcher(sourceDirectory);
            watcher.Created += Watcher_Created;
            watcher.Deleted += Watcher_Deleted;
        }

        public void Start()
        {
            logger.AddRecord($"запущен:  {DateTime.Now.ToString("dd / MM / yyyy hh: mm:ss")}");

            watcher.EnableRaisingEvents = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            logger.AddRecord($"закрыт:  {DateTime.Now.ToString("dd / MM / yyyy hh: mm:ss")}");

            watcher.EnableRaisingEvents = false;
            enabled = false;
        }
        
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string filePath = e.FullPath;
            string fileEvent = "создан";
            RecordEntry(fileEvent, filePath);

            if (Entity.IsCompressed(filePath)) return;
            Entity.Transfer(filePath, targetDirectory, archiveDirectory, compressionLevel, enableArhivation, enableEncoding);
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "удален";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            logger.AddRecord($"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")} файл {filePath} был {fileEvent}");
        }

    }
}
