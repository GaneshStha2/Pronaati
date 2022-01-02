using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.MockTest.Entity
{
    public class ENaatiMockTestAnswer
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int MockTestId { get; set; }
        public int QuestionSetId { get; set; }
        public int MockTestQuestionMasterId { get; set; }
        public int QuestionDetailId { get; set; }
        public string AnswerAudioUrl { get; set; }
        public TestSource TestSource { get; set; }
        public DateTime AnsweredDate { get; set; }


        public int PackageId { get; set; }
    }

    public enum TestSource
    {
        MockTest,
        Package
    }
}
