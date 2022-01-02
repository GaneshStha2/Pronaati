using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class VocabularyAndPronounciationViewModel
    {
        public int Id { get; set; }

        
        public string Word { get; set; }
        public string WordType { get; set; }
        public string FileUrl { get; set; }
        [AllowHtml]
        public string WordMeaning { get; set; }
    }
}