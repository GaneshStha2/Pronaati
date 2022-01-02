using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Riddhasoft.Setup.Entity.Package
{
    public class EPackage
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string Code { get; set; }

        [Required]
        public string PackageTitle { get; set; }

        [Required]
        [AllowHtml]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

    }
}
