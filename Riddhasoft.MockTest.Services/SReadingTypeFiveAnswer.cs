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
    public class SReadingTypeFiveAnswer : IBaseService<EReadingTypeFiveAnswer>
    {
        RiddhaDBContext db = null;
        public SReadingTypeFiveAnswer()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EReadingTypeFiveAnswer> Add(EReadingTypeFiveAnswer model)
        {
            db.ReadingTypeFiveAnswer.Add(model);
            db.SaveChanges();
            return new ServiceResult<EReadingTypeFiveAnswer>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<EReadingTypeFiveAnswer>> AddRange(List<EReadingTypeFiveAnswer> model)
        {
            db.ReadingTypeFiveAnswer.AddRange(model);
            db.SaveChanges();
            return new ServiceResult<List<EReadingTypeFiveAnswer>>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EReadingTypeFiveAnswer>> List()
        {
            return new ServiceResult<IQueryable<EReadingTypeFiveAnswer>>()
            {
                Data = db.ReadingTypeFiveAnswer,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EReadingTypeFiveAnswer model)
        {
            db.ReadingTypeFiveAnswer.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EReadingTypeFiveAnswer> Update(EReadingTypeFiveAnswer model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EReadingTypeFiveAnswer>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}
