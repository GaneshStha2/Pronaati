using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.Package
{
    public class SPackageDetails : Riddhasoft.Services.Common.IBaseService<EPackageDetails>
    {
        RiddhaDBContext db = null;
        public SPackageDetails()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EPackageDetails> Add(EPackageDetails model)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<List<EPackageDetails>> AddPackageDetails(List<EPackageDetails> packageDetails)
        {
            db.PackageDetails.AddRange(packageDetails);
            db.SaveChanges();
            return new ServiceResult<List<EPackageDetails>>()
            {
                Data = packageDetails,
                Message = "Added Succesfuly",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EPackageDetails>> List()
        {
            return new ServiceResult<IQueryable<EPackageDetails>>()
            {
                Data = db.PackageDetails,
                Status = ResultStatus.Ok,
                Message = ""
            };
        }

        public ServiceResult<int> Remove(EPackageDetails model)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<int> RemovePackageDetails(List<EPackageDetails> packageDetails)
        {
            db.PackageDetails.RemoveRange(packageDetails);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Status = ResultStatus.Ok,
                Message = "Removed Successfully"
            };
        }


        public ServiceResult<EPackageDetails> Update(EPackageDetails model)
        {
            throw new NotImplementedException();
        }
    }
}
