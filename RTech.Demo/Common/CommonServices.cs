using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity;
using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Setup.Services;
using RTech.Demo.Areas.EmailTemplete.ViewModels;
using RTech.Demo.Areas.MockTest.ViewModels;
using RTech.Demo.Areas.Student.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace RTech.Demo.Common
{
    public class CommonServices
    {
        RiddhaDBContext dbContext = null;
        public CommonServices()
        {
            dbContext = new RiddhaDBContext();
        }
        Random rnd = new Random();
        public string getStudentNameFromId(int id)
        {
            var data = dbContext.StudentInformation.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                string FullName = data.FirstName + " " + data.LastName;
                if (FullName.Length <= 10)
                {
                    return FullName;
                }
                return FullName.Substring(0, 8) + "..";
                //return FullName;
            }
            return "";
        }

        public string getStudentFullNameFromId(int id)
        {
            var data = dbContext.StudentInformation.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                string FullName = data.FirstName + " " + data.MiddleName + " " + data.LastName;
                return FullName;

            }
            return "";
        }

        public string getStudentUsernameFromId(int id)
        {
            var data = dbContext.StudentInformation.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                return data.Username;
            }
            return "";
        }

        public string getStudentEmailFromId(int id)
        {
            var data = dbContext.StudentInformation.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                return data.Email;
            }
            return "";
        }

        public string GetCountryName(string CountryCode)
        {


            var CountryName = dbContext.Country.Where(x => x.CountryCode == CountryCode).FirstOrDefault().Country;
            return CountryName;

        }
        public EditProfileViewModel getProfileInfo(int id)
        {
            if (id != 0)
            {
                var c = dbContext.StudentInformation.Find(id);
                if (c != null)
                {
                    var info = new EditProfileViewModel()
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        MiddleName = c.MiddleName,
                        LastName = c.LastName,
                        Address = c.Address,
                        Phone = c.PhoneNumber,
                        Email = c.Email,
                        Gender = c.Gender,
                        BirthCountry = c.BirthCountry,
                        Occupation = c.Occupation,
                        PhotoUrl = c.PhotoUrl,
                        WebsiteUrl = c.WebsiteUrl,
                        FacebookUrl = c.FacebookUrl,
                        LinkedInUrl = c.LinkedInUrl,
                        TwitterUrl = c.TwitterUrl
                    };
                    return info;
                }
            }
            return new EditProfileViewModel();
        }

        public bool checkGuid(string guid)
        {
            var data = dbContext.StudentLogin.Where(x => x.VerificationCode == guid).FirstOrDefault();
            if (data != null)
            {
                data.IsActive = true;
                dbContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public List<CountryDropDownViewModel> getCountryDropDown()
        {
            var result = (from c in dbContext.Country.ToList()
                          select new CountryDropDownViewModel()
                          {
                              Id = c.Id,
                              CountryCode = c.CountryCode,
                              Country = c.Country
                          }).ToList();
            return result;
        }


        public string getStudentImage(int id)
        {
            var data = dbContext.StudentInformation.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                return data.PhotoUrl;
            }
            return "";
        }

        public string getCountryNameFromCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var data = dbContext.Country.Where(x => x.CountryCode == code).FirstOrDefault();
                if (data != null)
                {
                    return data.Country;
                }
            }
            return "";
        }

        public static string GetExtension(string path)
        {

            string extension = Path.GetExtension(path);
            return extension;
        }

        public static string GetIconForFile(string path)
        {
            string extension = Path.GetExtension(path);
            extension = extension.Trim('.').ToLower();

            if (extension == "png" || extension == "jpg")
            {
                return "/images/image.png";
            }
            else if (extension == "mp3")
            {


                return "/images/audio.png";
            }
            else if (extension == "pdf")
            {

                return "/images/pdf-logo.png";
            }
            else if (extension == "docx")
            {
                return "/images/doc.png";

            }
            else if (extension.ToLower() == "avi" || extension.ToLower() == "flv" || extension.ToLower() == "wmv" || extension.ToLower() == "mov" || extension.ToLower() == "mp4")
            {
                return "/images/video.png";

            }
            return "";
        }

        public bool checkIfCourseAlreadyBought(string courseCode, PaymentFor paymentFor)
        {
            if (paymentFor == PaymentFor.OnlineCourse)
            {
                var data = dbContext.PackagePaymentDetails.Where(x => x.UserId == Common.StudentSession.LoggedStudent.StudentId && x.CourseCode.ToLower() == courseCode.ToLower() && x.PaymentFor == PaymentFor.OnlineCourse && x.ExpiryDate >= DateTime.Now).FirstOrDefault();
                if (data != null)
                {
                    return true;
                }
            }
            else if (paymentFor == PaymentFor.OnlineTraining)
            {
                var data = dbContext.PackagePaymentDetails.Where(x => x.UserId == StudentSession.LoggedStudent.StudentId && x.CourseCode.ToLower() == courseCode.ToLower() && x.PaymentFor == PaymentFor.OnlineTraining && x.ExpiryDate >= DateTime.Now).FirstOrDefault();
                if (data != null)
                {
                    return true;
                }
            }
            else if (paymentFor == PaymentFor.MockTestPackage)
            {
                var data = dbContext.PackagePaymentDetails.Where(x => x.UserId == StudentSession.LoggedStudent.StudentId && x.CourseCode.ToLower() == courseCode.ToLower() && x.PaymentFor == PaymentFor.MockTestPackage && x.ExpiryDate >= DateTime.Now).FirstOrDefault();
                if (data != null)
                {
                    return true;
                }
            }
            else if (paymentFor == PaymentFor.NaatiPackage)
            {
                var data = dbContext.PackagePaymentDetails.Where(x => x.UserId == StudentSession.LoggedStudent.StudentId && x.CourseCode.ToLower() == courseCode.ToLower() && x.PaymentFor == PaymentFor.NaatiPackage && x.ExpiryDate >= DateTime.Now).FirstOrDefault();
                if (data != null)
                {
                    return true;
                }
            }
            else if (paymentFor == PaymentFor.NaatiMocktest)
            {
                var data = dbContext.PackagePaymentDetails.Where(x => x.UserId == StudentSession.LoggedStudent.StudentId && x.CourseCode.ToLower() == courseCode.ToLower() && x.PaymentFor == PaymentFor.NaatiMocktest && x.ExpiryDate >= DateTime.Now).FirstOrDefault();
                if (data != null)
                {
                    return true;
                }
            }


            return false;
        }

        public string getCodeFromId(int id, PaymentFor paymentfor)
        {
            if (paymentfor == PaymentFor.OnlineCourse)
            {
                var data = dbContext.OnlineClassRoomCourse.Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    return data.Code;
                }
            }
            else if (paymentfor == PaymentFor.OnlineTraining)
            {
                var data = dbContext.OnlineTraining.Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    return data.Code;
                }
            }

            else if (paymentfor == PaymentFor.NaatiPackage)
            {
                var data = dbContext.NaatiPackage.Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    return data.Code;
                }
            }
            return "";
        }

        public string generateUserCode(string userEmail)
        {
            string userCode = userEmail.Substring(0, 3);
            userCode = userCode.Trim();
            this_is_start:
            string code = userCode + "-" ;
            code = code + generateRandomNuumber(100000, 999999);
            var data = dbContext.StudentInformation.Where(x => x.Code.Trim() == code.Trim()).FirstOrDefault();
            if (data != null)
            {
                goto this_is_start;
            }

            return code;
        }

        public string generateRandomNuumber(int a, int b)
        {

            int num = rnd.Next(a, b);  // creates a number between a and b
            return num.ToString();
        }

        public static void UpdateSpeakingAndWritingSessionTime(TimeSpan time)
        {

            var previoustime = Common.MockTestSession.SpeakingAndWritingTime;

            var newTime = previoustime - time;

            Common.MockTestSession.SpeakingAndWritingTime = newTime;


        }

        public List<MockTestQuestionPackages> GetMockTestPackages()
        {

            var data = (from c in dbContext.QuestionPackageMaster.ToList()
                        select new MockTestQuestionPackages()
                        {
                            Id = c.Id,
                            Code = c.PackageCode,
                            PackageName = c.PackageName,
                            Price = c.PackagePrice
                        }).ToList();
            return data;
        }

        public List<string> getCountryCodeList(string countryName)
        {
            if (!string.IsNullOrEmpty(countryName))
            {
                var data = dbContext.Country.Where(x => x.Country.ToLower().Trim().Contains(countryName.Trim().ToLower())).ToList();
                if (data.Count > 0)
                {
                    List<string> result = data.Select(x => x.CountryCode).ToList();
                    return result;
                }
            }
            return null;
        }

        public string getCountryCodeFromCountryName(string countryName)
        {
            if (!string.IsNullOrEmpty(countryName))
            {
                var data = dbContext.Country.Where(x => x.Country.Trim().ToLower() == countryName.Trim().ToLower()).FirstOrDefault();
                if (data != null)
                {
                    return data.CountryCode;
                }
            }
            return "";

        }

        public string getQuestionSetsNamesUsed(int questionId, QuestionType questionType)
        {
            if (questionId != 0 && questionType != QuestionType.Dummy)
            {
                string questionSetsNames = "";
                var data = dbContext.QuestionSetDetail.Where(x => x.QuestionId == questionId).ToList();
                foreach (var item in data)
                {
                    questionSetsNames = questionSetsNames + ", " + getQuestionSetCodeFromId(item.QuestionSetMasterId);
                }
                if (!string.IsNullOrEmpty(questionSetsNames))
                {
                    questionSetsNames = questionSetsNames.Substring(1);
                    return questionSetsNames.Trim();
                }
            }
            return "";
        }

        public string getQuestionSetCodeFromId(int questionSetId)
        {
            if (questionSetId != 0)
            {
                var data = dbContext.QuestionSetMaster.Where(x => x.Id == questionSetId).FirstOrDefault();
                if (data != null)
                {
                    return data.QuestionSetCode;
                }
            }
            return "";
        }

        public string getNaatiMockTestFromMockTestId(int naatiMockTestId)
        {
            string name = dbContext.NaatiMockTestMaster.Where(x => x.Id == naatiMockTestId).Select(x => x.Title).FirstOrDefault();
            return name;
        }

        public string getNaatiQuestionSetNameFromQuestionSetId(int questionSetId)
        {
            string name = dbContext.QuestionSetMaster.Where(x => x.Id == questionSetId).Select(x => x.QuestionSetName).FirstOrDefault();
            return name;
        }
        public string getNaatiPackageNameFromPackageId(int packageId)
        {
            string name = dbContext.NaatiPackage.Where(x => x.Id == packageId).Select(x => x.Title).FirstOrDefault();
            return name;
        }

        //Conversation Name
        public string getNaatiMoctestQuestionName(int questionId)
        {
            string name = dbContext.MockTestQuestionMaster.Where(x => x.Id == questionId).Select(x => x.Title).FirstOrDefault();
            return name;
        }

        public string GenerateTransactionId()
        {
            SPackagePaymentDetails _packagePaymentDetailsServices = new SPackagePaymentDetails();
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < 8; i++)
                s = String.Concat(s, random.Next(10).ToString());
            //return s;
            while (_packagePaymentDetailsServices.List().Data.Where(x => x.TransactionId == s).Count() > 0)
            {
                for (int i = 0; i < 8; i++)
                    s = String.Concat(s, random.Next(10).ToString());
            }
            return s;
        }

        public string GenerateOrderId()
        {
            SPackagePaymentDetails _packagePaymentDetailsServices = new SPackagePaymentDetails();
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < 8; i++)
                s = String.Concat(s, random.Next(10).ToString());
            //return s;
            while (_packagePaymentDetailsServices.List().Data.Where(x => x.OrderId == s).Count() > 0)
            {
                for (int i = 0; i < 8; i++)
                    s = String.Concat(s, random.Next(10).ToString());
            }
            return s;
        }



        //public void SendEmail(EmailTemplateViewModel model)
        //{
        //    if (model != null)
        //    {
        //        MailCommon mail = new MailCommon();
        //        var baseUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

        //        string mailLink = baseUrl + "/EmailTemplate/StudentSignUpEmailTemplete/Index?NewSignedUpEmail=" +model.ReceivingEmailAddress;
        //        string mailBody = GetTemplate(mailLink);

        //        //mail.SendMail(model.ReceivingEmailAddress, model.EmailSubject, mailBody, model.SenderEmail, password);
        //    }
        //}

        public static string GetTemplate(string link)
        {
            using (var myWebClient = new WebClient())
            {
                myWebClient.Headers["User-Agent"] = "MOZILLA/5.0 (WINDOWS NT 6.1; WOW64) APPLEWEBKIT/537.1 (KHTML, LIKE GECKO) CHROME/21.0.1180.75 SAFARI/537.1";

                string page = myWebClient.DownloadString(link);

                return page;
            }
        }

    }

    public static class TimeManagement
    {



        public static TimeSpan ManageTime()
        {
            

            var startTime = Common.MockTestSession.StartDateTime;
            var UserOnMockTestDateTime = Common.MockTestSession.UserOnMockTestDateTime;


            var TimeSpendByUserDateTime = (DateTime.Now - UserOnMockTestDateTime);

            // in Timespan Format  = 
            var TimeSpend = new TimeSpan(TimeSpendByUserDateTime.Hours, TimeSpendByUserDateTime.Minutes, TimeSpendByUserDateTime.Seconds);

            Common.MockTestSession.UserOnMockTestDateTime = DateTime.Now;



            return TimeSpend;


        }


        public static void SpeakingTimeManagement(TimeSpan timeSpend)
        {

            #region Speaking Time Section 

            var SpeakingTime = Common.MockTestSession.SpeakingTime;

            if (SpeakingTime > new TimeSpan(0, 0, 0))
            {
                Common.MockTestSession.SpeakingTime = SpeakingTime - timeSpend;
            }

            #endregion


            #region Speaking And Writing Time Section 
            //var SpeakingAndWritingTime = Common.MockTestSession.SpeakingAndWritingTime;

            //if (SpeakingAndWritingTime > new TimeSpan(0, 0, 0))
            //{


            //    Common.MockTestSession.SpeakingAndWritingTime = SpeakingAndWritingTime - timeSpend;
            //}
            #endregion

        }

        public static void ReadingTimeManagement(TimeSpan timeSpend)
        {



            #region Reading Section 


            var readingTime = Common.MockTestSession.ReadingTime;
            if (readingTime > new TimeSpan(0, 0, 0))
            {

                Common.MockTestSession.ReadingTime = readingTime - timeSpend;
            }

            #endregion



        }

        public static void ListeningTimeManagement(TimeSpan timeSpend)
        {


            #region Listening Time Section 


            var listeningTime = Common.MockTestSession.ListeningTime;
            if (listeningTime > new TimeSpan(0, 0, 0))
            {

                Common.MockTestSession.ListeningTime = listeningTime - timeSpend;

            }
            #endregion
        }
    }

    public class Log
    {
        public static void Write(string text)
        {
            string txt = string.Format("time={0},message={1}", DateTime.Now, text) + Environment.NewLine;

            File.AppendAllText("Log.txt", txt);
        }
        public static void SytemLog(string text)
        {
            string txt = string.Format(" time={0},message={1}", DateTime.Now, text) + Environment.NewLine;
            File.AppendAllText(System.Web.Hosting.HostingEnvironment.MapPath("\\Log.txt"), txt);
        }

     
    }

 
}