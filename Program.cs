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
            FileService fileService = new FileService();
            CalculationService calculationService = new CalculationService();
            string inputFilePath = @"C:\\Users\\Margot Porras\\source\\repos\\FileParserToCSV\\Incoming\\InputData.txt";
            List<CustomerModel> customers = fileService.ReadCustomersFromFile(inputFilePath);
            HeaderRecordModel headerRecord = fileService.CreateHeaderRecord(inputFilePath, customers);
            List<string> totalAmounts = calculationService.CalculateTotalAmountPerCustomer(customers);
            fileService.WriteFile(customers, headerRecord, totalAmounts);
            Console.ReadLine();
        }
    }
}
