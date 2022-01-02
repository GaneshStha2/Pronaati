using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.Course
{
    public class SOnllineTrainingDetails : Riddhasoft.Services.Common.IBaseService<EOnlineTrainingDetails>
    {
        RiddhaDBContext db = null;
        public SOnllineTrainingDetails()
        {
            db = new RiddhaDBContext();
        }

        public ServiceResult<EOnlineTrainingDetails> Add(EOnlineTrainingDetails model)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<List<EOnlineTrainingDetails>> AddOnlineTrainingDetails(List<EOnlineTrainingDetails> details)
        {
            db.OnlineTrainingDetails.AddRange(details);
            db.SaveChanges();
            return new ServiceResult<List<EOnlineTrainingDetails>>()
            {
                Data = details,
                Status = ResultStatus.Ok,
                Message = "Added Successfully"
            };
        }

        public ServiceResult<IQueryable<EOnlineTrainingDetails>> List()
        {
            return new ServiceResult<IQueryable<EOnlineTrainingDetails>>()
            {
                Data = db.OnlineTrainingDetails,
                Status = ResultStatus.Ok,
                Message = ""
            };
        }

        public ServiceResult<int> Remove(EOnlineTrainingDetails model)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<int> RemoveOnlineTrainingDetails(List<EOnlineTrainingDetails> details)
        {
            db.OnlineTrainingDetails.RemoveRange(details);
            db.SaveChanges();
            return new ServiceResult<int>
            {
                Data = 1,
                Status = ResultStatus.Ok,
                Message = "Deleted Successfully"
            };
        }

        public ServiceResult<EOnlineTrainingDetails> Update(EOnlineTrainingDetails model)
        {
            throw new NotImplementedException();
        }
    }
}
