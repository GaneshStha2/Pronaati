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
    public class SListeningTypeEightAnswer : IBaseService<ELIsteningTYpeEightAnswer>
    {
        RiddhaDBContext db = null;
        public SListeningTypeEightAnswer()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<ELIsteningTYpeEightAnswer> Add(ELIsteningTYpeEightAnswer model)
        {
            db.LIsteningTYpeEightAnswer.Add(model);
            db.SaveChanges();
            return new ServiceResult<ELIsteningTYpeEightAnswer>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<ELIsteningTYpeEightAnswer>> AddRange(List<ELIsteningTYpeEightAnswer> model)
        {
            db.LIsteningTYpeEightAnswer.AddRange(model);
            db.SaveChanges();
            return new ServiceResult<List<ELIsteningTYpeEightAnswer>>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<ELIsteningTYpeEightAnswer>> List()
        {
            return new ServiceResult<IQueryable<ELIsteningTYpeEightAnswer>>()
            {
                Data = db.LIsteningTYpeEightAnswer,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(ELIsteningTYpeEightAnswer model)
        {
            db.LIsteningTYpeEightAnswer.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<ELIsteningTYpeEightAnswer> Update(ELIsteningTYpeEightAnswer model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ELIsteningTYpeEightAnswer>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}
