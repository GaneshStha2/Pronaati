using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.QuestionSet
{
    public class EQuestionSetMaster
    {
        public int Id { get; set; }
        public string QuestionSetName { get; set; }
        public string QuestionSetCode { get; set; }
        public bool IsUnscored { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int LastModifiedBy { get; set; }
    }
}
