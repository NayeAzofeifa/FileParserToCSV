using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace FileParserToCSV
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fileService = new FileService();
            string inputFilePath = @"C:\\Users\\Margot Porras\\source\\repos\\FileParserToCSV\\Incoming\\InputData.txt";
            List<string> inputFileLines = fileService.readFile(inputFilePath);
            foreach (string line in inputFileLines)
            {
                Console.WriteLine(line);
            }
            Console.ReadLine();
        }
    }
}
