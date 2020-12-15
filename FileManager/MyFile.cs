using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.IO.Compression;
namespace MyWatcher
{
    class MyFile : Entity
    {
        string initialExtension;
        public MyFile(string path)
        {
            Path = path;
        }

        public override void Move(string targetPath)
        { 
            File.Move(Path, targetPath);
            Path = targetPath;
        }

        public override void Compress(string targetPath, int compressionLevel) 
        {
            initialExtension = GetExtension();

            string fileName = GetName();
            string compressedfileName;
            if (initialExtension.Length != 0)
                compressedfileName = fileName.Remove(fileName.Length - initialExtension.Length, initialExtension.Length) + ".gz";
            else compressedfileName = fileName + ".gz";

            string compressedPath = targetPath + compressedfileName;
            Archiver.CompressFile(Path, compressedPath, compressionLevel);
            Path = compressedPath;
        }

        public override void Decompress()
        {
            string fileName = GetName();
            int lastPoint = fileName.LastIndexOf('.');
            fileName = fileName.Remove(lastPoint, fileName.Length - lastPoint) + initialExtension;
            string decompressedPath = GetDirectory() + fileName;
            
            Archiver.DecompressFile(Path, decompressedPath);
            Path = decompressedPath;
        }

        public override void Encrypt(byte[] Key, byte[] IV)
        {
            string text = ReadFromFile();
            byte[] encrypted = Encrypter.EncryptStringToBytes_Aes(text, Key, IV);
            WriteToFile(Encrypter.ByteArrayToString(encrypted));
        }

        public override void Decrypt(byte[] Key, byte[] IV)
        {
            byte[] encryptedText = Encrypter.StringToByteArray(ReadFromFile());
            string DecryptedText = Encrypter.DecryptStringFromBytes_Aes(encryptedText, Key, IV);
            WriteToFile(DecryptedText);
        }

        public string ReadFromFile()
        {
            string text;
            using (StreamReader sr = new StreamReader(Path, Encoding.UTF8))
            {
                text = sr.ReadToEnd();
            }
            return text;
        }

        public void WriteToFile(string text)
        {
            using (StreamWriter sw = new StreamWriter(Path, false, System.Text.Encoding.UTF8))
            {
                sw.Write(text);
            }
        }

        public string GetExtension()
        {
            int lastPoint = Path.LastIndexOf('.');
            if (lastPoint == -1) return "";
            return Path.Substring(lastPoint, Path.Length - lastPoint);
        }

    }
}
