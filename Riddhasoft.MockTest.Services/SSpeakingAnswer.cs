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
    public class SSpeakingAnswer : IBaseService<ESpeakingAnswer>
    {
        RiddhaDBContext db = null;
        public SSpeakingAnswer()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<ESpeakingAnswer> Add(ESpeakingAnswer model)
        {
            db.SpeakingAnswer.Add(model);
            db.SaveChanges();
            return new ServiceResult<ESpeakingAnswer>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<ESpeakingAnswer>> AddRange(List<ESpeakingAnswer> modelList)
        {
            db.SpeakingAnswer.AddRange(modelList);
            db.SaveChanges();
            return new ServiceResult<List<ESpeakingAnswer>>()
                {
                    Data = modelList,
                    Message = "Added Successfully !",
                    Status = ResultStatus.Ok
                };

        }
        public ServiceResult<IQueryable<ESpeakingAnswer>> List()
        {
            return new ServiceResult<IQueryable<ESpeakingAnswer>>()
            {
                Data = db.SpeakingAnswer,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(ESpeakingAnswer model)
        {
            db.SpeakingAnswer.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<ESpeakingAnswer> Update(ESpeakingAnswer model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ESpeakingAnswer>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}
