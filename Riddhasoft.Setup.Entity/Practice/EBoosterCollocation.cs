using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Practice
{
    public class EBoosterCollocation
    {
        [Key]
        public int Id { get; set; }
        public string Question { get; set; }
        public string QuestionText { get; set; }
        public string OptionStatement { get; set; }
        
    }

    public class EBoosterCollocationDetails
    {
        public int Id { get; set; }
        public int EBoosterCollocationMasterId { get; set; }
        public string Options { get; set; }
        public bool IsAnswer { get; set; }
        public virtual EBoosterCollocation EBoosterCollocationMaster { get; set; }
    }
}
