using Riddhasoft.Setup.Entity.QuestionSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.MockTest.Entity
{

    /// <summary>
    /// Answers of all types of speaking tests are stored in same table . Question Type is managed using enum
    /// </summary>
    public class ESpeakingAnswer
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public QuestionType QuestionType { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }
        public string  AnswerAuduioUrl { get; set; }

    }

   
}
