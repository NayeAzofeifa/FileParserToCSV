using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileParserToCSV.Models;

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
        public List<HeaderRecordModel> createHeaderRecords(string path)
        {
            List<HeaderRecordModel> headerRecords = new List<HeaderRecordModel>();
            List<CustomerModel> customers = ReadCustomersFromFile(path);
            foreach (var customer in customers)
            {
                HeaderRecordModel newHeader = new HeaderRecordModel
                {
                    SourceFileName = Path.GetFileName(path),
                    CustomerCount = customer.Counter,
                    CustomersTotalAmount = "10",
                    TodaysDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    TodaysTimestamp = DateTime.Now.ToString("hh:mm:ss tt"),
                };
                headerRecords.Add(newHeader);
            }
            return headerRecords;
        }
    }
}
