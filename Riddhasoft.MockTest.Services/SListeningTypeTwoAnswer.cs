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
    public class SListeningTypeTwoAnswer : IBaseService<EListeningTypeTwoAnswer>
    {
        RiddhaDBContext db = null;
        public SListeningTypeTwoAnswer()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EListeningTypeTwoAnswer> Add(EListeningTypeTwoAnswer model)
        {
            db.ListeningTypeTwoAnswer.Add(model);
            db.SaveChanges();
            return new ServiceResult<EListeningTypeTwoAnswer>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<List<EListeningTypeTwoAnswer>> AddRange(List<EListeningTypeTwoAnswer> model)
        {
            db.ListeningTypeTwoAnswer.AddRange(model);
            db.SaveChanges();
            return new ServiceResult<List<EListeningTypeTwoAnswer>>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<IQueryable<EListeningTypeTwoAnswer>> List()
        {
            return new ServiceResult<IQueryable<EListeningTypeTwoAnswer>>()
            {
                Data = db.ListeningTypeTwoAnswer,
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<int> Remove(EListeningTypeTwoAnswer model)
        {
            db.ListeningTypeTwoAnswer.Remove(model );
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
            
        }

        public ServiceResult<EListeningTypeTwoAnswer> Update(EListeningTypeTwoAnswer model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EListeningTypeTwoAnswer>()
            {
                Data = model,
                Message = "Updted Successfully !",
                Status = ResultStatus.Ok
            };
            
        }
    }
}
