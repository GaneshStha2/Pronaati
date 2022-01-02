using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Speaking
{

    /// <summary>
    /// Student have to describe the picture of map shown
    /// </summary>
    public class ESpeakingTypeFive
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }

        public string NameOfImage { get; set; }
        public int BeginWithInTimeSec { get; set; }

        public int SpeakingTimeSec { get; set; }
        public bool IsTaken { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public int CreatedByUserId { get; set; }

    }
}
