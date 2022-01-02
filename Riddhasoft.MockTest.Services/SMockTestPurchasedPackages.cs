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
    public class SMockTestPurchasedPackages : IBaseService<EMockTestPurchasedPackages>
    {
        RiddhaDBContext db = null;
        public SMockTestPurchasedPackages()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EMockTestPurchasedPackages> Add(EMockTestPurchasedPackages model)
        {
            db.MockTestPurchasedPackages.Add(model);
            db.SaveChanges();
            return new ServiceResult<EMockTestPurchasedPackages>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<EMockTestPurchasedPackages>> AddRange(List<EMockTestPurchasedPackages> model)
        {
            db.MockTestPurchasedPackages.AddRange(model);
            db.SaveChanges();
            return new ServiceResult<List<EMockTestPurchasedPackages>>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }


        public ServiceResult<IQueryable<EMockTestPurchasedPackages>> List()
        {
            return new ServiceResult<IQueryable<EMockTestPurchasedPackages>>()
            {
                Data = db.MockTestPurchasedPackages,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EMockTestPurchasedPackages model)
        {
            db.MockTestPurchasedPackages.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
        }


        public ServiceResult<int> RemoveRange(List<EMockTestPurchasedPackages> model)
        {
            db.MockTestPurchasedPackages.RemoveRange(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EMockTestPurchasedPackages> Update(EMockTestPurchasedPackages model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EMockTestPurchasedPackages>
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };

        }
    }
}
