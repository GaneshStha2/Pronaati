using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riddhasoft.DB;
using Riddhasoft.MockTest.Entity;
using Riddhasoft.Services.Common;

namespace Riddhasoft.MockTest.Services
{
    public class SFeedbBack : Riddhasoft.Services.Common.IBaseService<EFeedBack>
    {

        RiddhaDBContext db = null;
        public SFeedbBack()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EFeedBack> Add(EFeedBack model)
        {
            db.FeedBack.Add(model);
            db.SaveChanges();
            return new ServiceResult<EFeedBack>()
            {
                Data = model,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EFeedBack>> List()
        {

            return new ServiceResult<IQueryable<EFeedBack>>()
            {
                Data = db.FeedBack,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EFeedBack model)
        {
            db.FeedBack.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EFeedBack> Update(EFeedBack model)
        {
            db.Entry<EFeedBack>(model).State = EntityState.Modified;
            db.SaveChanges();

            return new ServiceResult<EFeedBack>()
            {
                Data = model,
                Message = "Updated Successfully",
                Status = ResultStatus.Ok
            };
        }
    }
}
