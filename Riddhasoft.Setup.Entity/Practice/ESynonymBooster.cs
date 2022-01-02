using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Practice
{
    public class ESynonymBooster
    {
        [Key]
        public int Id { get; set; }
        public string Word { get; set; }    
        public string WordType { get; set; }

        public string Question { get; set; }

    }

    public class ESynonymBoosterOptionDetails
    {

        public int Id { get; set; }
        public int SynonymBoosterMasterId { get; set; }
        
        public string Options { get; set; }
        public bool IsAnswer { get; set; }
        public virtual ESynonymBooster SynonymBoosterMaster { get; set; }
    }
}
