using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CodeAdvent24
{
    public static class FileHelper
    {
        public static string GetContents(string fileName)
        {
            var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\" + $"\\Inputs\\{fileName}"));
            return File.ReadAllText(path);
        }
    }
}
