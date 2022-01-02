using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.Practice
{
    public class SVocabularyAndPronunciationBooster : Riddhasoft.Services.Common.IBaseService<EVocabularyAndPronunciationBooster>
    {
        DB.RiddhaDBContext db = null;
        public SVocabularyAndPronunciationBooster()
        {
            db = new DB.RiddhaDBContext();
        }
        public ServiceResult<EVocabularyAndPronunciationBooster> Add(EVocabularyAndPronunciationBooster model)
        {
            db.EVocabularyAndPronunciationBoosters.Add(model);
            db.SaveChanges();
            return new ServiceResult<EVocabularyAndPronunciationBooster>()
            {
                Data = model,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EVocabularyAndPronunciationBooster>> List()
        {
            return new ServiceResult<IQueryable<EVocabularyAndPronunciationBooster>>()
            {
                Data = db.EVocabularyAndPronunciationBoosters,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EVocabularyAndPronunciationBooster model)
        {

            db.EVocabularyAndPronunciationBoosters.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Deleted Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EVocabularyAndPronunciationBooster> Update(EVocabularyAndPronunciationBooster model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EVocabularyAndPronunciationBooster>()
            {
                Data = model,
                Message = "Updated successfully",
                Status = ResultStatus.Ok
            };
        }
    }
}
