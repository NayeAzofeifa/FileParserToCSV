using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileParserToCSV.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FileParserToCSV.Services
{
    public class FileService
    {
        public List<CustomerModel> ReadCustomersFromFile(string path)
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            try
            {
                List<string> fileLines = File.ReadAllLines(path).ToList();
                fileLines = fileLines.Skip(1).ToList();
                foreach (string line in fileLines)
                {
                    string[] entries = line.Split('|');
                    if (entries.Length > 16)
                    {
                        Console.WriteLine("Error in line: line has fewer characters");
                        continue;
                    }
                    CustomerModel newCustomer = new CustomerModel
                    {
                        Counter = entries[0],
                        FirstName = entries[1],
                        LastName = entries[2],
                        Phone = entries[3],
                        AddressOne = entries[4],
                        AddressTwo = entries[5],
                        City = entries[6],
                        State = entries[7],
                        ZipCode = entries[8],
                        Account = entries[9],
                        DetailsOne = entries[10],
                        AmountOne = entries[11],
                        DetailsTwo = entries[12],
                        AmountTwo = entries[13],
                        DetailsThree = entries[14],
                        AmountThree = entries[15],
                    };
                    customers.Add(newCustomer);
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while reading the file");
            }
            return customers;
        }
        
        public List<HeaderRecordModel> CreateHeaderRecords(string path)
        {
            List<HeaderRecordModel> headerRecords = new List<HeaderRecordModel>();
            List<CustomerModel> customers = ReadCustomersFromFile(path);
            CalculationService calculationService = new CalculationService();
            List<string> amounts = calculationService.CalculateTotalAmountPerCustomer(path);
            foreach (var customer in customers)
            {
                HeaderRecordModel newHeader = new HeaderRecordModel
                {
                    SourceFileName = Path.GetFileName(path),
                    CustomerCount = customer.Counter,
                    CustomersTotalAmount = calculationService.CalculateCustomersTotalAmount(amounts),
                    TodaysDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    TodaysTimestamp = DateTime.Now.ToString("hh:mm:ss tt"),
                };
                headerRecords.Add(newHeader);
            }
            return headerRecords;
        }
        public List<DetailsRecordModel> CreateDetailsRecords(List<CustomerModel> customers)
        {
            CultureInfo culture = new CultureInfo("en-US");
            List<DetailsRecordModel> allDetails = new List<DetailsRecordModel>();
            foreach (CustomerModel customer in customers)
            {
                List<DetailsRecordModel> details = new List<DetailsRecordModel>{
                new DetailsRecordModel {Description = customer.DetailsOne, Code = "A" , 
                    Amount = Decimal.Parse(customer.AmountOne, culture.NumberFormat).ToString("C", culture)},
                new DetailsRecordModel {Description = customer.DetailsTwo, Code = "B" ,
                    Amount = Decimal.Parse(customer.AmountTwo, culture.NumberFormat).ToString("C", culture)},
                new DetailsRecordModel {Description = customer.DetailsThree, Code = "C" ,
                    Amount = Decimal.Parse(customer.AmountThree, culture.NumberFormat).ToString("C", culture)}
                };

                allDetails.AddRange(details);
            }
            return allDetails;
        }

    }
}
