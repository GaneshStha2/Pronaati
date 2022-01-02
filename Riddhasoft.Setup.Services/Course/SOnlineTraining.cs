using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Course;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.Course
{
    public class SOnlineTraining : Riddhasoft.Services.Common.IBaseService<EOnlineTraining>
    {
        RiddhaDBContext db = null;
        public SOnlineTraining()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EOnlineTraining> Add(EOnlineTraining model)
        {
            model.CreatedDate = DateTime.Now;
            db.OnlineTraining.Add(model);
            db.SaveChanges();
            return new ServiceResult<EOnlineTraining>()
            {
                Data = model,
                Status = ResultStatus.Ok,
                Message = "Added Successfully"
            };
            
        }

        public ServiceResult<IQueryable<EOnlineTraining>> List()
        {
            return new ServiceResult<IQueryable<EOnlineTraining>>()
            {
                Data = db.OnlineTraining,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EOnlineTraining model)
        {
            db.OnlineTraining.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Deleted Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EOnlineTraining> Update(EOnlineTraining model)
        {            
            db.Entry<EOnlineTraining>(model).State = EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EOnlineTraining>()
            {
                Data = model,
                Message = "Updated Successfully",
                Status = ResultStatus.Ok
            };
        }
    }
}
