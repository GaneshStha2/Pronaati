using Riddhasoft.DB;
using Riddhasoft.Setup.Entity;
using Riddhasoft.Setup.Services;
using Riddhasoft.Setup.Services.Course;
using Riddhasoft.Setup.Services.Package;
using Riddhasoft.Setup.Services.QuestionPackage;
using RTech.Demo.Areas.Student.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.Services
{
    public class PaymentServices
    {
        SOnlineClassRoomCourse _onlineClassRoomCourseServies = null;
        SOnlineTraining _onlineTrainingServices = null;
        STutorial _tutorialServices = null;
        SQuestionPackageMaster _questionPackageMasterServices = null;
        SNaatiMockTest _naatiMockTestServices = null;
        SNaatiPackage _naatiPackageServices = null;
        RiddhaDBContext db = null;
        public PaymentServices()
        {
            _onlineClassRoomCourseServies = new SOnlineClassRoomCourse();
            _onlineTrainingServices = new SOnlineTraining();
            _tutorialServices = new STutorial();
            _questionPackageMasterServices = new SQuestionPackageMaster();
            _naatiMockTestServices = new SNaatiMockTest();
            _naatiPackageServices = new SNaatiPackage();
            db = new RiddhaDBContext();
        }

        public bool checkIfCodeExists(string code, PaymentFor paymentFor)
        {
            if (paymentFor == PaymentFor.MockTestPackage)
            {
                var data = db.QuestionPackageMaster.Where(x => x.PackageCode.ToLower() == code.ToLower()).FirstOrDefault();
                if (data == null)
                {
                    return false;
                }
            }
            else if (paymentFor == PaymentFor.OnlineCourse)
            {
                var data = db.OnlineClassRoomCourse.Where(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (data == null)
                {
                    return false;
                }
            }
            else if (paymentFor == PaymentFor.OnlineTraining)
            {
                var data = db.OnlineTraining.Where(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (data == null)
                {
                    return false;
                }
            }
            else if (paymentFor == PaymentFor.Tutorial)
            {
                var data = db.ETutorials.Where(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (data == null)
                {
                    return false;
                }
            }

            else if (paymentFor == PaymentFor.NaatiPackage)
            {
                var data = db.NaatiPackage.Where(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (data == null)
                {
                    return false;
                }
            }

            else if (paymentFor == PaymentFor.NaatiMocktest)
            {
                var data = db.NaatiMockTestMaster.Where(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (data == null)
                {
                    return false;
                }
            }

            return true;
        }
        public PaymentViewModel getPaymentDetails(string Code, PaymentFor paymentFor)
        {
            if (paymentFor == PaymentFor.OnlineTraining)
            {
                var data = _onlineTrainingServices.List().Data.Where(x => x.Code.ToLower() == Code.ToLower()).FirstOrDefault();
                var model = new PaymentViewModel()
                {
                    PaymentFor = paymentFor,
                    Code = data.Code,
                    CourseName = data.PackageTitle,
                    Amount = data.Price,
                    SubTotal = data.Price,
                    Total = data.Price
                };
                return model;
            }
            else if (paymentFor == PaymentFor.OnlineCourse)
            {
                var data = _onlineClassRoomCourseServies.List().Data.Where(x => x.Code.ToLower() == Code.ToLower()).FirstOrDefault();
                var model = new PaymentViewModel()
                {
                    PaymentFor = paymentFor,
                    Code = data.Code,
                    CourseName = data.Name,
                    Amount = data.Price,
                    SubTotal = data.Price,
                    Total = data.Price
                };
                return model;
            }
            else if (paymentFor == PaymentFor.MockTestPackage)
            {
                var data = _questionPackageMasterServices.List().Data.Where(x => x.PackageCode.ToLower() == Code.ToLower()).FirstOrDefault();
                var model = new PaymentViewModel()
                {
                    PaymentFor = paymentFor,
                    Code = data.PackageCode,
                    CourseName = data.PackageName,
                    Amount = data.PackagePrice,
                    Total = data.PackagePrice
                };
                return model;
            }


            else if (paymentFor == PaymentFor.NaatiPackage)
            {
                var data = _naatiPackageServices.List().Data.Where(x => x.Code.ToLower() == Code.ToLower()).FirstOrDefault();
                var model = new PaymentViewModel()
                {
                    PaymentFor = paymentFor,
                    Code = data.Code,
                    CourseName = data.Title,
                    Amount = data.Price,
                    Total = data.Price
                };
                return model;
            }
             else if (paymentFor == PaymentFor.NaatiMocktest)
            {
                var data = _naatiMockTestServices.List().Data.Where(x => x.Code.ToLower() == Code.ToLower()).FirstOrDefault();
                var model = new PaymentViewModel()
                {
                    PaymentFor = paymentFor,
                    Code = data.Code,
                    CourseName = data.Title,
                    Amount = data.Price,
                    Total = data.Price
                };
                return model;
            }

            return null;
        }

        //public int getPackageIdFromCode(string packageCode)
        //{
        //    if (!string.IsNullOrEmpty(packageCode))
        //    {
        //        var data = db.QuestionPackageMaster.Where(x => x.PackageCode.Trim().ToLower() == packageCode.Trim().ToLower()).FirstOrDefault();
        //        return data.Id;
        //    }
        //    return 0;

        //}

        
    }
}