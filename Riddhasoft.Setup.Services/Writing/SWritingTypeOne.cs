using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Setup.Entity.Writing;

namespace Riddhasoft.Setup.Services
{
    public class SWritingTypeOne : IBaseService<EWritingTypeOne>
    {
        RiddhaDBContext db = new RiddhaDBContext();
        public ServiceResult<EWritingTypeOne> Add(EWritingTypeOne model)
        {
            model.CreatedDateTime = DateTime.Now;
            db.WritingTypeOne.Add(model);
            db.SaveChanges();
            return new ServiceResult<EWritingTypeOne>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<IQueryable<EWritingTypeOne>> List()
        {
            return new ServiceResult<IQueryable<EWritingTypeOne>>()
            {
                Data = db.WritingTypeOne,
                Message = "",
                Status = ResultStatus.Ok
            };
        }



        public ServiceResult<int> Remove(EWritingTypeOne model)
        {
            try
            {
                int QuestionSetCount = db.QuestionSetDetail.Count(x => x.QuestionId == model.Id && x.QuestionType == QuestionType.WritingTypeOne);
                if (QuestionSetCount > 0)
                {
                    return new ServiceResult<int>
                    {
                        Data = 1,
                        Message = string.Format("There {1} {0} question set with this question. Question Can't be deleted.", QuestionSetCount, QuestionSetCount == 1 ? "is" : "are"),
                        Status = ResultStatus.dataBaseError
                    };
                }

                db.WritingTypeOne.Remove(model);
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

        public ServiceResult<EWritingTypeOne> Update(EWritingTypeOne model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EWritingTypeOne>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };

        }
    }
}
