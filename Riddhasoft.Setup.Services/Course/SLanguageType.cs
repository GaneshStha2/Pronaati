using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Course;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.Course
{
    public class SLanguageType : Riddhasoft.Services.Common.IBaseService<ELanguageType>
    {
        RiddhaDBContext db = null;
        public SLanguageType()
        {
            db = new RiddhaDBContext();
        }

        public ServiceResult<ELanguageType> Add(ELanguageType model)
        {
            db.CourseType.Add(model);
            db.SaveChanges();
            return new ServiceResult<ELanguageType>
            {
                Data = model,
                Message = "AddedSucess",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<ELanguageType>> List()
        {
            return new ServiceResult<IQueryable<ELanguageType>>
            {
                Data = db.CourseType,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(ELanguageType model)
        {

            try
            {
                int onlineClassRoomCourseCount = db.OnlineClassRoomCourse.Count(x => x.LanguageTypeId == model.Id);
                if (onlineClassRoomCourseCount > 0)
                {
                    return new ServiceResult<int>()
                    {
                        Data = 1,
                        Message = string.Format("There {1} {0} online class room course with this Course Type. Course Type Can't be deleted.", onlineClassRoomCourseCount, onlineClassRoomCourseCount == 1 ? "is" : "are"),
                        Status = ResultStatus.dataBaseError
                    };
                }

                int OnlineTrainingCount = db.OnlineTraining.Count(x => x.CourseTypeId == model.Id);
                if(OnlineTrainingCount > 0)
                {
                    return new ServiceResult<int>()
                    {
                        Data = 1,
                        Message = string.Format("There {1} {0} online class room course with this Course Type. Course Type Can't be deleted.", onlineClassRoomCourseCount, onlineClassRoomCourseCount == 1 ? "is" : "are"),
                        Status = ResultStatus.dataBaseError
                    };
                }


                db.CourseType.Remove(model);
                db.SaveChanges();
                return new ServiceResult<int>
                {
                    Data = 1,
                    Message = "RemoveSucess",
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



        public ServiceResult<ELanguageType> Update(ELanguageType model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ELanguageType>
            {
                Data = model,
                Message = "UpdateSucess",
                Status = ResultStatus.Ok
            };
        }
    }
}
