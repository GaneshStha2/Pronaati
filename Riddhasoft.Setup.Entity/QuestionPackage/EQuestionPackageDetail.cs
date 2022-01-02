using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.QuestionPackage
{
    public class EQuestionPackageDetail
    {
        public int Id { get; set; }
        public int QuestionPackageMasterId { get; set; }
        public int QuestionSetId { get; set; }
        public int ValidDuration { get; set; }
    }
}
