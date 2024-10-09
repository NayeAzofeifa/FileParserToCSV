using FileParserToCSV.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileParserToCSV.Services;
using System.Globalization;

namespace FileParserToCSV.Services
{
    public class CalculationService
    {
        public List<string> CalculateTotalAmountPerCustomer(string path)
        {
            FileService fileService = new FileService();
            List<CustomerModel> customers = fileService.ReadCustomersFromFile(path);
            List<string> totalAmountPerCustomer = new List<string>();
            CultureInfo culture = new CultureInfo("en-US");

            foreach (CustomerModel customer in customers)
            {
                decimal amountOne = Decimal.Parse(customer.AmountOne, culture.NumberFormat);
                decimal amountTwo = Decimal.Parse(customer.AmountTwo, culture.NumberFormat);
                decimal amountThree = Decimal.Parse(customer.AmountThree, culture.NumberFormat);

                decimal customerTotalAmount = amountOne + amountTwo + amountThree;
                string totalAmount = customerTotalAmount.ToString("C", culture);
                totalAmountPerCustomer.Add(totalAmount);
            }
            return totalAmountPerCustomer;
        }
        
        public string CalculateCustomersTotalAmount()
        {
            return "";
        }

        public string AssingCodeProduct()
        {
            return "";
        }


        /*
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
         */
    }
}
