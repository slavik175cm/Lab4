using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace MyWatcher
{
    class Logger
    {
        private string logPath;
        private bool enableLogging;

        public Logger(string logPath, bool enableLogging)
        {
            this.logPath = logPath;
            this.enableLogging = enableLogging;

            using (StreamWriter writer = new StreamWriter("d:\\C#_2sem\\log.txt", true))
            {
                writer.WriteLine(enableLogging.ToString());
            }
        }

        public void AddRecord(string message)
        {
            using (StreamWriter writer = new StreamWriter("d:\\C#_2sem\\log.txt", true))
            {
                writer.WriteLine(message);
            }
            if (!enableLogging) return;
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine(message);
            }
        }
    }
}
