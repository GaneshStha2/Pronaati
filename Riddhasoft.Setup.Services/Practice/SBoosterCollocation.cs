using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.Practice
{
    public class SBoosterCollocation : IBaseService<EBoosterCollocation>
    {
        RiddhaDBContext db = null;
        public SBoosterCollocation()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EBoosterCollocation> Add(EBoosterCollocation model)
        {
            db.BoosterCollocation.Add(model);
            db.SaveChanges();
            return new ServiceResult<EBoosterCollocation>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<IQueryable<EBoosterCollocation>> List()
        {
            return new ServiceResult<IQueryable<EBoosterCollocation>>()
            {
                Data = db.BoosterCollocation,
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<int> Remove(EBoosterCollocation model)
        {
            db.BoosterCollocation.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Deleted Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EBoosterCollocation> Update(EBoosterCollocation model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EBoosterCollocation>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<List<EBoosterCollocationDetails>> AddDetails(List<EBoosterCollocationDetails> list)
        {
            db.BoosterCollocationDetails.AddRange(list);
            db.SaveChanges();
            return new ServiceResult<List<EBoosterCollocationDetails>>()
            {
                Data = list,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> RemoveDetails(List<EBoosterCollocationDetails> list)
        {
            db.BoosterCollocationDetails.RemoveRange(list);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Deleted Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EBoosterCollocationDetails>> OptionsList()
        {
            return new ServiceResult<IQueryable<EBoosterCollocationDetails>>()
            {
                Data = db.BoosterCollocationDetails,
                Status = ResultStatus.Ok
            };
        }
    }
}
