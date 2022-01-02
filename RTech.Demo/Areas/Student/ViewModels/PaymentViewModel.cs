using Riddhasoft.Setup.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        public PaymentFor PaymentFor { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string CourseName { get; set; }

        public string Language { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public decimal SubTotal { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string NameOnCard { get; set; }

        [Required]
        public int CardExpMonth { get; set; }

        [Required]
        public int CardExpYear { get; set; }

        [Required]
        public string CardSecurityKey { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }
    }
}