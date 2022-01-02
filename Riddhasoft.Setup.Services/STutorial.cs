using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services
{
    public class STutorial : Riddhasoft.Services.Common.IBaseService<ETutorials>
    {
        Riddhasoft.DB.RiddhaDBContext db = null;

        public STutorial()
        {
            db = new DB.RiddhaDBContext();
        }
        public ServiceResult<ETutorials> Add(ETutorials model)
        {
            db.ETutorials.Add(model);
            db.SaveChanges();
            return new ServiceResult<ETutorials>()
            {

                Data = model,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<ETutorials>> List()
        {
            return new ServiceResult<IQueryable<ETutorials>>()
            {
                Data = db.ETutorials,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(ETutorials model)
        {
            db.ETutorials.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<ETutorials> Update(ETutorials model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ETutorials>()
            {

                Data = model,
                Message = "Updated successfully",
                Status = ResultStatus.Ok
            };
        }


    }
}
