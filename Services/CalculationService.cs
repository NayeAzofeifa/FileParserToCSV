using FileParserToCSV.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileParserToCSV.Services;
using System.Globalization;
using System.Text.RegularExpressions;

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

        public string CalculateCustomersTotalAmount(List<string> totalAmountPerCustomer)
        {
            decimal finalAmount = 0;
            CultureInfo culture = new CultureInfo("en-US");
            foreach(string amount in totalAmountPerCustomer)
            {
                decimal amountParsed = Decimal.Parse(amount, NumberStyles.Currency, culture);
                finalAmount += amountParsed;
            }
            return finalAmount.ToString("C", culture);
        }

        public string AssignCodeProduct(string amount)
        {
            CultureInfo culture = new CultureInfo("en-US");
            decimal amountToCode = Decimal.Parse(amount, culture.NumberFormat);
            if(amountToCode < 500) { return "N";}
            else if (amountToCode < 1000) { return "A"; }
            else if (amountToCode < 1500) { return "C"; }
            else if (amountToCode < 2000) { return "L"; }
            else if (amountToCode < 2500) { return "P"; }
            else if (amountToCode < 3000) { return "X"; }
            else if (amountToCode < 5000) { return "T"; }
            else if (amountToCode < 10000) { return "S"; }
            else if (amountToCode < 20000) { return "U"; }
            else if (amountToCode < 30000) { return "R"; }
            else { return "V"; }
        }
    }
}
