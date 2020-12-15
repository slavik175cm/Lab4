using System;
using System.Collections.Generic;
using System.Text;

namespace MyWatcher
{
    public class ConfigModel
    {
        public ArchiveOptions archiveOptions = new ArchiveOptions();
        public EncodingOptions encodingOptions = new EncodingOptions();
        public WatcherOptions watcherOptions = new WatcherOptions();
        public LoggerOptions loggerOptions = new LoggerOptions();

        public class LoggerOptions
        {
            public bool enableLogging;
            public string logFilePath = null;
        }

        public class ArchiveOptions
        {
            public bool enableArchivation;
            public string archiveDirectoryPath = null;
            public int compressionLevel;
        }

        public class WatcherOptions
        {
            public string sourseDirectoryPath = null;
            public string targetDirectoryPath = null;
        }

        public class EncodingOptions
        {
            public bool enableEncoding;
        }
    }
}
