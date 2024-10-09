﻿using FileParserToCSV.Models;
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

        public List<string> CalculateTotalAmountCustomer(string path)
        {
            FileService fileService = new FileService();
            List<CustomerModel> customers = fileService.ReadCustomersFromFile(path);
            List<string> totalAmountPerCustomer = new List<string>();
           // CultureInfo culture = new CultureInfo("en-US");

            foreach (CustomerModel customer in customers)
            {
                decimal amountOne = Decimal.Parse(customer.AmountOne);
                decimal amountTwo = Decimal.Parse(customer.AmountTwo);
                decimal amountThree = Decimal.Parse(customer.AmountThree);

                decimal customerTotalAmount = amountOne + amountTwo + amountThree;
                string totalAmount = customerTotalAmount.ToString();
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

        public string AssingCodeProduct()
        {
            return "";
        }

    }
}
