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
    public class SWritingAnswer : IBaseService<EWritingAnswer>
    {
        RiddhaDBContext db = null;
        public SWritingAnswer()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EWritingAnswer> Add(EWritingAnswer model)
        {
            db.WritingAnswer.Add(model);
            db.SaveChanges();
            return new ServiceResult<EWritingAnswer>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<EWritingAnswer>> AddRange(List<EWritingAnswer> modelList)
        {
            db.WritingAnswer.AddRange(modelList);
            db.SaveChanges();
            return new ServiceResult<List<EWritingAnswer>>()
            {
                Data = modelList,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EWritingAnswer>> List()
        {
            return new ServiceResult<IQueryable<EWritingAnswer>>()
            {
                Data = db.WritingAnswer,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EWritingAnswer model)
        {
            db.WritingAnswer.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EWritingAnswer> Update(EWritingAnswer model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EWritingAnswer>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}
