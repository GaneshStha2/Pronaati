using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Riddhasoft.Setup.Entity.Course
{
    public class EOnlineTraining
    {

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string Code { get; set; }

        [Required]
        public string PackageTitle { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        [AllowHtml]
        public string Description { get; set; }

        [Required]
        public string ImageURL { get; set; }

        [Required]
        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public int CourseTypeId { get; set; }

        public virtual ELanguageType CourseType { get; set; }
    }
}
