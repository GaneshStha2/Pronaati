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
    public class SOnlineClassRoomCourseDetails : Riddhasoft.Services.Common.IBaseService<EOnlineClassRoomCourseDetails>
    {
        RiddhaDBContext db = null;
        public SOnlineClassRoomCourseDetails()
        {
            db = new RiddhaDBContext();
        }

        public ServiceResult<EOnlineClassRoomCourseDetails> Add(EOnlineClassRoomCourseDetails model)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<List<EOnlineClassRoomCourseDetails>> AddOnlineClassRoomCourseDetails(List<EOnlineClassRoomCourseDetails> details)
        {
            db.OnlineClassRoomCourseDetails.AddRange(details);
            db.SaveChanges();
            return new ServiceResult<List<EOnlineClassRoomCourseDetails>>()
            {
                Data = details,
                Status = ResultStatus.Ok,
                Message = "Added Successfully"
            };
        }

        public ServiceResult<IQueryable<EOnlineClassRoomCourseDetails>> List()
        {
            return new ServiceResult<IQueryable<EOnlineClassRoomCourseDetails>>()
            {
                Data = db.OnlineClassRoomCourseDetails,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EOnlineClassRoomCourseDetails model)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<int> RemoveOnlineClassRoomCourseDetails(List<EOnlineClassRoomCourseDetails> details)
        {
            db.OnlineClassRoomCourseDetails.RemoveRange(details);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Status = ResultStatus.Ok,
                Message = "Removed Successfully"
            };
        }

        public ServiceResult<EOnlineClassRoomCourseDetails> Update(EOnlineClassRoomCourseDetails model)
        {
            throw new NotImplementedException();
        }
    }
}
