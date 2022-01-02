using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity
{
    public class ECompany
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100), Required]
        public string Name { get; set; }
        [StringLength(100)]
        public string NameNp { get; set; }
        [StringLength(10)]
        public string Code { get; set; }
        [StringLength(150), Required]
        public string Address { get; set; }
        [StringLength(150)]
        public string AddressNp { get; set; }
        [StringLength(50), Required]
        public string ContactNo { get; set; }
        [StringLength(100), Required]
        public string ContactPerson { get; set; }
        [StringLength(250)]
        public string Email { get; set; }
        [StringLength(250)]
        public string WebUrl { get; set; }
        [StringLength(25)]
        public string PAN { get; set; }
        public bool IsDeleted { get; set; }
        public string LogoUrl { get; set; }
    }
    public class ECompanyLicense
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int LicensePeriodInDays { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public virtual ECompany Company { get; set; }
    }
    public class ECompanyLicenseLog
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public DateTime IssueDate { get; set; }
        public int LicensePeriodInDays { get; set; }
        public virtual ECompany Company { get; set; }

    }
}
