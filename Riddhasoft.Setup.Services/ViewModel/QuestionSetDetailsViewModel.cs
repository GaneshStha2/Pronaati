using Riddhasoft.Setup.Entity.QuestionSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.ViewModel
{
    public class QuestionSetDetailsViewModel
    {
        public int Id { get; set; }
        public int QuestionSetMasterId { get; set; }
        public int QuestionId { get; set; }
      
        public string QuestionTitle { get; set; }
    }
}
