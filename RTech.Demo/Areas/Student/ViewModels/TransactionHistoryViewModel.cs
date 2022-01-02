using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class TransactionHistoryViewModel
    {
        public int Id { get; set; }
        public string PackageTitle { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; }
    }

    public class InvoiceMasterViewModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string InvoiceNumber { get; set; }
        public string CreatedDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
    }

    public class InvoiceDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        
    }


    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }

        //Detail

        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string InvoiceNumber { get; set; }
        public string CreatedDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}