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
    public class SOnlineClassRoomCoursePracticeDetails : IBaseService<EOnlineClassRoomCoursePracticeDetails>
    {
        RiddhaDBContext db = null;
        public SOnlineClassRoomCoursePracticeDetails()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EOnlineClassRoomCoursePracticeDetails> Add(EOnlineClassRoomCoursePracticeDetails model)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<List<EOnlineClassRoomCoursePracticeDetails>> AddOnlineClassroomPracticeDetails(List<EOnlineClassRoomCoursePracticeDetails> details)
        {
            db.EOnlineClassRoomCoursePracticeDetails.AddRange(details);
            db.SaveChanges();
            return new ServiceResult<List<EOnlineClassRoomCoursePracticeDetails>>()
            {
                Data = details,
                Status = ResultStatus.Ok,
                Message = "Added Successfully !"
            };
        }

        public ServiceResult<IQueryable<EOnlineClassRoomCoursePracticeDetails>> List()
        {
            return new ServiceResult<IQueryable<EOnlineClassRoomCoursePracticeDetails>>()
            {
                Data = db.EOnlineClassRoomCoursePracticeDetails,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EOnlineClassRoomCoursePracticeDetails model)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<int> RemoveOnlineClassroomCoursePracticeDetails(List<EOnlineClassRoomCoursePracticeDetails> details)
        {
            db.EOnlineClassRoomCoursePracticeDetails.RemoveRange(details);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Status = ResultStatus.Ok,
                Message = "Removed Successfullyy !"
            };
        }

        public ServiceResult<EOnlineClassRoomCoursePracticeDetails> Update(EOnlineClassRoomCoursePracticeDetails model)
        {
            throw new NotImplementedException();
        }
    }
}
