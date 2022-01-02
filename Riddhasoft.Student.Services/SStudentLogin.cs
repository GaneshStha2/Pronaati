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
    public class SStudentLogin
    {
        RiddhaDBContext db = null;
        public SStudentLogin()
        {
            db = new RiddhaDBContext();
        }

        public ServiceResult<EStudentLogin> Add(EStudentLogin model)
        {
            db.StudentLogin.Add(model);
            db.SaveChanges();
            return new ServiceResult<EStudentLogin>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<EStudentLogin> Update(EStudentLogin model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EStudentLogin>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<IQueryable<EStudentLogin>> List()
        {
            return new ServiceResult<IQueryable<EStudentLogin>>()
            {
                Data = db.StudentLogin,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(int id)
        {
            var data = db.StudentLogin.Where(x => x.StudentId == id).FirstOrDefault();
            if (data != null)
            {
                data.IsDeleted = true;
                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return new ServiceResult<int>()
                {
                    Data = 1,
                    Message = "Deleted Successfully !",
                    Status = ResultStatus.Ok
                };
            }
            return new ServiceResult<int>()
            {
                Data = 0,
                Message = "Something went wrong. Please contact Administrator !",
                Status = ResultStatus.dataBaseError
            };
        }

        public ServiceResult<int> RemovePermanently(EStudentLogin model)
        {
            db.StudentLogin.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Deleted Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<bool> IsUserNameEmailExist(string usernameEmail)
        {
            var data = db.StudentLogin.Where(x => x.UserName.ToLower() == usernameEmail.ToLower()).FirstOrDefault();
            if (data == null)
            {
                var data1 = db.StudentLogin.Where(x => x.Email.ToLower() == usernameEmail.ToLower()).FirstOrDefault();
                if (data1 == null)
                {
                    return new ServiceResult<bool>()
                    {
                        Data = false,
                        Message = "Sorry, the provided Username/Email doesn't exist in the database !",
                        Status = ResultStatus.dataBaseError
                    };
                }
            }
            return new ServiceResult<bool>()
            {
                Data = true,
                Message = "",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<EStudentLogin> Login(string userName, string password)
        {
            var data = db.StudentLogin.Where(x => (x.Email.ToLower() == userName.ToLower() || x.UserName.ToLower() == userName.ToLower()) && x.Password == password).FirstOrDefault();
            if (data != null)
            {
                return new ServiceResult<EStudentLogin>()
                {
                    Data = data,
                    Message = "",
                    Status = ResultStatus.Ok
                };
            }
            return new ServiceResult<EStudentLogin>()
            {
                Data = null,
                Message = "Login Credentials don't match. Please try again !",
                Status = ResultStatus.dataBaseError
            };
        }

        public ServiceResult<bool> matchPassword(int studentId, string password)
        {
            var data = db.StudentLogin.Where(x => x.StudentId == studentId && x.Password == password).FirstOrDefault();
            if (data != null)
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
                Message = "Incorrect Password",
                Status = ResultStatus.dataBaseError
            };
        }
    }
}
