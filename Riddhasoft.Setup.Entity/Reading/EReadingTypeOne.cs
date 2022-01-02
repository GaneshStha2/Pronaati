using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Reading
{
    public class EReadingTypeOne
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ReadingText { get; set; }
        public string Question { get; set; }
        public string Response1 { get; set; }
        public string Response2 { get; set; }
        public string Response3 { get; set; }
        public string Response4 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsTaken { get; set; }
        public string IsCorrectAnswer { get; set; }
    }
}
