using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Practice
{
    public class VocabularyAndPronunciationBoosterVm
    {

        public int Id { get; set; }
        public string Word { get; set; }
        public string WordType { get; set; }
        public string FileUrl { get; set; }
        public string WordMeaning { get; set; }
    }
}