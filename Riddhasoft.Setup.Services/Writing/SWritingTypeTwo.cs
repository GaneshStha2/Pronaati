using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Setup.Entity.Writing;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.Writing
{
    public class SWritingTypeTwo : IBaseService<EWritingTypeTwo>
    {
        RiddhaDBContext db = new RiddhaDBContext();
        public ServiceResult<EWritingTypeTwo> Add(EWritingTypeTwo model)
        {
            model.CreatedDateTime = DateTime.Now;
            db.WritingTypeTwo.Add(model);
            db.SaveChanges();
            return new ServiceResult<EWritingTypeTwo>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EWritingTypeTwo>> List()
        {
            return new ServiceResult<IQueryable<EWritingTypeTwo>>()
            {
                Data = db.WritingTypeTwo,
                Message = "",
                Status = ResultStatus.Ok
            };
        }


        public ServiceResult<int> Remove(EWritingTypeTwo model)
        {
            try
            {
                int QuestionSetCount = db.QuestionSetDetail.Count(x => x.QuestionId == model.Id && x.QuestionType == QuestionType.WritingTypeTwo);
                if (QuestionSetCount > 0)
                {
                    return new ServiceResult<int>
                    {
                        Data = 1,
                        Message = string.Format("There {1} {0} question set with this question. Question Can't be deleted.", QuestionSetCount, QuestionSetCount == 1 ? "is" : "are"),
                        Status = ResultStatus.dataBaseError
                    };
                }

                db.WritingTypeTwo.Remove(model);
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

        public ServiceResult<EWritingTypeTwo> Update(EWritingTypeTwo model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EWritingTypeTwo>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}
