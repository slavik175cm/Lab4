using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
namespace MyWatcher
{
    class Entity
    {
        public string Path;
        
        public virtual void Move(string targetPath) { }
        public virtual void Compress(string targetPath, int compressionLevel) { }
        public virtual void Decompress() { }
        public virtual void Encrypt(byte[] Key, byte[] IV) { }
        public virtual void Decrypt(byte[] Key, byte[] IV) { }

        static public void Transfer(string filePath, string TargetDirectory, string ArchiveDirectory, int compressionLevel, bool enableArhivation, bool enableEncoding)
        {
            Entity entity = Entity.CreateNewEntity(filePath);
            using (Aes myAes = Aes.Create())
            {
                if (enableEncoding) entity.Encrypt(myAes.Key, myAes.IV);
                entity.Compress(TargetDirectory, compressionLevel);
                Entity compressedEntity = Entity.CreateNewEntity(entity.Path);
                entity.Decompress();
                if (enableEncoding) entity.Decrypt(myAes.Key, myAes.IV);
                if (enableArhivation) FileArchive.AddToArchive(ArchiveDirectory, compressedEntity);
                    else File.Delete(compressedEntity.Path);
            }
        }
        
        static public Entity CreateNewEntity(string path)
        {
            return Directory.Exists(path) ? (Entity)new MyDirectory(path) : new MyFile(path);
        }

        static public bool IsCompressed(string path) 
        {
            return path.EndsWith(".zip") || path.EndsWith(".gz");
        }
        
        public string GetName() 
        {
            int lastPoint = Path.LastIndexOf('\\');
            return Path.Substring(lastPoint, Path.Length - lastPoint);
        }

        public string GetDirectory()
        {
            int lastPoint = Path.LastIndexOf('\\');
            return Path.Substring(0, lastPoint);
        }


    }
}
