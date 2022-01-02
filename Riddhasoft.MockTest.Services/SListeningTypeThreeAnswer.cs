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
    public class SListeningTypeThreeAnswer : IBaseService<EListeningTypeThreeAnswer>
    {
        RiddhaDBContext db = null;
        public SListeningTypeThreeAnswer()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EListeningTypeThreeAnswer> Add(EListeningTypeThreeAnswer model)
        {
            db.ListeningTypeThreeAnswer.Add(model);
            db.SaveChanges();
            return new ServiceResult<EListeningTypeThreeAnswer>()
            {
                Data = model,
                Message = "Addedd Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<EListeningTypeThreeAnswer>> AddRange(List<EListeningTypeThreeAnswer> model)
        {
            db.ListeningTypeThreeAnswer.AddRange(model);
            db.SaveChanges();
            return new ServiceResult<List<EListeningTypeThreeAnswer>>()
            {
                Data = model,
                Message = "Addedd Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EListeningTypeThreeAnswer>> List()
        {
            return new ServiceResult<IQueryable<EListeningTypeThreeAnswer>>()
            {
                Data = db.ListeningTypeThreeAnswer,
                Status = ResultStatus.Ok
            };            
        }

        public ServiceResult<int> Remove(EListeningTypeThreeAnswer model)
        {
            db.ListeningTypeThreeAnswer.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<EListeningTypeThreeAnswer> Update(EListeningTypeThreeAnswer model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EListeningTypeThreeAnswer>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
            
        }
    }
}
