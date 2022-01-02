using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services
{
    public class SFolder : Riddhasoft.Services.Common.IBaseService<EFolder>
    {
        Riddhasoft.DB.RiddhaDBContext db = null;
        public SFolder()
        {
            db = new DB.RiddhaDBContext();
        }
        public ServiceResult<EFolder> Add(EFolder model)
        {
            db.EFolder.Add(model);
            db.SaveChanges();
            return new ServiceResult<EFolder>()
            {
                Data = model,
                Message = "Added Successfully",
                Status = ResultStatus.Ok

            };
        }

        public ServiceResult<IQueryable<EFolder>> List()
        {

            return new ServiceResult<IQueryable<EFolder>>()
            {
                Data = db.EFolder,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EFolder model)
        {
            db.EFolder.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EFolder> Update(EFolder model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EFolder>()
            {
                Data = model,
                Message = "Updated Successfully",
                Status = ResultStatus.Ok
            };
        }
    }
}
