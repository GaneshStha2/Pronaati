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
    public class SQuestionPackageMaster : IBaseService<EQuestionPackageMaster>
    {
        RiddhaDBContext db = null;
        public SQuestionPackageMaster()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EQuestionPackageMaster> Add(EQuestionPackageMaster model)
        {
            model.CreatedDate = DateTime.Now;
            //model.CreatedBy = 0;
            db.QuestionPackageMaster.Add(model);
            db.SaveChanges();
            return new ServiceResult<EQuestionPackageMaster>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EQuestionPackageMaster>> List()
        {
            return new ServiceResult<IQueryable<EQuestionPackageMaster>>()
            {
                Data = db.QuestionPackageMaster,
                Message = "",
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<int> Remove(EQuestionPackageMaster model)
        {
            db.QuestionPackageMaster.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EQuestionPackageMaster> Update(EQuestionPackageMaster model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EQuestionPackageMaster>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}
