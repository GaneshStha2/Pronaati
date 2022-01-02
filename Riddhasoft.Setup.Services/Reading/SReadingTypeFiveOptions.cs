using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Reading;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.Reading
{
    public class SReadingTypeFiveOptions : IBaseService<EReadingTypeFiveOptions>
    {
        RiddhaDBContext db = new RiddhaDBContext();
        public ServiceResult<EReadingTypeFiveOptions> Add(EReadingTypeFiveOptions model)
        {
            db.ReadingTypeFiveOptions.Add(model);
            db.SaveChanges();
            return new ServiceResult<EReadingTypeFiveOptions>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<List<EReadingTypeFiveOptions>> AddRange(List<EReadingTypeFiveOptions> list)
        {
            db.ReadingTypeFiveOptions.AddRange(list);
            db.SaveChanges();
            return new ServiceResult<List<EReadingTypeFiveOptions>>()
            {
                Data = list,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<IQueryable<EReadingTypeFiveOptions>> List()
        {
            return new ServiceResult<IQueryable<EReadingTypeFiveOptions>>()
            {
                Data = db.ReadingTypeFiveOptions,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove (int id)
        {
            try
            {
                var readingTypeFiveOptions = db.ReadingTypeFiveOptions.Find(id);
                db.ReadingTypeFiveOptions.Remove(readingTypeFiveOptions);
                db.SaveChanges();
                return new ServiceResult<int>()
                {
                    Data = 1,
                    Message = "Removed Successfully !",
                    Status = ResultStatus.Ok
                };
            }
            catch(SqlException ex)
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
        public ServiceResult<int> Remove(EReadingTypeFiveOptions model)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<EReadingTypeFiveOptions> Update(EReadingTypeFiveOptions model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EReadingTypeFiveOptions>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}
