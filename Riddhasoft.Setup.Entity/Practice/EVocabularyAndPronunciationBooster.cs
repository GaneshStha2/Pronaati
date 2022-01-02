using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Riddhasoft.Setup.Entity.Practice
{
    public class EVocabularyAndPronunciationBooster
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string WordType { get; set; }
        public string FileUrl { get; set; }

        [AllowHtml]
        public string WordMeaning { get; set; }


    }
}
