using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Student.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Student.Services
{

    public class SStudentInformation
    {
        RiddhaDBContext db = null;
        public SStudentInformation()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EStudentInformation> Add(EStudentInformation model)
        {
            db.StudentInformation.Add(model);
            db.SaveChanges();
            return new ServiceResult<EStudentInformation>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EStudentInformation>> List()
        {
            return new ServiceResult<IQueryable<EStudentInformation>>()
            {
                Data = db.StudentInformation,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EStudentInformation> Update(EStudentInformation model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EStudentInformation>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EStudentInformation model)
        {
            db.StudentInformation.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Data Deleted Successfully",
                Status = ResultStatus.Ok
            };

        }



        public ServiceResult<bool> IsEmailExist(string email)
        {
            var data = db.StudentInformation.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
            if (data == null)
            {
                return new ServiceResult<bool>()
                {
                    Data = true,
                    Message = "",
                    Status = ResultStatus.Ok
                };
            }
            return new ServiceResult<bool>()
            {
                Data = false,
                Message = "User with this email address alredy exists !",
                Status = ResultStatus.Ok
            };

        }



        public ServiceResult<bool> IsUsernameExist(string username)
        {
            var data = db.StudentInformation.Where(x => x.Username.ToLower() == username.ToLower()).FirstOrDefault();
            if (data == null)
            {
                return new ServiceResult<bool>()
                {
                    Data = true,
                    Message = "",
                    Status = ResultStatus.Ok
                };
            }
            return new ServiceResult<bool>()
            {
                Data = false,
                Message = "Sorry this username is already taken !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<bool> IsEmailExistWhileUpdating(int studentId, string email)
        {
            var data = db.StudentInformation.Where(x => x.Email.ToLower() == email.ToLower() && x.Id != studentId).FirstOrDefault();
            if (data == null)
            {
                return new ServiceResult<bool>()
                {
                    Data = true,
                    Message = "",
                    Status = ResultStatus.Ok
                };
            }
            return new ServiceResult<bool>()
            {
                Data = false,
                Message = "A user with this email address is already registered !",
                Status = ResultStatus.dataBaseError
            };

        }


    }
}
