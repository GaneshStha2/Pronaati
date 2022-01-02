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
    public class SSpeakingTypeSeven : Riddhasoft.Services.Common.IBaseService<ESpeakingTypeSeven>
    {
        Riddhasoft.DB.RiddhaDBContext db = null;
        public SSpeakingTypeSeven()
        {
            db = new DB.RiddhaDBContext();
        }
        public ServiceResult<ESpeakingTypeSeven> Add(ESpeakingTypeSeven model)
        {
            db.SpeakingTypeSeven.Add(model);
            db.SaveChanges();
            return new ServiceResult<ESpeakingTypeSeven>()
            {
                Data = model,
                Message = "Data added successfully",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<IQueryable<ESpeakingTypeSeven>> List()
        {
            return new ServiceResult<IQueryable<ESpeakingTypeSeven>>()
            {
                Data = db.SpeakingTypeSeven,
                Message = "",
                Status = ResultStatus.Ok

            };

        }

        public ServiceResult<int> Remove(ESpeakingTypeSeven model)
        {
            try
            {
                int QuestionSetCount = db.QuestionSetDetail.Count(x => x.QuestionId == model.Id && x.QuestionType == QuestionType.SpeakingTypeSeven);
                if (QuestionSetCount > 0)
                {
                    return new ServiceResult<int>
                    {
                        Data = 1,
                        Message = string.Format("There {1} {0} question set with this question. Question Can't be deleted.", QuestionSetCount, QuestionSetCount == 1 ? "is" : "are"),
                        Status = ResultStatus.dataBaseError
                    };
                }


                db.SpeakingTypeSeven.Remove(model);
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

        public ServiceResult<ESpeakingTypeSeven> Update(ESpeakingTypeSeven model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ESpeakingTypeSeven>()
            {
                Data = model,
                Message = "Data udpated successfully",
                Status = ResultStatus.Ok
            };

        }
    }
}
