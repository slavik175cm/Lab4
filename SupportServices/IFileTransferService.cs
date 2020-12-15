using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportServices
{
    public interface IFileTransferService
    {
        void TransferFile(string sourceDirectoryPath, string destinationDirectoryPath, string fileName);
    }
}
