using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace SupportServices
{
    public class FileUtils : IFileTransferService
    {
        public void TransferFile(string sourceDirectoryPath, string destinationDirectoryPath, string fileName)
        {
            string destinationFilePath = Path.Combine(destinationDirectoryPath, fileName);
            Console.WriteLine(Path.Combine(sourceDirectoryPath, fileName));
            Console.WriteLine(destinationFilePath);
            if (File.Exists(destinationFilePath))
            {
                File.Delete(destinationFilePath);
                return;
            }
            File.Move(Path.Combine(sourceDirectoryPath, fileName), destinationFilePath);
        }

        public string GetStringFromFile(string path)
        {
            string response = "";
            using (StreamReader sr = new StreamReader(path))
            {
                response = sr.ReadToEnd();
            }
            return response;
        }

        public void CreateTextFile(string path)
        {
            File.Create(path);
        }

        public void WriteToTextFile(string path, string text)
        {
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                sw.Write(text);
            }
        }
    }
}
