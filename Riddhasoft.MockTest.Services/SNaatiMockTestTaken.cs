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
    public class SNaatiMockTestTaken : Riddhasoft.Services.Common.IBaseService<ENaatiMockTestTaken>
    {
        RiddhaDBContext db = null;

        public SNaatiMockTestTaken()
        {
            db = new RiddhaDBContext();
        }

        public ServiceResult<ENaatiMockTestTaken> Add(ENaatiMockTestTaken model)
        {
            db.NaatiMockTestTaken.Add(model);
            db.SaveChanges();
            return new ServiceResult<ENaatiMockTestTaken>()
            {
                Data = model,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<ENaatiMockTestTaken>> List()
        {
            return new ServiceResult<IQueryable<ENaatiMockTestTaken>>()
            {
                Data = db.NaatiMockTestTaken,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(ENaatiMockTestTaken model)
        {
            db.NaatiMockTestTaken.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Deleted Successfully"
            };
        }

        public ServiceResult<ENaatiMockTestTaken> Update(ENaatiMockTestTaken model)
        {
            db.Entry<ENaatiMockTestTaken>(model).State = EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ENaatiMockTestTaken>()
            {
                Data = model,
                Message = "Updated Successfully",
                Status = ResultStatus.Ok
            };
        }
    }
}
