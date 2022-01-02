using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels.WritingMockTestVm
{
    public class MockTestWritingTypeOneViewModel
    {
        public int Id { get; set; }

        public int TotalTime { get; set; }
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int WordCount { get; set; }
    }
}