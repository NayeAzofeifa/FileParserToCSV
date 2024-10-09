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
        
        public HeaderRecordModel CreateHeaderRecord(string path, List<CustomerModel> customers)
        {
            int totalCustomers = customers.Count;
            CalculationService calculationService = new CalculationService();
            List<string> amountsPerCustomer = calculationService.CalculateTotalAmountPerCustomer(customers);
            string totalAmount = calculationService.CalculateCustomersTotalAmount(amountsPerCustomer);
            HeaderRecordModel newHeader = new HeaderRecordModel
            {
                SourceFileName = Path.GetFileName(path),
                CustomerCount = totalCustomers,
                CustomersTotalAmount = totalAmount,
                TodaysDate = DateTime.Now.ToString("yyyy-MM-dd"),
                TodaysTimestamp = DateTime.Now.ToString("hh:mm:ss tt"),
            };
            return newHeader;
        }
        public List<DetailsRecordModel> CreateDetailsRecords(List<CustomerModel> customers)
        {
            CultureInfo culture = new CultureInfo("en-US");
            List<DetailsRecordModel> allDetails = new List<DetailsRecordModel>();
            CalculationService calculationService = new CalculationService();
            foreach (CustomerModel customer in customers)
            {
                List<DetailsRecordModel> details = new List<DetailsRecordModel>{
                new DetailsRecordModel {Description = customer.DetailsOne, 
                    Code = calculationService.AssignCodeProduct(customer.AmountOne), 
                    Amount = Decimal.Parse(customer.AmountOne, culture.NumberFormat).ToString("C", culture)},
                new DetailsRecordModel {Description = customer.DetailsTwo, 
                    Code = calculationService.AssignCodeProduct(customer.AmountTwo) ,
                    Amount = Decimal.Parse(customer.AmountTwo, culture.NumberFormat).ToString("C", culture)},
                new DetailsRecordModel {Description = customer.DetailsThree, 
                    Code = calculationService.AssignCodeProduct(customer.AmountThree),
                    Amount = Decimal.Parse(customer.AmountThree, culture.NumberFormat).ToString("C", culture)}
                };
                allDetails.AddRange(details);
            }
            return allDetails;
        }
        
        public void WriteFile(List<CustomerModel> customers, HeaderRecordModel header, List<string> totalAmounts)
        {
            string outputDirectory = @"C:\\Users\\Margot Porras\\source\\repos\\FileParserToCSV\\Outgoing";
            //Path.Combine(Directory.GetCurrentDirectory(), "Outgoing");
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
            foreach (CustomerModel customer in customers)
            {
                
                string customerName = customer.FirstName + customer.LastName;
                string outputFileName = Path.Combine(outputDirectory, $"Customer_{customer.Counter}_{customerName}.csv");

                int index = int.Parse(customer.Counter) - 1;
                string totalAmountPerCustomer = totalAmounts[index];

                using (StreamWriter writer = new StreamWriter(outputFileName))
                {
                    writer.WriteLine($"\"HEADER_RECORD\", \"{header.SourceFileName}\", {header.CustomerCount}, \"{header.CustomersTotalAmount}\", \"{header.TodaysDate}\", \"{header.TodaysTimestamp}\"");
                    writer.WriteLine($"\"CUSTOMER_RECORD\", \"{customer.Counter}\", \"{customer.FirstName}\", \"{customer.LastName}\", \"{customer.Phone}\", \"{customer.AddressOne}\", \"{customer.AddressTwo}\", \"{customer.City}\", \"{customer.State}\", \"{customer.ZipCode}\", \"{customer.Account}\"");
                    
                    List<DetailsRecordModel> details = CreateDetailsRecords(new List<CustomerModel> { customer });
                    foreach (var detail in details)
                    {
                        writer.WriteLine($"\"DETAILS_RECORD\", \"{detail.Description}\", \"{detail.Code}\", \"{detail.Amount}\"");
                    }
                    writer.WriteLine($"\"DETAILS_RECORD\", \"TOTAL\", \"\", \"{totalAmountPerCustomer}\"");
                }
            }
            Console.WriteLine("Files have been successfully created");
        }
    }
}
