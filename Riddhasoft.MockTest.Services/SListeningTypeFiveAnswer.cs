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
    public class SListeningTypeFiveAnswer : IBaseService<EListeningTypeFiveAnswer>
    {
        RiddhaDBContext db = null;
        public SListeningTypeFiveAnswer()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EListeningTypeFiveAnswer> Add(EListeningTypeFiveAnswer model)
        {
            db.ListeningTypeFiveAnswer.Add(model);
            db.SaveChanges();
            return new ServiceResult<EListeningTypeFiveAnswer>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<List<EListeningTypeFiveAnswer>> AddRange(List<EListeningTypeFiveAnswer> model)
        {
            db.ListeningTypeFiveAnswer.AddRange(model);
            db.SaveChanges();
            return new ServiceResult<List<EListeningTypeFiveAnswer>>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<IQueryable<EListeningTypeFiveAnswer>> List()
        {
            return new ServiceResult<IQueryable<EListeningTypeFiveAnswer>>()
            {
                Data = db.ListeningTypeFiveAnswer,
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<int> Remove(EListeningTypeFiveAnswer model)
        {
            db.ListeningTypeFiveAnswer.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<EListeningTypeFiveAnswer> Update(EListeningTypeFiveAnswer model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EListeningTypeFiveAnswer>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };            
        }
    }
}
