using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services
{
    public class SPackagePaymentDetails : IBaseService<EPackagePaymentDetails>
    {
        RiddhaDBContext db = null;
        public SPackagePaymentDetails()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EPackagePaymentDetails> Add(EPackagePaymentDetails model)
        {
            db.PackagePaymentDetails.Add(model);
            db.SaveChanges();
            return new ServiceResult<EPackagePaymentDetails>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EPackagePaymentDetails>> List()
        {
            return new ServiceResult<IQueryable<EPackagePaymentDetails>>()
            {
                Data = db.PackagePaymentDetails,
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<int> Remove(EPackagePaymentDetails model)
        {
            db.PackagePaymentDetails.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<EPackagePaymentDetails> Update(EPackagePaymentDetails model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EPackagePaymentDetails>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
            
        }
    }
}
