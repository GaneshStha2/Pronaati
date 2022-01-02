using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.User.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Services.User
{
    public class SUser : Riddhasoft.Services.Common.IBaseService<EUser>
    {
        RiddhaDBContext db = null;
        public SUser()
        {
            db = new RiddhaDBContext();
        }

        public Common.ServiceResult<IQueryable<EUser>> List()
        {
            return new Riddhasoft.Services.Common.ServiceResult<IQueryable<EUser>>()
            {
                Data = db.User,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public Common.ServiceResult<EUser> Add(EUser model)
        {
            db.User.Add(model);
            db.SaveChanges();
            return new ServiceResult<EUser>()
            {
                Data = model,
                Message = "AddedSuccess",
                Status = ResultStatus.Ok
            };
        }

        public Common.ServiceResult<EUser> Update(EUser model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EUser>()
            {
                Data = model,
                Message = "UpdatedSuccess",
                Status = ResultStatus.Ok
            };
        }

        public Common.ServiceResult<int> Remove(EUser model)
        {
            model.IsDeleted = true;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "RemoveSuccess",
                Status = ResultStatus.Ok
            };
        }
    }
}
