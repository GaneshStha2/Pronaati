using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Package;

namespace Riddhasoft.Setup.Services.Package
{
    public class SPackage : Riddhasoft.Services.Common.IBaseService<EPackage>
    {
        RiddhaDBContext db = null;
        public SPackage()
        {
            db = new RiddhaDBContext();
        }

        public ServiceResult<EPackage> Add(EPackage model)
        {
            model.CreatedDate = DateTime.Now;
            db.Package.Add(model);
            db.SaveChanges();
            return new ServiceResult<EPackage>()
            {
                Data = model,
                Status = ResultStatus.Ok,
                Message = "Added Successfully"
            };
            
        }

        public ServiceResult<IQueryable<EPackage>> List()
        {
            return new ServiceResult<IQueryable<EPackage>>()
            {
                Data = db.Package,
                Status = ResultStatus.Ok,
                Message = ""
            };
        }

        public ServiceResult<int> Remove(EPackage model)
        {
            db.Package.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>
            {
                Data = 1,
                Status = ResultStatus.Ok,
                Message = "Deleted Successfully"
            };
        }

        public ServiceResult<EPackage> Update(EPackage model)
        {
            db.Entry<EPackage>(model).State = EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EPackage>()
            {
                Data = model,
                Status = ResultStatus.Ok,
                Message = "Updated Successfullt"
            };
        }
    }
}
