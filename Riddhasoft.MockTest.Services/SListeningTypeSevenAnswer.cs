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
    public class SListeningTypeSevenAnswer : IBaseService<ELIsteningTYpeSevenAnswer>
    {
        RiddhaDBContext db = null;
        public SListeningTypeSevenAnswer()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<ELIsteningTYpeSevenAnswer> Add(ELIsteningTYpeSevenAnswer model)
        {
            db.LIsteningTYpeSevenAnswer.Add(model);
            db.SaveChanges();
            return new ServiceResult<ELIsteningTYpeSevenAnswer>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<List<ELIsteningTYpeSevenAnswer>> AddRange(List<ELIsteningTYpeSevenAnswer> model)
        {
            db.LIsteningTYpeSevenAnswer.AddRange(model);
            db.SaveChanges();
            return new ServiceResult<List<ELIsteningTYpeSevenAnswer>>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<IQueryable<ELIsteningTYpeSevenAnswer>> List()
        {
            return new ServiceResult<IQueryable<ELIsteningTYpeSevenAnswer>>()
            {
                Data = db.LIsteningTYpeSevenAnswer,
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<int> Remove(ELIsteningTYpeSevenAnswer model)
        {
            db.LIsteningTYpeSevenAnswer.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<ELIsteningTYpeSevenAnswer> Update(ELIsteningTYpeSevenAnswer model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ELIsteningTYpeSevenAnswer>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
            
        }
    }
}
