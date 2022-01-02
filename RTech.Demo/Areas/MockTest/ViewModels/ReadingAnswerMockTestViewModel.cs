using Riddhasoft.Setup.Entity.QuestionSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels
{
    public class ReadingTypeOneAnswerMockTestViewModel
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public QuestionType questionType { get; set; }
        public int StudentId { get; set; }
        public string Answer { get; set; }
        public bool ISCorrectAnswer { get; set; }
    }
    public class ReadingTypeTwoAnswerMockTestViewModel
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }
        public string Answers { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
    }

    public class ReadingTypeThreeAnswerMockTestViewModel
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }
        public string Answers { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
    }

    public class ReadingTypeFourAnswerMockTestViewModel
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }
        public string Answers { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
    }

    public class ReadingTypeFiveAnswerMockTestViewModel
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }
        public string Answers { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
    }
}