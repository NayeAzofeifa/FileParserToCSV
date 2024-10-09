using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileParserToCSV
{
    public class FileService
    {
        public List<string> readFile(string path)
        {
            List<string> fileLines = File.ReadAllLines(path).ToList();
            return fileLines;
        }


    }
}
