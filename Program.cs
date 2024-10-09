using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using FileParserToCSV.Models;
using FileParserToCSV.Services;

namespace FileParserToCSV
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string baseDirectory = AppContext.BaseDirectory;
            DirectoryInfo projectDirectory = Directory.GetParent(baseDirectory)?.Parent?.Parent?.Parent;
            string inputFilePath = Path.Combine(projectDirectory.FullName, "Incoming", "InputData.txt");

            FileService fileService = new FileService();
            CalculationService calculationService = new CalculationService();
            
            List<CustomerModel> customers = fileService.ReadCustomersFromFile(inputFilePath);
            HeaderRecordModel headerRecord = fileService.CreateHeaderRecord(inputFilePath, customers);
            List<string> totalAmounts = calculationService.CalculateTotalAmountPerCustomer(customers);
            
            fileService.WriteFile(customers, headerRecord, totalAmounts);


            string incomingPath = Path.Combine(projectDirectory.FullName, "Incoming");
            fileService.BackupFiles(incomingPath);

            string outgoingPath = Path.Combine(projectDirectory.FullName, "Outgoing");
            fileService.BackupFiles(outgoingPath);

            Console.ReadLine();
        }
    }
}
