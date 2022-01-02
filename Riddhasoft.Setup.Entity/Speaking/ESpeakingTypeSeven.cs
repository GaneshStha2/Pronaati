using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Speaking
{
    /// <summary>
    /// Student will hear a question and have to give a answer
    /// </summary>
    public class ESpeakingTypeSeven
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string QuestionAudioURl { get; set; }

        public int BeginWithInTimeSec { get; set; }

        public int SpeakingTimeSec { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public int CreatedByUserId { get; set; }
        public bool IsTaken { get; set; }
    }
}
