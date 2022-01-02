using Riddhasoft.DB;
using Riddhasoft.MockTest.Entity;
using Riddhasoft.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.MockTest.Services
{
    public class SListeningTypeOneAnswer : IBaseService<ELIsteningTypeOneAnswer>
    {
        RiddhaDBContext db = null;
        public SListeningTypeOneAnswer()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<ELIsteningTypeOneAnswer> Add(ELIsteningTypeOneAnswer model)
        {
            db.LIsteningTypeOneAnswer.Add(model);
            db.SaveChanges();
            return new ServiceResult<ELIsteningTypeOneAnswer>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<ELIsteningTypeOneAnswer>> AddRange(List<ELIsteningTypeOneAnswer> model)
        {
            db.LIsteningTypeOneAnswer.AddRange(model);
            db.SaveChanges();
            return new ServiceResult<List<ELIsteningTypeOneAnswer>>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<ELIsteningTypeOneAnswer>> List()
        {
            return new ServiceResult<IQueryable<ELIsteningTypeOneAnswer>>()
            {
                Data = db.LIsteningTypeOneAnswer,
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<int> Remove(ELIsteningTypeOneAnswer model)
        {
            db.LIsteningTypeOneAnswer.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };            
        }

        public ServiceResult<ELIsteningTypeOneAnswer> Update(ELIsteningTypeOneAnswer model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ELIsteningTypeOneAnswer>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };            
        }
    }
}
