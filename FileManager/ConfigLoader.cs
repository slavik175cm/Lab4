using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ConfigManager;
namespace MyWatcher
{
    class ConfigLoader
    {
        public string logMessage;
        ConfigModel configModel;
        ConfigProvider configprovider;

        public ConfigModel GetConfigModel(string configPath)
        {
            configModel = new ConfigModel();
            configprovider = new ConfigProvider();
            logMessage = "";

            try
            {
                TryToFillModel(configPath);
            } catch(Exception e)
            {
                logMessage += e.Message + ". Using default values\n";
            }
            Validate();

            return configModel;
        }

        private void TryToFillModel(string configPath)
        {
            IParser parser;
            if (File.Exists(configPath))
            {
                switch (GetExtension(configPath))
                {
                    case ".xml":
                        parser = new XMLParser();
                        break;
                    case ".json":
                        parser = new JsonParser();
                        break;
                    default:
                        throw new Exception("There is no suitable parser");
                }

                try
                {
                    configprovider.GetFilledModel(configModel, GetStringFromFile(configPath), parser);
                } catch
                {
                    throw new Exception($"File {configPath} can not be parsed");
                }
            }

            if (Directory.Exists(configPath))
            {
                DirectoryInfo configDir = new DirectoryInfo(configPath);
                FileInfo[] jsonFiles = configDir.GetFiles("*.json");
                parser = new JsonParser();
                foreach (var jsonFile in jsonFiles)
                {
                    try
                    {
                        configprovider.GetFilledModel(configModel, GetStringFromFile(jsonFile.FullName), parser);
                        logMessage += $"Used configuration from file: {jsonFile.FullName}";
                        return;
                    } catch { }
                }

                FileInfo[] XMLFiles = configDir.GetFiles("*.xml");
                parser = new XMLParser();
                foreach (var XMLFile in XMLFiles)
                {
                    try
                    {
                        configprovider.GetFilledModel(configModel, GetStringFromFile(XMLFile.FullName), parser);
                        logMessage += $"Used configuration from file: {XMLFile.FullName}";
                        return;
                    } catch { }
                }
                throw new Exception($"There is no suitable file in the directory {configPath}");
            }
        }

        private void Validate()
        {

            if (configModel.archiveOptions.archiveDirectoryPath == null)
            {
                logMessage += "archiveDirectoryPath has not been found. Default value is being used\n";
                configModel.archiveOptions.archiveDirectoryPath = "d:\\C#_2sem\\TargetDirectory\\Archive";
            } else
            if (!Directory.Exists(configModel.archiveOptions.archiveDirectoryPath))
            {
                logMessage += "archiveDirectoryPath is not a directory. Default value is being used\n";
                configModel.archiveOptions.archiveDirectoryPath = "d:\\C#_2sem\\TargetDirectory\\Archive";
            }


            if (configModel.archiveOptions.compressionLevel < 0 || configModel.archiveOptions.compressionLevel > 1)
            {
                logMessage += "compressionLevel is not valid. Default value is being used\n";
                configModel.archiveOptions.compressionLevel = 1;
            }


            if (configModel.watcherOptions.sourseDirectoryPath == null)
            {
                logMessage += "sourseDirectoryPath has not been found. Default value is being used\n";
                configModel.watcherOptions.sourseDirectoryPath = "d:\\C#_2sem\\SourceDirectory";
            } else
            if (!Directory.Exists(configModel.watcherOptions.sourseDirectoryPath))
            {
                logMessage += "sourseDirectoryPath is not a directory. Default value is being used\n";
                configModel.watcherOptions.sourseDirectoryPath = "d:\\C#_2sem\\SourceDirectory";
            }

            if (configModel.watcherOptions.targetDirectoryPath == null)
            {
                logMessage += "targetDirectoryPath has not been found. Default value is being used\n";
                configModel.watcherOptions.targetDirectoryPath = "d:\\C#_2sem\\TargetDirectory";
            } else
            if (!Directory.Exists(configModel.watcherOptions.targetDirectoryPath))
            {
                logMessage += "targetDirectoryPath is not a directory. Default value is being used\n";
                configModel.watcherOptions.sourseDirectoryPath = "d:\\C#_2sem\\TargetDirectory";
            }


            if (configModel.loggerOptions.logFilePath == null)
            {
                logMessage += "LogFilePath has not been found. Default value is being used\n";
                configModel.loggerOptions.logFilePath = "d:\\C#_2sem\\log.txt";
            } else
            if (!File.Exists(configModel.loggerOptions.logFilePath))
            {
                logMessage += "LogFilePath is not a directory. Default value is being used\n";
                configModel.loggerOptions.logFilePath = "d:\\C#_2sem\\log.txt";
            }

        }

        static string GetStringFromFile(string path)
        {
            string response = "";
            using (StreamReader sr = new StreamReader(path))
            {
                response = sr.ReadToEnd();
            }
            return response;
        }

        public string GetExtension(string text)
        {
            int lastPoint = text.LastIndexOf('.');
            if (lastPoint == -1) return "";
            return text.Substring(lastPoint, text.Length - lastPoint);
        }
    }
}
