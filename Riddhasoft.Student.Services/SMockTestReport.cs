using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Student.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Student.Services
{
    public class SMockTestReport : IBaseService<EMockTestReport>
    {
        RiddhaDBContext db = null;
        public SMockTestReport()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EMockTestReport> Add(EMockTestReport model)
        {
            db.MockTestReport.Add(model);
            db.SaveChanges();
            return new ServiceResult<EMockTestReport>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EMockTestReport>> List()
        {
            return new ServiceResult<IQueryable<EMockTestReport>>()
            {
                Data = db.MockTestReport,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EMockTestReport model)
        {
            db.MockTestReport.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EMockTestReport> Update(EMockTestReport model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EMockTestReport>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}
