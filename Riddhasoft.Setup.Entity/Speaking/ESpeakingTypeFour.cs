using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Speaking
{
    /// <summary>
    /// Student will hear a sentence and have to repeat exact sentence
    /// </summary>
    public class ESpeakingTypeFour
    {

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string AudioUrl { get; set; }

        public int BeginWithInTimeSec { get; set; }
        public int SpeakingTimeSec { get; set; }
        public bool IsTaken { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public int CreatedByUserId { get; set; }



    }
}
