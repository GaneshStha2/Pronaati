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
    public class SReadingTypeThreeAnswer : IBaseService<EReadingTypeThreeAnswer>
    {
        RiddhaDBContext db = null;
        public SReadingTypeThreeAnswer()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EReadingTypeThreeAnswer> Add(EReadingTypeThreeAnswer model)
        {
            db.ReadingTypeThreeAnswer.Add(model);
            db.SaveChanges();
            return new ServiceResult<EReadingTypeThreeAnswer>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<EReadingTypeThreeAnswer>> AddRange(List<EReadingTypeThreeAnswer> model)
        {
            db.ReadingTypeThreeAnswer.AddRange(model);
            db.SaveChanges();
            return new ServiceResult<List<EReadingTypeThreeAnswer>>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }


        public ServiceResult<IQueryable<EReadingTypeThreeAnswer>> List()
        {
            return new ServiceResult<IQueryable<EReadingTypeThreeAnswer>>()
            {
                Data = db.ReadingTypeThreeAnswer,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EReadingTypeThreeAnswer model)
        {
            db.ReadingTypeThreeAnswer.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<EReadingTypeThreeAnswer> Update(EReadingTypeThreeAnswer model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EReadingTypeThreeAnswer>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}
