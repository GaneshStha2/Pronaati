using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Student.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services
{
    public class SMockTestReport
    {
        RiddhaDBContext db = null;
        public SMockTestReport()
        {
            db = new RiddhaDBContext();
        }

        public ServiceResult<IQueryable<EMockTestReport>> List()
        {
            return new ServiceResult<IQueryable<EMockTestReport>>()
            {
                Data = db.MockTestReport,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public string getStudentName(int studentId)
        {
            var data = db.StudentInformation.Where(x => x.Id == studentId).FirstOrDefault();
            if (data != null)
            {
                return data.FirstName.Trim() + " "  + data.LastName.Trim();
            }
            return "";
        }

        public string getQuestionSetName(int questionSetId)
        {
            var data = db.QuestionSetMaster.Where(x => x.Id == questionSetId).FirstOrDefault();
            if (data != null)
            {
                return data.QuestionSetName;
            }
            return "";
        }
    }
}
