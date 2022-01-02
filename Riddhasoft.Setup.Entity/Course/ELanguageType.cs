using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Course
{
    public class ELanguageType
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string Code { get; set; }
        [StringLength(200)]
        public string Name { get; set; }

    }
}
