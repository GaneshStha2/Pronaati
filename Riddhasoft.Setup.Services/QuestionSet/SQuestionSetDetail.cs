using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.QuestionSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.QuestionSet
{
    public class SQuestionSetDetail : IBaseService<EQuestionSetDetail>
    {
        RiddhaDBContext db = null;
        public SQuestionSetDetail()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EQuestionSetDetail> Add(EQuestionSetDetail model)
        {
            db.QuestionSetDetail.Add(model);
            db.SaveChanges();
            return new ServiceResult<EQuestionSetDetail>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<List<EQuestionSetDetail>> AddDetails(List<EQuestionSetDetail> list)
        {
           
            db.QuestionSetDetail.AddRange(list);
            db.SaveChanges();
            return new ServiceResult<List<EQuestionSetDetail>>()
            {
                Data = list,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<IQueryable<EQuestionSetDetail>> List()
        {
            return new ServiceResult<IQueryable<EQuestionSetDetail>>()
            {
                Data = db.QuestionSetDetail,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EQuestionSetDetail model)
        {
            db.QuestionSetDetail.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> RemoveRange(List<EQuestionSetDetail> detailList)
        {
            db.QuestionSetDetail.RemoveRange(detailList);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed  Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EQuestionSetDetail> Update(EQuestionSetDetail model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EQuestionSetDetail>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}