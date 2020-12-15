using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigManager;
using ServiceLayer;
using SupportServices;
namespace DataManager
{
    class DataManager
    {
        static void Main()
        {
            FileUtils fileUtils = new FileUtils();
            IParser XmlParser = new XMLParser();
            ServiceLayer.ServiceLayer sl = new ServiceLayer.ServiceLayer();
            IXMLGenerator xmlGenerator = new XMLGeneratorService();
            ConfigProvider configurationManager = new ConfigProvider();
            Options.DataManagerOptions dataManagerOptions = new Options.DataManagerOptions();

            configurationManager.GetFilledModel(dataManagerOptions, fileUtils.GetStringFromFile("d:\\C#_2sem\\Project2\\DataManager\\DataManagerOptions.XML"), XmlParser);
            
            string connectionString = dataManagerOptions.DataBaseOptions.BuildConnectionString();
            int batchSize = dataManagerOptions.SendingOptions.BatchSize;
            string destinationDirectory = dataManagerOptions.SendingOptions.DestinationDirectory;
            string tempFileDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            int batchIterator = 0;
            while(true)
            {
                batchIterator++;
                var models = sl.GetManyPersonFullInfos(connectionString, batchIterator, batchSize);
                if (models.Count == 0)
                    break;
                
                // to XMLsss
                string fileName = "Batch" + batchIterator.ToString();
                fileUtils.CreateTextFile(Path.Combine(tempFileDirectory, fileName));
                fileUtils.WriteToTextFile(Path.Combine(tempFileDirectory, fileName), 
                    xmlGenerator.Generate<ServiceLayer.Models.FullPersonInfo>(models, (batchIterator - 1) * batchSize + 1));

                // Send to FTP
                fileUtils.TransferFile(tempFileDirectory, destinationDirectory, fileName);
            }
            Console.ReadLine();
        }

    }
}
