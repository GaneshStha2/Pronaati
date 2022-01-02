using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels
{
    public class DialogueVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsTaken { get; set; }
        public string Description { get; set; }
        public string Instruction { get { return ("Complete both dialogues within 20 minutes time"); } }
    }

    public class DialoguesListVm
    {
        public int MockTestId { get; set; }
        public string MocktestTitle { get; set; }
        public string Instruction { get { return ("Complete both dialogues within 20 minutes time"); } }
        public List<DialogueVm> Dialogues { get; set; }
    }

    public class ConversationsListVm
    {
        public int DialogueId { get; set; }
        public string DialogueTitle { get; set; }
        public List<DialogueVm> Converstions { get; set; }
    }

}