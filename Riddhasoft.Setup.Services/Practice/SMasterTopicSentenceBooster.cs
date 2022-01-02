using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.Practice
{
    public class SMasterTopicSentenceBooster : Riddhasoft.Services.Common.IBaseService<EMasterTopicSentenceBooster>
    {
        DB.RiddhaDBContext db = null;
        public SMasterTopicSentenceBooster()
        {
            db = new DB.RiddhaDBContext();
        }
        public ServiceResult<EMasterTopicSentenceBooster> Add(EMasterTopicSentenceBooster model)
        {
            db.EMasterTopicSentenceBoosters.Add(model);
            db.SaveChanges();
            return new ServiceResult<EMasterTopicSentenceBooster>()
            {
                Data = model,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EMasterTopicSentenceBooster>> List()
        {

            return new ServiceResult<IQueryable<EMasterTopicSentenceBooster>>()
            {
                Data = db.EMasterTopicSentenceBoosters,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EMasterTopicSentenceBooster model)
        {
            db.EMasterTopicSentenceBoosters.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Deleted Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EMasterTopicSentenceBooster> Update(EMasterTopicSentenceBooster model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EMasterTopicSentenceBooster>()
            {
                Data = model,
                Message = "Udpated Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<EMasterTopicSentenceBoosterOptionDetails>> AddDetails(List<EMasterTopicSentenceBoosterOptionDetails> list)
        {

            db.EMasterTopicSentenceBoosterOptionDetails.AddRange(list);
            db.SaveChanges();
            return new ServiceResult<List<EMasterTopicSentenceBoosterOptionDetails>>()
            {
                Data = list,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> RemoveDetails(List<EMasterTopicSentenceBoosterOptionDetails> list)
        {

            db.EMasterTopicSentenceBoosterOptionDetails.RemoveRange(list);
            db.SaveChanges();
            return new ServiceResult<int>()
            {

                Data = 1,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EMasterTopicSentenceBoosterOptionDetails>> DetailsList()
        {

            return new ServiceResult<IQueryable<EMasterTopicSentenceBoosterOptionDetails>>()
            {
                Data = db.EMasterTopicSentenceBoosterOptionDetails,
                Status = ResultStatus.Ok
            };
        }
    }
}
