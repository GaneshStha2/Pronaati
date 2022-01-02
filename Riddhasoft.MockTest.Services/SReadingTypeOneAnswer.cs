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
    public class SReadingTypeOneAnswer : IBaseService<EReadingTypeOneAnswer>
    {
        RiddhaDBContext db = null;
        public SReadingTypeOneAnswer()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EReadingTypeOneAnswer> Add(EReadingTypeOneAnswer model)
        {
            db.ReadingTypeOneAnswer.Add(model);
            db.SaveChanges();
            return new ServiceResult<EReadingTypeOneAnswer>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<List<EReadingTypeOneAnswer>> AddRange(List<EReadingTypeOneAnswer> model)
        {
            db.ReadingTypeOneAnswer.AddRange(model);
            db.SaveChanges();
            return new ServiceResult<List<EReadingTypeOneAnswer>>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<IQueryable<EReadingTypeOneAnswer>> List()
        {
            return new ServiceResult<IQueryable<EReadingTypeOneAnswer>>()
            {
                Data = db.ReadingTypeOneAnswer,
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<int> Remove(EReadingTypeOneAnswer model)
        {
            db.ReadingTypeOneAnswer.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Remomved Successfully !",
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<EReadingTypeOneAnswer> Update(EReadingTypeOneAnswer model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EReadingTypeOneAnswer>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
            
        }
    }
}
