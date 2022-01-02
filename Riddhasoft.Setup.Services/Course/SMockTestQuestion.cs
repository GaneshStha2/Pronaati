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
    public class SMockTestQuestion : Riddhasoft.Services.Common.IBaseService<EMockTestQuestionMaster>
    {
        RiddhaDBContext db = null;

        public SMockTestQuestion()
        {
            db = new RiddhaDBContext();
        }

        public ServiceResult<EMockTestQuestionMaster> Add(EMockTestQuestionMaster model)
        {
            db.MockTestQuestionMaster.Add(model);
            db.SaveChanges();
            return new ServiceResult<EMockTestQuestionMaster>()
            {
                Data = model,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EMockTestQuestionMaster>> List()
        {
            return new ServiceResult<IQueryable<EMockTestQuestionMaster>>()
            {
                Data = db.MockTestQuestionMaster,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EMockTestQuestionMaster model)
        {
            db.MockTestQuestionMaster.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Deleted Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EMockTestQuestionMaster> Update(EMockTestQuestionMaster model)
        {
            db.Entry<EMockTestQuestionMaster>(model).State = EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EMockTestQuestionMaster>()
            {
                Data = model,
                Message = "Updated Successfully",
                Status = ResultStatus.Ok
            };
        }


        public ServiceResult<List<EMockTestQuestionDetail>> ListMockTestQuestionDetail()
        {

            return new ServiceResult<List<EMockTestQuestionDetail>>()
            {
                Data = db.MockTestQuestionDetail.ToList(),
                Message = "",
                Status = ResultStatus.Ok
            };
        }


        public ServiceResult<List<EMockTestQuestionDetail>> AddMockTestQuestionDetail(List<EMockTestQuestionDetail> list)
        {
            db.MockTestQuestionDetail.AddRange(list);
            db.SaveChanges();

            return new ServiceResult<List<EMockTestQuestionDetail>>()
            {
                Data = list,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> RemoveMockTestQuestionDetail(List<EMockTestQuestionDetail> list)
        {
            db.MockTestQuestionDetail.RemoveRange(list);
            db.SaveChanges();

            return new ServiceResult<int>()
            {
                Data = 1,
                Status = ResultStatus.Ok
            };
        }

    }
}
