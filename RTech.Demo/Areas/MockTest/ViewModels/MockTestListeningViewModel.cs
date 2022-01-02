using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels
{
    public class MockTestListeningViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int ListeningTypeOneId { get; set; }
        public int ListeningTypeTwoId { get; set; }
        public int ListeningTypeThreeId { get; set; }
        public int ListeningTypeFourId { get; set; }
        public int ListeningTypeFiveId { get; set; }
        public int ListeningTypeSixId { get; set; }
        public int ListeningTypeSevenId { get; set; }
        public int ListeningTypeEightId { get; set; }
        public string ListeningTypeOneAnswer { get; set; }
        public string ListeningTypeTwoOptions { get; set; }
        public string ListeningTypeThreeAnswers { get; set; }
        public int ListeningTypeFourAnswer { get; set; }
        public int ListeningTypeFiveAnswer { get; set; }
        public int ListeningTypeSixAnswer { get; set; }
        public int ListeningTypeSevenAnswer { get; set; }
        public string ListeningTypeEightAnswer { get; set; }
        public string TestDateTime { get; set; }
    }
}