using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Listening;
using Riddhasoft.Setup.Entity.QuestionSet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.Listening
{
    public class SListeningTypeSeven : Riddhasoft.Services.Common.IBaseService<EListeningTypeSeven>
    {
        RiddhaDBContext db = new RiddhaDBContext();
        public ServiceResult<EListeningTypeSeven> Add(EListeningTypeSeven model)
        {
            model.CreatedDateTime = DateTime.Now;
            db.ListeningTypeSeven.Add(model);
            db.SaveChanges();
            return new ServiceResult<EListeningTypeSeven>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EListeningTypeSeven>> List()
        {
            return new ServiceResult<IQueryable<EListeningTypeSeven>>()
            {
                Data = db.ListeningTypeSeven,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EListeningTypeSeven model)
        {
            try
            {
                int QuestionSetCount = db.QuestionSetDetail.Count(x => x.QuestionId == model.Id && x.QuestionType == QuestionType.ListeningTypeSeven);
                if (QuestionSetCount > 0)
                {
                    return new ServiceResult<int>
                    {
                        Data = 1,
                        Message = string.Format("There {1} {0} question set with this question. Question Can't be deleted.", QuestionSetCount, QuestionSetCount == 1 ? "is" : "are"),
                        Status = ResultStatus.dataBaseError
                    };
                }
                db.ListeningTypeSeven.Remove(model);
                db.SaveChanges();
                return new ServiceResult<int>()
                {
                    Data = 1,
                    Message = "Removed Successfully !",
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

        public ServiceResult<EListeningTypeSeven> Update(EListeningTypeSeven model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EListeningTypeSeven>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}
