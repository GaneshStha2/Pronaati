using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels.ReadingMockTestVm
{
    public class StartReadingTypeOneViewModel
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string TextToRead { get; set; }
        public string Response1 { get; set; }
        public bool Response1_CheckBox { get; set; }

        public string Response2 { get; set; }
        public bool Response2_CheckBox { get; set; }

        public string Response3 { get; set; }
        public bool Response3_CheckBox { get; set; }

        public string Response4 { get; set; }
        public bool Response4_CheckBox { get; set; }

        public string Question { get; set; }
    }

    public class StartReadingTypeTwoViewModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string StudentName { get; set; }
        public string Title { get; set; }
        public string TextToRead { get; set; }
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
        public string Question { get; set; }
    }

    public class StartReadingTypeThreeViewModel
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string QuestionSource1 { get; set; }
        public string QuestionSource2 { get; set; }
        public string QuestionSource3 { get; set; }
        public string QuestionSource4 { get; set; }
        public string QuestionSource5 { get; set; }
        public string QuestionSource6 { get; set; }
    }

    public class StartReadingTypeFourViewModel
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Title { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<string> TextList { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
        public string Option6 { get; set; }
        public string Option7 { get; set; }
        public string Option8 { get; set; }

    }

    public class StartReadingTypeFiveViewModel
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public int QuestionId { get; set; }
        public string Title { get; set; }

        public List<string> QuestionTextList { get; set; }


        public string QuestionText { get; set; }

        public List<OptionsDropDownVm> OptionsDropDowns { get; set; }



    }

    public class OptionsDropDownVm {

        public List<ReadingTypeFiveOptions> Options { get; set; }

        // Is Determined by the index 
        public int IsCorrectAnswer { get; set; }
    }
    public class ReadingTypeFiveOptions {

        public string OptionName { get; set; }

    } 
    
}