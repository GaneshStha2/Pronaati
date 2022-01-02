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
    public class SReadingTypeFourAnswer : IBaseService<EReadingTypeFourAnswer>
    {
        RiddhaDBContext db = null;
        public SReadingTypeFourAnswer()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EReadingTypeFourAnswer> Add(EReadingTypeFourAnswer model)
        {
            db.ReadingTypeFourAnswer.Add(model);
            db.SaveChanges();
            return new ServiceResult<EReadingTypeFourAnswer>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<EReadingTypeFourAnswer>> AddRange(List<EReadingTypeFourAnswer> model)
        {
            db.ReadingTypeFourAnswer.AddRange(model);
            db.SaveChanges();
            return new ServiceResult<List<EReadingTypeFourAnswer>>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }


        public ServiceResult<IQueryable<EReadingTypeFourAnswer>> List()
        {
            return new ServiceResult<IQueryable<EReadingTypeFourAnswer>>()
            {
                Data = db.ReadingTypeFourAnswer,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EReadingTypeFourAnswer model)
        {
            db.ReadingTypeFourAnswer.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EReadingTypeFourAnswer> Update(EReadingTypeFourAnswer model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EReadingTypeFourAnswer>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}
