using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Course;

namespace Riddhasoft.Setup.Services.Course
{
    public class SOnlineClassRoomCourse : Riddhasoft.Services.Common.IBaseService<EOnlineClassRoomCourse>
    {
        RiddhaDBContext db = null;
        public SOnlineClassRoomCourse()
        {
            db = new RiddhaDBContext();
        }

        public ServiceResult<EOnlineClassRoomCourse> Add(EOnlineClassRoomCourse model)
        {
            model.CreatedDate = DateTime.Now;
            db.OnlineClassRoomCourse.Add(model);
            db.SaveChanges();
            return new ServiceResult<EOnlineClassRoomCourse>()
            {
                Data = model,
                Message = "Added Successfuly !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EOnlineClassRoomCourse>> List()
        {
            return new ServiceResult<IQueryable<EOnlineClassRoomCourse>>()
            {
                Data = db.OnlineClassRoomCourse,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EOnlineClassRoomCourse model)
        {
            db.OnlineClassRoomCourse.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EOnlineClassRoomCourse> Update(EOnlineClassRoomCourse model)
        {
            db.Entry<EOnlineClassRoomCourse>(model).State = EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EOnlineClassRoomCourse>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}
