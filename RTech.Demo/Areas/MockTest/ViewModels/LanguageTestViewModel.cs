using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels
{
    public class LanguageTestViewModel
    {
        public int Id { get; set; }
        
        public string Description { get; set; }
        public string LanguageType { get; set; }
        public int QuestionSetId { get; set; }
        public int Order { get; set; }
        public string QuestionAudioLink { get; set; }
        public string RecordedAudioLink { get; set; }
        public string CorrectAnswerAudioLink { get; set; }
        public string DialogueTitle { get; set; }
        public string DialogueDescription { get; set; }
        public int ScreenCount { get; set; }

        public int PackageId { get; set; }

        public string TestInstruction1 { get { return ("Step 1: Click to listen to dialogue – the player button on the top"); }}
        public string TestInstruction2 { get { return ("Step 2: After listening to the dialogue, click “Record” button to record your answer."); } }
        public string TestInstruction3 { get { return ("Step 3: After you complete your recording, click “Stop Recording” button and click “Next” to move to the next dialogue."); } }
    }
}