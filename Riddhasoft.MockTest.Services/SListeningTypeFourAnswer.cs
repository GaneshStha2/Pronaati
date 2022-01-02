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
    public class SListeningTypeFourAnswer : IBaseService<EListeningTypeFourAnswer>
    {
        RiddhaDBContext db = null;
        public SListeningTypeFourAnswer()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EListeningTypeFourAnswer> Add(EListeningTypeFourAnswer model)
        {
            db.ListeningTypeFourAnswer.Add(model);
            db.SaveChanges();
            return new ServiceResult<EListeningTypeFourAnswer>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<List<EListeningTypeFourAnswer>> AddRange(List<EListeningTypeFourAnswer> model)
        {
            db.ListeningTypeFourAnswer.AddRange(model);
            db.SaveChanges();
            return new ServiceResult<List<EListeningTypeFourAnswer>>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<IQueryable<EListeningTypeFourAnswer>> List()
        {
            return new ServiceResult<IQueryable<EListeningTypeFourAnswer>>()
            {
                Data = db.ListeningTypeFourAnswer,
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<int> Remove(EListeningTypeFourAnswer model)
        {
            db.ListeningTypeFourAnswer.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<EListeningTypeFourAnswer> Update(EListeningTypeFourAnswer model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EListeningTypeFourAnswer>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
            
        }
    }
}
