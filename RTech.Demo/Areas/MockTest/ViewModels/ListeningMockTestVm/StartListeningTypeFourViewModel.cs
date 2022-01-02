using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels.ListeningMockTestVm
{
    public class StartListeningTypeFourViewModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int BeginWithin { get; set; }
        public string Title{ get; set; }
        public string AudioUrl{ get; set; }
        public string Paragraph1{ get; set; }
        public string Paragraph2{ get; set; }
        public string Paragraph3{ get; set; }
        public string Paragraph4{ get; set; }
        public bool Para1 { get; set; }
        public bool Para2 { get; set; }
        public bool Para3 { get; set; }
        public bool Para4 { get; set; }

    }
}