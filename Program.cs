using System;
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
            /*foreach (var customer in customers)
          {
              Console.WriteLine($"{customer.Counter} - {customer.FirstName} {customer.LastName}");
          }*/

            /*
            List<HeaderRecordModel> records = fileService.CreateHeaderRecords(inputFilePath);

            foreach (HeaderRecordModel record in records)
            {
                Console.WriteLine($"{record.CustomerCount} {record.CustomersTotalAmount} - {record.SourceFileName} {record.TodaysDate} {record.TodaysTimestamp}");
            }*/

            
            List<DetailsRecordModel> detailRecords = fileService.CreateDetailsRecords(customers);

            foreach (DetailsRecordModel detail in detailRecords)
            {
                Console.WriteLine($"{detail.Description} {detail.Code} {detail.Amount}");
            }
            
            /*
            CultureInfo culture = new CultureInfo("en-US");
            string amount = "3942.61";
            decimal amountToCode = Decimal.Parse(amount, culture.NumberFormat);
            Console.WriteLine($"{0} - {amount}");
            Console.WriteLine($"{1} - {amountToCode}");*/
            /*
            List<string> amounts = calculationService.CalculateTotalAmountPerCustomer(inputFilePath);
            string total = calculationService.CalculateCustomersTotalAmount(amounts);
            Console.WriteLine($"{1} - {total}");
            
            foreach (string amount in amounts)
            {
                Console.WriteLine($"{0} - {amount}");
            }*/


            Console.ReadLine();
        }
    }
}
