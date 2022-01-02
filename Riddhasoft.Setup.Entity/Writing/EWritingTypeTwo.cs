using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Writing
{
    public class EWritingTypeTwo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public int TotalTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsTaken { get; set; }
    }
}
