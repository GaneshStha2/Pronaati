using Riddhasoft.Student.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace RTech.Demo.Areas.Student.ViewModels
{
    public class SignUpViewModel
    {
        public int Id { get; set; }

        public Institute Institute { get; set; }

        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }

        //public string MiddleName { get; set; }

        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string Address { get; set; }

        [Required, Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid email")]
        [Required, DataType(DataType.EmailAddress , ErrorMessage ="Valid Email Address is Requires")]
        public string Email { get; set; }

        public Gender Gender { get; set; }

        [Required]
        public string BirthCountry { get; set; }

        [Required]
        public string Occupation { get; set; }

        //[Required]
        //public string Username { get; set; }

        [Required, StringLength(15,MinimumLength =7)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }
        public bool AgreeTermsAndConditions { get; set; }
    }


}