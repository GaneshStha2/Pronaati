using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.Practice
{
    public class SSynonymBooster : Riddhasoft.Services.Common.IBaseService<ESynonymBooster>
    {
        DB.RiddhaDBContext db = null;
        public SSynonymBooster()
        {
            db = new DB.RiddhaDBContext();
        }
        public ServiceResult<ESynonymBooster> Add(ESynonymBooster model)
        {
            db.ESynonymBoosters.Add(model);
            db.SaveChanges();
            return new ServiceResult<ESynonymBooster>()
            {
                Data = model,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<ESynonymBooster>> List()
        {
            return new ServiceResult<IQueryable<ESynonymBooster>>()
            {

                Data = db.ESynonymBoosters,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(ESynonymBooster model)
        {
            db.ESynonymBoosters.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Deleted Successfully",
                Status = ResultStatus.Ok
                
            };
        }

        public ServiceResult<ESynonymBooster> Update(ESynonymBooster model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ESynonymBooster>()
            {
                Data = model,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<ESynonymBoosterOptionDetails>> AddDetails(List<ESynonymBoosterOptionDetails> list) {

            db.ESynonymBoosterOptionDetails.AddRange(list);
            db.SaveChanges();
            return new ServiceResult<List<ESynonymBoosterOptionDetails>>()
            {
                Data = list,
                Status  = ResultStatus.Ok
            };
        }

        public ServiceResult<int> RemoveDetails(List<ESynonymBoosterOptionDetails> list) {

            db.ESynonymBoosterOptionDetails.RemoveRange(list);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<IQueryable<ESynonymBoosterOptionDetails>> OptionsList() {




            return new ServiceResult<IQueryable<ESynonymBoosterOptionDetails>>()
            {
                Data = db.ESynonymBoosterOptionDetails,
                Status = ResultStatus.Ok
            };
        }
    }
}
