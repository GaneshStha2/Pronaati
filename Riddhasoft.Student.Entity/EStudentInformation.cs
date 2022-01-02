
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Student.Entity
{
    public class EStudentInformation
    {
        public int Id { get; set; }
        public Institute Institute { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string BirthCountry { get; set; }
        public string Occupation { get; set; }
        public string Username { get; set; }
        public string PhotoUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        public string Code { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum Occupation
    {
        Student,
        Teacher
    }

    public enum Institute
    {
        Institute1 = 1,
        Institute2 = 2
    }
}
