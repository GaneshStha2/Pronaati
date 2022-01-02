using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.MockTest.Entity
{
    public class ELIsteningTypeOneAnswer
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }
        public string AnswerText { get; set; }
        //public int WordCount { get; set; }
    }

    public class EListeningTypeTwoAnswer
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }

        public string Answers { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
    }

    public class EListeningTypeThreeAnswer
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }

        public string Answers { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
    }

    public class EListeningTypeFourAnswer
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }

        public string Answer { get; set; }
        public bool IScorrectAnswer { get; set; }
    }

    public class EListeningTypeFiveAnswer
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }

        public string Answer { get; set; }
        public bool IScorrectAnswer { get; set; }
    }

    public class EListeningTypeSixAnswer
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }

        public string Answer { get; set; }
        public bool IScorrectAnswer { get; set; }
    }

    public class ELIsteningTYpeSevenAnswer
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }

        public string Answer { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
    }

    public class ELIsteningTYpeEightAnswer
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }

        public string Answer { get; set; }
        public bool IScorrectAnswer { get; set; }
    }
}
