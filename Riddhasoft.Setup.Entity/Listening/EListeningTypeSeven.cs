using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Listening
{
    public class EListeningTypeSeven
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AudioUrl { get; set; }
        public int TimeBeforeAudio { get; set; }
        public string TranscriptionText { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public  string Answers { get; set; }
        public bool IsTaken { get; set; }
    }
}
