using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services
{
    public class SFiles : Riddhasoft.Services.Common.IBaseService<EFiles>
    {
        Riddhasoft.DB.RiddhaDBContext db = null;
        public SFiles()
        {
            db = new DB.RiddhaDBContext();
        }
        public ServiceResult<EFiles> Add(EFiles model)
        {
            db.EFiles.Add(model);
            db.SaveChanges();
            return new ServiceResult<EFiles>()
            {
                Data = model,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EFiles>> List()
        {

            return new ServiceResult<IQueryable<EFiles>>()
            {
                Data = db.EFiles,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EFiles model)
        {
            db.EFiles.Remove(model);
            db.SaveChanges();

            return new ServiceResult<int>()
            {
                Data = 1,
                Message ="Removed Successfully",
                 Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EFiles> Update(EFiles model)
        {

            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EFiles>()
            {
                Data = model,
                Message = "Updated Successfully",
                Status = ResultStatus.Ok
            };
        }
    }
}
