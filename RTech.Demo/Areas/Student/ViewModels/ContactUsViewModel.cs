using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class ContactUsViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Message { get; set; }
        public string Phone { get; set; }

        public string CompanyEmail { get; set; }
        public string CompanyLocation { get; set; }
        public string CompanyContact { get; set; }
    }
}