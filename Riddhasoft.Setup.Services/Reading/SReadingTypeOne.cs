using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Setup.Entity.Reading;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.Reading
{
    public class SReadingTypeOne : IBaseService<EReadingTypeOne>
    {
        RiddhaDBContext db = new RiddhaDBContext();
        public ServiceResult<EReadingTypeOne> Add(EReadingTypeOne model)
        {
            model.CreatedDateTime = DateTime.Now;
            db.ReadingTypeOne.Add(model);
            db.SaveChanges();
            return new ServiceResult<EReadingTypeOne>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EReadingTypeOne>> List()
        {
            return new ServiceResult<IQueryable<EReadingTypeOne>>()
            {
                Data = db.ReadingTypeOne,
                Message = "",
                Status = ResultStatus.Ok
            };
        }



        public ServiceResult<int> Remove(EReadingTypeOne model)
        {
            try
            {
                int QuestionSetCount = db.QuestionSetDetail.Count(x => x.QuestionId == model.Id && x.QuestionType == QuestionType.ListeningTypeOne);
                if (QuestionSetCount > 0)
                {
                    return new ServiceResult<int>
                    {
                        Data = 1,
                        Message = string.Format("There {1} {0} question set with this question. Question Can't be deleted.", QuestionSetCount, QuestionSetCount == 1 ? "is" : "are"),
                        Status = ResultStatus.dataBaseError
                    };
                }
                db.ReadingTypeOne.Remove(model);
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

        public ServiceResult<EReadingTypeOne> Update(EReadingTypeOne model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EReadingTypeOne>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}
