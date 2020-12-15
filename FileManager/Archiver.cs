using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
namespace MyWatcher
{
    class Archiver
    {
        public static void CompressFile(string sourceFile, string compressedFile, int compressionLevel)
        {
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(compressedFile))
                {
                    using (GZipStream compressionStream = new GZipStream(targetStream, (CompressionLevel)compressionLevel))
                    {
                        sourceStream.CopyTo(compressionStream); 
                    }
                }
            }
        }

        public static void DecompressFile(string compressedFile, string targetFile)
        {
            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(targetFile))
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                    }
                }
            }
        }

        public static void CompressDirectory(string sourceFolder, string zipFile, int compressionLevel)
        {
            if (File.Exists(zipFile))
            {
                File.Delete(zipFile);
            }
            ZipFile.CreateFromDirectory(sourceFolder, zipFile, (CompressionLevel)compressionLevel, true);
        }

        public static void DecompressDirectory(string zipFile, string targetFolder)
        {
            if (Directory.Exists(targetFolder))
            {
                Directory.Delete(targetFolder, true);
            }
            ZipFile.ExtractToDirectory(zipFile, targetFolder);
        }
    }
}
