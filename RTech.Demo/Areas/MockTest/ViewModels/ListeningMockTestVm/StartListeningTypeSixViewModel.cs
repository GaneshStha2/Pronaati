using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels.ListeningMockTestVm
{
    public class StartListeningTypeSixViewModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string AudioUrl { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
        public bool Op1 { get; set; }
        public bool Op2 { get; set; }
        public bool Op3 { get; set; }
        public bool Op4 { get; set; }
        public bool Op5 { get; set; }
        public int BeginWithin { get; set; }
    }
}