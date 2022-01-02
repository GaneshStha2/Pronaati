using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Speaking
{
    /// <summary>
    /// In Type six their will be an lecture image and Lecture Audio 
    /// Student will Listen to audio and give response to the lecture through speaking 
    /// </summary>
    public class ESpeakingTypeSix
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public string AudioUrl { get; set; }


        public int BeginWithInTimeSec { get; set; }
        public int SpeakingTimeSec { get; set; }
        public bool IsTaken { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public int CreatedByUserId { get; set; }

    }
}
