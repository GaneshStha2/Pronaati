using Riddhasoft.Student.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.User
{
    public class UserSetupVm
    {
        public int Id { get; set; }
        public Institute Institute { get; set; }
        public string InstituteName { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string GenderName { get; set; }
        public string BirthCountry { get; set; }
        public string Occupation { get; set; }
        public string OccupationName { get; set; }
        public string IsDeleted { get; set; }
        public string IsActive { get; set; }
    }
}