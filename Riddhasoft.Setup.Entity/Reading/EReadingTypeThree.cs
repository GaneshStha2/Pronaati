using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Reading
{
    public class EReadingTypeThree
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string QuestionSource1 { get; set; }
        public string QuestionSource2 { get; set; }
        public string QuestionSource3 { get; set; }
        public string QuestionSource4 { get; set; }
        public string QuestionSource5 { get; set; }
        public string QuestionSource6 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsTaken { get; set; }
        

    }
}
