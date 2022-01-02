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
    public class SReadingTypeFive : IBaseService<EReadingTypeFive>
    {
        RiddhaDBContext db = new RiddhaDBContext();
        public ServiceResult<EReadingTypeFive> Add(EReadingTypeFive model)
        {
            model.CreatedDateTime = DateTime.Now;
            db.ReadingTypeFive.Add(model);
            db.SaveChanges();
            return new ServiceResult<EReadingTypeFive>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<EReadingTypeFive> Add(EReadingTypeFive model, List<EReadingTypeFiveDropdown> dropdowns)
        {
            model.CreatedDateTime = DateTime.Now;
            db.ReadingTypeFive.Add(model);
            db.SaveChanges();

            dropdowns.ForEach(x => x.ReadingTypeFiveId = model.Id);

            db.ReadingTypeFiveDropdown.AddRange(dropdowns);
            db.SaveChanges();
            return new ServiceResult<EReadingTypeFive>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }



        public ServiceResult<EReadingTypeFive> Update(EReadingTypeFive model, List<EReadingTypeFiveDropdown> dropdowns)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            Remove(model.Id);

            dropdowns.ForEach(x => x.ReadingTypeFiveId = model.Id);
            db.ReadingTypeFiveDropdown.AddRange(dropdowns);
            db.SaveChanges();

            return new ServiceResult<EReadingTypeFive>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EReadingTypeFive> Update(EReadingTypeFive model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EReadingTypeFive>()
            {
                Data = model,
                Message = "",
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<int> Remove(int id)
        {
            var data = db.ReadingTypeFiveDropdown.Where(x => x.ReadingTypeFiveId == id).ToList();
            db.ReadingTypeFiveDropdown.RemoveRange(data);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "",
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<IQueryable<EReadingTypeFive>> List()
        {
            return new ServiceResult<IQueryable<EReadingTypeFive>>()
            {
                Data = db.ReadingTypeFive,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EReadingTypeFiveDropdown>> ReadingTypeFiveDropdownList()
        {
            return new ServiceResult<IQueryable<EReadingTypeFiveDropdown>>()
            {
                Data = db.ReadingTypeFiveDropdown,
                Message = "",
                Status = ResultStatus.Ok
            };
        }



        public ServiceResult<int> Remove(EReadingTypeFive model)
        {
            try
            {
                int QuestionSetCount = db.QuestionSetDetail.Count(x => x.QuestionId == model.Id && x.QuestionType == QuestionType.ReadingTypeFive);
                if (QuestionSetCount > 0)
                {
                    return new ServiceResult<int>
                    {
                        Data = 1,
                        Message = string.Format("There {1} {0} question set with this question. Question Can't be deleted.", QuestionSetCount, QuestionSetCount == 1 ? "is" : "are"),
                        Status = ResultStatus.dataBaseError
                    };
                }
                db.ReadingTypeFive.Remove(model);
                db.SaveChanges();
                return new ServiceResult<int>()
                {
                    Data = 1,
                    Message = "",
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




    }
}
