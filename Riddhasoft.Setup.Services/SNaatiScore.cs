using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services
{
    public class SNaatiScore : Riddhasoft.Services.Common.IBaseService<ENaatiScore>
    {
        RiddhaDBContext db = null;
        public SNaatiScore()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<ENaatiScore> Add(ENaatiScore model)
        {
            db.NaatiScore.Add(model);
            db.SaveChanges();

            return new ServiceResult<ENaatiScore>()
            {
                Data = model,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<IQueryable<ENaatiScore>> List()
        {
            return new ServiceResult<IQueryable<ENaatiScore>>()
            {
                Data = db.NaatiScore,
                Message = "",
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<int> Remove(ENaatiScore model)
        {
            db.NaatiScore.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Deleted Successfully",
                Status = ResultStatus.Ok
            };
            throw new NotImplementedException();
        }
        public ServiceResult<ENaatiScore> Update(ENaatiScore model)
        {
            db.Entry<ENaatiScore>(model).State = EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ENaatiScore>()
            {
                Data = model,
                Message = "Update Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<ENaatiScoreDetail>> AddNaatiScoreDetails(List<ENaatiScoreDetail> list)
        {
            db.NaatiScoreDetail.AddRange(list);
            db.SaveChanges();

            return new ServiceResult<List<ENaatiScoreDetail>>()
            {
                Data = list,
                Message = "",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<int> RemoveNaatiScoreDetails(List<ENaatiScoreDetail> list)
        {
            db.NaatiScoreDetail.RemoveRange(list);
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<ENaatiScoreDetail>> ListNaatiScoreDetails()
        {
            return new ServiceResult<List<ENaatiScoreDetail>>()
            {
                Data = db.NaatiScoreDetail.ToList(),
                Message = "",
                Status = ResultStatus.Ok
            };
        }



      
    }
}
