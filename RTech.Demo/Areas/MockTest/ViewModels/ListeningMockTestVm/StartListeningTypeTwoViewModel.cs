using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels.Listening
{
    public class StartListeningTypeTwoViewModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string AudioUrl { get; set; }
        public int BeginWithin { get; set; }
        public int TotalTime { get; set; }
        public string QuestionText { get; set; }
        public string Response1 { get; set; }
        public string Response2 { get; set; }
        public string Response3 { get; set; }
        public string Response4 { get; set; }
        public string Response5 { get; set; }
        public string Response6 { get; set; }
        public string Response7 { get; set; }
        public bool Response1_checkBox { get; set; }
        public bool Response2_checkBox { get; set; }
        public bool Response3_checkBox { get; set; }
        public bool Response4_checkBox { get; set; }
        public bool Response5_checkBox { get; set; }
        public bool Response6_checkBox { get; set; }
        public bool Response7_checkBox { get; set; }
    }
}