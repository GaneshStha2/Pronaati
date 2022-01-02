using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Course
{
    public class ESegment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public int FromLanguageId { get; set; }
        public int ToLanguageId { get; set; }

        public virtual ELanguageType Language{ get; set; }
    }
}
