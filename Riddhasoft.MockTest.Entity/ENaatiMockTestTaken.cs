using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.MockTest.Entity
{
    public class ENaatiMockTestTaken
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int MockTestId { get; set; }

        public int? PackageId { get; set; }
        public bool IsTaken { get; set; }
        public bool IsScored { get; set; }
        public DateTime TestTakenDate { get; set; }
    }
}
