using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Riddhasoft.Setup.Entity.Speaking
{

    /// <summary>
    /// Student will be provided a set up of question where they have 
    /// Could talk about one or more questions 
    /// </summary>
    public class ESpeakingTypeOne
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        [AllowHtml]
        public string Question { get; set; }

        
        public int BeginWithinTImeSec { get; set; }

        public int SpeakingTimeSec { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByUserId { get; set; }
        public bool IsTaken { get; set; }
    }


}
