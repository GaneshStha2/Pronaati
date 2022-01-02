using Riddhasoft.DB;
using Riddhasoft.MockTest.Entity;
using Riddhasoft.Services.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.MockTest.Services
{
    public class SNaatiMockTestAnswer : Riddhasoft.Services.Common.IBaseService<ENaatiMockTestAnswer>
    {
        RiddhaDBContext db = null;
        public SNaatiMockTestAnswer()
        {
            db = new RiddhaDBContext();
        }

        public ServiceResult<ENaatiMockTestAnswer> Add(ENaatiMockTestAnswer model)
        {
            db.NaatiMockTestAnswer.Add(model);
            db.SaveChanges();
            return new ServiceResult<ENaatiMockTestAnswer>()
            {
                Data = model,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<ENaatiMockTestAnswer>> List()
        {
            return new ServiceResult<IQueryable<ENaatiMockTestAnswer>>()
            {
                Data = db.NaatiMockTestAnswer,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(ENaatiMockTestAnswer model)
        {
            db.NaatiMockTestAnswer.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Deleted Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> RemoveRange(List<ENaatiMockTestAnswer> list)
        {
            db.NaatiMockTestAnswer.RemoveRange(list);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Deleted Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<ENaatiMockTestAnswer> Update(ENaatiMockTestAnswer model)
        {
            db.Entry<ENaatiMockTestAnswer>(model).State = EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ENaatiMockTestAnswer>()
            {
                Data = model,
                Message = "Updated Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<ENaatiMockTestAnswer>> AddRange(List<ENaatiMockTestAnswer> model)
        {
            db.NaatiMockTestAnswer.AddRange(model);
            db.SaveChanges();
            return new ServiceResult<List<ENaatiMockTestAnswer>>()
            {
                Data = model,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }
    }
}
