using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigManager
{
    public interface IParser
    {
        ParsedObject Parse(string text);
    }
}
