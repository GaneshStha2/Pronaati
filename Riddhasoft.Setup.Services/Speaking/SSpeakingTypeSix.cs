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
    public class SSpeakingTypeSix : Riddhasoft.Services.Common.IBaseService<ESpeakingTypeSix>
    {
        Riddhasoft.DB.RiddhaDBContext db = null;
        public SSpeakingTypeSix()
        {
            db = new DB.RiddhaDBContext();
        }
        public ServiceResult<ESpeakingTypeSix> Add(ESpeakingTypeSix model)
        {
            db.SpeakingTypeSix.Add(model);
            db.SaveChanges();
            return new ServiceResult<ESpeakingTypeSix>()
            {
                Data = model,
                Message = "",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<IQueryable<ESpeakingTypeSix>> List()
        {
            return new ServiceResult<IQueryable<ESpeakingTypeSix>>()
            {

                Data = db.SpeakingTypeSix,
                Message = "",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<int> Remove(ESpeakingTypeSix model)
        {
            try
            {
                int QuestionSetCount = db.QuestionSetDetail.Count(x => x.QuestionId == model.Id && x.QuestionType == QuestionType.SpeakingTypeSix);
                if (QuestionSetCount > 0)
                {
                    return new ServiceResult<int>
                    {
                        Data = 1,
                        Message = string.Format("There {1} {0} question set with this question. Question Can't be deleted.", QuestionSetCount, QuestionSetCount == 1 ? "is" : "are"),
                        Status = ResultStatus.dataBaseError
                    };
                }

                db.SpeakingTypeSix.Remove(model);
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

        public ServiceResult<ESpeakingTypeSix> Update(ESpeakingTypeSix model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ESpeakingTypeSix>()
            {
                Data = model,
                Message = "Data updated successfully",
                Status = ResultStatus.Ok

            };

        }
    }
}
