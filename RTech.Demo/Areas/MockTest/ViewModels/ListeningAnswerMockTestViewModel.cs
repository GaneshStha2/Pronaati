using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels
{
    public class ListeningTypeOneAnswerMockTestViewModel
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }
        public string AnswerText { get; set; }
    }

    public class ListeningTypeTwoAnswerMockTestViewModel
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }

        public string Answers { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
    }

    public class ListeningTypeThreeAnswerMockTestViewModel {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }

        public string Answers { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
    }

    public class ListeningTypeFourAnswerMockTestViewModel
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }

        public string Answer { get; set; }
        public bool IScorrectAnswer { get; set; }
    }

    public class ListeningTypeFiveAnswerMockTestViewModel
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }

        public string Answer { get; set; }
        public bool IScorrectAnswer { get; set; }
    }

    public class ListeningTypeSixAnswerMockTestViewModel
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }

        public string Answer { get; set; }
        public bool IScorrectAnswer { get; set; }
    }

    public class ListeningTypeSevenAnswerMockTestViewModel
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }

        public string Answer { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
    }

    public class ListeningTypeEightAnswerMockTestViewModel
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