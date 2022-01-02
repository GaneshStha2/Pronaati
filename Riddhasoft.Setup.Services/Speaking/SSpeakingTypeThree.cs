using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Setup.Entity.Speaking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.Speaking
{
    public class SSpeakingTypeThree : Riddhasoft.Services.Common.IBaseService<ESpeakingTypeThree>
    {
        Riddhasoft.DB.RiddhaDBContext db = null;
        public SSpeakingTypeThree()
        {
            db = new DB.RiddhaDBContext();
        }
        public ServiceResult<ESpeakingTypeThree> Add(ESpeakingTypeThree model)
        {
            db.SpeakingTypeThree.Add(model);
            db.SaveChanges();
            return new ServiceResult<ESpeakingTypeThree>()
            {
                Data = model,
                Message = "Data inserted successfully",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<IQueryable<ESpeakingTypeThree>> List()
        {
            return new ServiceResult<IQueryable<ESpeakingTypeThree>>()
            {
                Data = db.SpeakingTypeThree,
                Message = "",
                Status = ResultStatus.Ok,

            };

        }

        public ServiceResult<int> Remove(ESpeakingTypeThree model)
        {
            try
            {
                int QuestionSetCount = db.QuestionSetDetail.Count(x => x.QuestionId == model.Id && x.QuestionType == QuestionType.SpeakingTypeThree);
                if (QuestionSetCount > 0)
                {
                    return new ServiceResult<int>
                    {
                        Data = 1,
                        Message = string.Format("There {1} {0} question set with this question. Question Can't be deleted.", QuestionSetCount, QuestionSetCount == 1 ? "is" : "are"),
                        Status = ResultStatus.dataBaseError
                    };
                }
                db.SpeakingTypeThree.Remove(model);
                db.SaveChanges();
                return new ServiceResult<int>()
                {

                    Data = 1,
                    Message = "Data deleted successfully",
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

        public ServiceResult<ESpeakingTypeThree> Update(ESpeakingTypeThree model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ESpeakingTypeThree>()
            {
                Data = model,
                Message = "Data updated successfully",
                Status = ResultStatus.Ok
            };

        }
    }
}
