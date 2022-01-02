using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Reading
{
    public class EReadingTypeFive
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string QuestionText { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsTaken { get; set; }

    }

    public class EReadingTypeFiveDropdown {
        public int Id { get; set; }
        public int ReadingTypeFiveId { get; set; }
        /// <summary>
        /// "apple,mango,orange"
        /// </summary>
        public string Options { get; set; }
        /// <summary>
        /// 0
        /// </summary>
        public int CorrectIndex { get; set; }

    }
}
