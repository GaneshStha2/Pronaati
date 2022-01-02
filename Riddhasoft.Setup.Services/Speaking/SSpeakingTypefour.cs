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
    public class SSpeakingTypefour : Riddhasoft.Services.Common.IBaseService<ESpeakingTypeFour>
    {
        Riddhasoft.DB.RiddhaDBContext db = null;
        public SSpeakingTypefour()
        {
            db = new DB.RiddhaDBContext();
        }
        public ServiceResult<ESpeakingTypeFour> Add(ESpeakingTypeFour model)
        {
            db.SpeakingTypeFour.Add(model);
            db.SaveChanges();
            return new ServiceResult<ESpeakingTypeFour>()
            {
                Data = model,
                Message = "Data Inserted successfully",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<IQueryable<ESpeakingTypeFour>> List()
        {
            return new ServiceResult<IQueryable<ESpeakingTypeFour>>()
            {
                Data = db.SpeakingTypeFour,
                Message = "",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<int> Remove(ESpeakingTypeFour model)
        {
            try
            {

                int QuestionSetCount = db.QuestionSetDetail.Count(x => x.QuestionId == model.Id && x.QuestionType == QuestionType.SpeakingTypeFour);
                if (QuestionSetCount > 0)
                {
                    return new ServiceResult<int>
                    {
                        Data = 1,
                        Message = string.Format("There {1} {0} question set with this question. Question Can't be deleted.", QuestionSetCount, QuestionSetCount == 1 ? "is" : "are"),
                        Status = ResultStatus.dataBaseError
                    };
                }

                db.SpeakingTypeFour.Remove(model);
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

        public ServiceResult<ESpeakingTypeFour> Update(ESpeakingTypeFour model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ESpeakingTypeFour>()
            {
                Data = model,
                Message = "Data updated successfully",
                Status = ResultStatus.Ok
            };

        }
    }
}
