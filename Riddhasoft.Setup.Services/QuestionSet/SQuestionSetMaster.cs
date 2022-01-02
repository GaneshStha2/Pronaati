using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.QuestionSet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.QuestionSet
{
    public class SQuestionSetMaster : IBaseService<EQuestionSetMaster>
    {
        RiddhaDBContext db = null;
        public SQuestionSetMaster()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EQuestionSetMaster> Add(EQuestionSetMaster model)
        {
            model.CreatedDate = DateTime.Now;
            db.QuestionSetMaster.Add(model);
            db.SaveChanges();
            return new ServiceResult<EQuestionSetMaster>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }


        public ServiceResult<IQueryable<EQuestionSetMaster>> List()
        {
            return new ServiceResult<IQueryable<EQuestionSetMaster>>()
            {
                Data = db.QuestionSetMaster,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EQuestionSetMaster model)
        {
            try
            {
                int QuestionPackageCount = db.QuestionPackageDetail.Count(x => x.QuestionSetId == model.Id);
                if (QuestionPackageCount > 0)
                {
                    return new ServiceResult<int>
                    {
                        Data = 1,
                        Message = string.Format("There {1} {0} question package with this question set. Question Set Can't be deleted.", QuestionPackageCount, QuestionPackageCount == 1 ? "is" : "are"),
                        Status = ResultStatus.dataBaseError
                    };
                }
                db.QuestionSetMaster.Remove(model);
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

        public ServiceResult<EQuestionSetMaster> Update(EQuestionSetMaster model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EQuestionSetMaster>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}
