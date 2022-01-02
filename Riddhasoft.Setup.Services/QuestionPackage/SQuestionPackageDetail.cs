using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.QuestionPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.QuestionPackage
{
    public class SQuestionPackageDetail:IBaseService<EQuestionPackageDetail>
    {
        RiddhaDBContext db = null;
        public SQuestionPackageDetail()
        {
            db = new RiddhaDBContext();
        }

        public ServiceResult<EQuestionPackageDetail> Add(EQuestionPackageDetail model)
        {
            db.QuestionPackageDetail.Add(model);
            db.SaveChanges();
            return new ServiceResult<EQuestionPackageDetail>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<List<EQuestionPackageDetail>> AddDetails(List<EQuestionPackageDetail> detailList)
        {
            db.QuestionPackageDetail.AddRange(detailList);
            db.SaveChanges();
            return new ServiceResult<List<EQuestionPackageDetail>>()
            {
                Data = detailList,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<IQueryable<EQuestionPackageDetail>> List()
        {
            return new ServiceResult<IQueryable<EQuestionPackageDetail>>()
            {
                Data = db.QuestionPackageDetail,
                Message = "",
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<int> Remove(EQuestionPackageDetail model)
        {
            db.QuestionPackageDetail.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };            
        }

        public ServiceResult<int> RemoveDetailss(List<EQuestionPackageDetail> detailList)
        {
            db.QuestionPackageDetail.RemoveRange(detailList);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EQuestionPackageDetail> Update(EQuestionPackageDetail model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EQuestionPackageDetail>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
            
        }
    }
}
