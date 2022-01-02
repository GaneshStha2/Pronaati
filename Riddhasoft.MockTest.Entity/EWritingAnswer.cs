using Riddhasoft.Setup.Entity.QuestionSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.MockTest.Entity
{
    public class EWritingAnswer
    {
        /// <summary>
        /// answers of both writing type 1 and 2 are stored in same table and are recognized using Question Type Enum
        /// </summary>
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public QuestionType QuestionType { get; set; }
        public int StudentId { get; set; }
        public string AnswerText{ get; set; }
        
    }
}
