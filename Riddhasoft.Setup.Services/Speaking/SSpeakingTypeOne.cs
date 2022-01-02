using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Setup.Entity.Speaking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services
{
    public class SSpeakingTypeOne : Riddhasoft.Services.Common.IBaseService<ESpeakingTypeOne>
    {
        Riddhasoft.DB.RiddhaDBContext db = null;

        public SSpeakingTypeOne()
        {
            db = new DB.RiddhaDBContext();
        }

        public ServiceResult<ESpeakingTypeOne> Add(ESpeakingTypeOne model)
        {
            db.SpeakingTypeOne.Add(model);
            db.SaveChanges();
            return new ServiceResult<ESpeakingTypeOne>()
            {

                Data = model,
                Message = "Data inserted Successfully",
                Status = ResultStatus.Ok
            };

        }

        
        public ServiceResult<IQueryable<ESpeakingTypeOne>> List()
        {
            return new ServiceResult<IQueryable<ESpeakingTypeOne>>()
            {

                Data = db.SpeakingTypeOne,
                Message = "",
                Status = ResultStatus.Ok,
            };


        }

        public ServiceResult<int> Remove(ESpeakingTypeOne model)
        {
            try
            {
                int QuestionSetCount = db.QuestionSetDetail.Count(x => x.QuestionId == model.Id && x.QuestionType == QuestionType.SpeakingTypeOne);
                if (QuestionSetCount > 0)
                {
                    return new ServiceResult<int>
                    {
                        Data = 1,
                        Message = string.Format("There {1} {0} question set with this question. Question Can't be deleted.", QuestionSetCount, QuestionSetCount == 1 ? "is" : "are"),
                        Status = ResultStatus.dataBaseError
                    };
                }

                db.SpeakingTypeOne.Remove(model);
                db.SaveChanges();
                return new ServiceResult<int>()
                {
                    Data = 1,
                    Message = "Data Deleted Successfully",
                    Status = ResultStatus.Ok
                };
            }
            catch (SqlException ex)
            {
                return new ServiceResult<int>()
                {
                    Data = 1,
                    Message = ex.Message,
                    Status = ResultStatus.dataBaseError
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<int>()
                {
                    Data = 1,
                    Message = ex.Message,
                    Status = ResultStatus.unHandeledError
                };
            }

        }

        public ServiceResult<ESpeakingTypeOne> Update(ESpeakingTypeOne model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ESpeakingTypeOne>()
            {
                Data = model,
                Message = "Data Updated successfully",
                Status = ResultStatus.Ok

            };

        }


    }
}
