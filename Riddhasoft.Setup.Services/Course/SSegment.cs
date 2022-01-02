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
    public class SSegment : Riddhasoft.Services.Common.IBaseService<ESegment>
    {
        RiddhaDBContext db = null;

        public SSegment()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<ESegment> Add(ESegment model)
        {
            db.Segment.Add(model);
            db.SaveChanges();
            return new ServiceResult<ESegment>()
            {
                Data = model,
                Message = "AddSuccess",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<ESegment>> List()
        {

            return new ServiceResult<IQueryable<ESegment>>()
            {
                Data = db.Segment,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(ESegment model)
        {
            db.Segment.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "RemoveSuccess",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<ESegment> Update(ESegment model)
        {
            db.Entry<ESegment>(model).State = EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ESegment>()
            {
                Data = model,
                Message = "UpdateSuccess",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<ESegment>> AddList(List<ESegment> list)
        {
            db.Segment.AddRange(list);
            db.SaveChanges();
            return new ServiceResult<List<ESegment>>()
            {
                Data = list,
                Message = "AddedSuccess",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> RemoveList(List<ESegment> list)
        {
            db.Segment.RemoveRange(list);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "AddedSuccess",
                Status = ResultStatus.Ok
            };
        }

    }
}
