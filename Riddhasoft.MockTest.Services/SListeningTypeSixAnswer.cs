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
    public class SListeningTypeSixAnswer : IBaseService<EListeningTypeSixAnswer>
    {
        RiddhaDBContext db = null;
        public SListeningTypeSixAnswer()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EListeningTypeSixAnswer> Add(EListeningTypeSixAnswer model)
        {
            db.ListeningTypeSixAnswer.Add(model);
            db.SaveChanges();
            return new ServiceResult<EListeningTypeSixAnswer>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<EListeningTypeSixAnswer>> AddRange(List<EListeningTypeSixAnswer> model)
        {
            db.ListeningTypeSixAnswer.AddRange(model);
            db.SaveChanges();
            return new ServiceResult<List<EListeningTypeSixAnswer>>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EListeningTypeSixAnswer>> List()
        {
            return new ServiceResult<IQueryable<EListeningTypeSixAnswer>>()
            {
                Data = db.ListeningTypeSixAnswer,
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<int> Remove(EListeningTypeSixAnswer model)
        {
            db.ListeningTypeSixAnswer.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EListeningTypeSixAnswer> Update(EListeningTypeSixAnswer model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EListeningTypeSixAnswer>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };            
        }
    }
}
