using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels
{
    public class MockTestQuestionSetsViewModel
    {
        public int Id { get; set; }
        public int QuestionSetId { get; set; }
        public string QuestionSetTitle { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool isTaken { get; set; }
        public DateTime PurchasedDate { get; set; }
        public bool IsUnscored { get; set; }
    }
}