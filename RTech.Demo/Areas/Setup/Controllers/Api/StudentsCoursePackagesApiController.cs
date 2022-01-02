using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity;
using Riddhasoft.Setup.Services;
using Riddhasoft.Setup.Services.Course;
using Riddhasoft.Student.Services;
using RTech.Demo.Areas.Setup.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTech.Demo.Areas.Setup.Controllers.Api
{
    public class StudentsCoursePackagesApiController : ApiController
    {
        SOnlineClassRoomCourse _onlineClassroomCourseServices = null;
        SPackagePaymentDetails _packagePaymentDetailsServices = null;
        SStudentInformation _studentInformationServices = null;
        public StudentsCoursePackagesApiController()
        {
            _onlineClassroomCourseServices = new SOnlineClassRoomCourse();
            _packagePaymentDetailsServices = new SPackagePaymentDetails();
            _studentInformationServices = new SStudentInformation();
        }
        [HttpPost]
        public KendoGridResult<List<StudentsCoursePackagesKendoVm>> GetKendoList(KendoPageListArguments arg)
        {


            IQueryable<EPackagePaymentDetails> packageQuery;
            packageQuery = _packagePaymentDetailsServices.List().Data.Where(x => x.PaymentFor == Riddhasoft.Setup.Entity.PaymentFor.OnlineCourse);
            int totalRowNum = packageQuery.Count();
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            //IQueryable<EPackagePaymentDetails> paginatedQuery;
            var result = (from c in packageQuery.ToList()
                          join d in _studentInformationServices.List().Data on c.UserId equals d.Id
                          select new StudentsCoursePackagesKendoVm()
                          {
                              Id = c.Id,
                              StudentName = d.FirstName + " " + d.MiddleName + " " + d.LastName,
                              CourseCode = c.CourseCode,
                              CourseName = getCourseName(c.CourseCode),
                              CoursePrice = c.Amount,
                              PurchaseDate = c.TransactionDateTime.ToString("yyyy-MM-dd"),
                              StudentEmail = d.Email,
                              StudentAddress = d.Address,
                              StudentPhoneNumber = d.PhoneNumber,
                              Institute = Enum.GetName(typeof(PaymentFor), d.Institute),
                              ReceiptNumber = c.ReceiptNumber
                          }).ToList();
            switch (searchField)
            {
                case "CourseCode":
                    if (searchOp == "startswith")
                    {
                        result = result.Where(x => x.CourseCode.StartsWith(searchValue.Trim())).ToList();                        
                    }
                    else
                    {
                        result = result.Where(x => x.CourseCode == searchValue.Trim()).ToList();
                    }
                    break;

                case "CourseName":
                    if (searchOp == "startswith")
                    {
                        result = result.Where(x => x.CourseName.StartsWith(searchValue.Trim())).ToList();
                    }
                    else
                    {
                        result = result.Where(x => x.CourseName == searchValue.Trim()).ToList();
                    }
                    break;
                case "StudentName":
                    if (searchOp == "startswith")
                    {
                        result = result.Where(x => x.StudentName.StartsWith(searchValue.Trim())).ToList();
                    }
                    else
                    {
                        result = result.Where(x => x.StudentName == searchValue.Trim()).ToList();
                    }
                    break;
                case "StudentEmail":
                    if (searchOp == "startswith")
                    {
                        result = result.Where(x => x.StudentEmail.StartsWith(searchValue.Trim())).ToList();
                    }
                    else
                    {
                        result = result.Where(x => x.StudentEmail == searchValue.Trim()).ToList();
                    }
                    break;
                case "ReceiptNumber":
                    if (searchOp == "startswith")
                    {
                        result = result.Where(x => x.ReceiptNumber.StartsWith(searchValue.Trim())).ToList();
                    }
                    else
                    {
                        result = result.Where(x => x.ReceiptNumber == searchValue.Trim()).ToList();
                    }
                    break;
                default:
                    result = result;
                    break;
            }
           
            return new KendoGridResult<List<StudentsCoursePackagesKendoVm>>()
            {
                Data = result.Skip(arg.Skip).Take(arg.Take).OrderByDescending(x => x.Id).ToList(),
                Status = ResultStatus.Ok,
                TotalCount = result.Count
            };
        }


        public string getCourseName(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var data = _onlineClassroomCourseServices.List().Data.Where(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (data != null)
                {
                    return data.Name;
                }
            }
            return "";
        }
    }
}
