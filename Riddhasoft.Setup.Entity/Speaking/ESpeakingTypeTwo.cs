using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Speaking
{
    /// <summary>
    /// Student will be provided a text which they have to read 
    /// </summary>
    public class ESpeakingTypeTwo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public string TextToRead { get; set; }
        

        public int BeginWithInTimeSec { get; set; }

        public int SpeakingTimeSec { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public int CreatedByUserId { get; set; }
        public bool IsTaken { get; set; }
    }
}
