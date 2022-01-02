using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Speaking
{
    /// <summary>
    /// student will be provided a text which they have to read in 40 sec
    /// </summary>
    public class ESpeakingTypeThree
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string TextToRead { get; set; }
        
        public int BeginWithInTimeSec { get; set; }
        public int SpeakingTimeSec { get; set; }
        public bool IsTaken { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByUserId { get; set; }

    }
}
