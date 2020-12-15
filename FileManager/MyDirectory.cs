using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace MyWatcher
{
    class MyDirectory : Entity
    {
        public MyDirectory(string path)
        {
            Path = path;
        }

        public override void Move(string targetPath)
        {
            Directory.Move(Path, targetPath);
            Path = targetPath;
        }

        public override void Compress(string targetPath, int compressionLevel) 
        {
            string compressedPath = targetPath + GetName() + ".zip";
            Archiver.CompressDirectory(Path, compressedPath, compressionLevel);
            Path = compressedPath;
        }

        public override void Decompress() 
        {
            string DirectoryName = GetName();
            string extension = ".zip";
            string compressedPath = Path.Remove(Path.Length - DirectoryName.Length, DirectoryName.Length) + 
                                    DirectoryName.Remove(DirectoryName.Length - extension.Length, extension.Length);
            Archiver.DecompressDirectory(Path, compressedPath);
            Path = compressedPath;
        }

        public override void Encrypt(byte[] Key, byte[] IV) 
        {
            DirectoryInfo currentDir = new DirectoryInfo(Path);
            FileSystemInfo[] fileInfos = currentDir.GetFileSystemInfos();
            foreach (FileSystemInfo fileInfo in fileInfos)
            {
                CreateNewEntity(fileInfo.FullName).Encrypt(Key, IV);
            }
        }

        public override void Decrypt(byte[] Key, byte[] IV) 
        {
            DirectoryInfo currentDir = new DirectoryInfo(Path);
            FileSystemInfo[] fileInfos = currentDir.GetFileSystemInfos();
            foreach (FileSystemInfo fileInfo in fileInfos)
            {
                CreateNewEntity(fileInfo.FullName).Decrypt(Key, IV);
            }
        }
    }
}
