using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Practice
{
    public class EMasterTopicSentenceBooster
    {
        public int Id { get; set; }
        public string QuestionTitle { get; set; }

        public string Question { get; set; }

        public string OptionStatement { get; set; }


    }
    public class EMasterTopicSentenceBoosterOptionDetails {

        public int Id { get; set; }
        public int MasterTopicSentenceBoosterMasterId { get; set; }
        public string Options { get; set; }
        public bool IsCorrectAnswer { get; set; }

        public virtual EMasterTopicSentenceBooster MasterTopicSentenceBoosterMaster { get; set; }
    }

}
