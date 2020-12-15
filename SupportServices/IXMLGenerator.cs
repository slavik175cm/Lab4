using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportServices
{
    public interface IXMLGenerator
    {
        string Generate<T>(List<T> model, int startId);
    }
}
