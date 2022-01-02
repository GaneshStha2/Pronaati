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
    public class SReadingTypeTwoAnswer : IBaseService<EReadingTypeTwoAnswer>
    {
        RiddhaDBContext db = null;
        public SReadingTypeTwoAnswer()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EReadingTypeTwoAnswer> Add(EReadingTypeTwoAnswer model)
        {
            db.ReadingTypeTwoAnswer.Add(model);
            db.SaveChanges();
            return new ServiceResult<EReadingTypeTwoAnswer>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<EReadingTypeTwoAnswer>> AddRange(List<EReadingTypeTwoAnswer> model)
        {
            db.ReadingTypeTwoAnswer.AddRange(model);
            db.SaveChanges();
            return new ServiceResult<List<EReadingTypeTwoAnswer>>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }


        public ServiceResult<IQueryable<EReadingTypeTwoAnswer>> List()
        {
            return new ServiceResult<IQueryable<EReadingTypeTwoAnswer>>()
            {
                Data = db.ReadingTypeTwoAnswer,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EReadingTypeTwoAnswer model)
        {
            db.ReadingTypeTwoAnswer.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EReadingTypeTwoAnswer> Update(EReadingTypeTwoAnswer model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EReadingTypeTwoAnswer>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}
