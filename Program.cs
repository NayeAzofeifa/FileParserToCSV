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
            List<CustomerModel> customers = fileService.ReadCustomersFromFile(inputFilePath);
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Counter} - {customer.FirstName} {customer.LastName}");
            }
            Console.ReadLine();
        }
    }
}
