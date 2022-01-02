using Riddhasoft.MockTest.Entity;
using Riddhasoft.MockTest.Services;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity;
using Riddhasoft.Setup.Services;
using Riddhasoft.Setup.Services.Package;
using Riddhasoft.Setup.Services.QuestionPackage;
using Riddhasoft.Student.Entity;
using Riddhasoft.Student.Services;
using RTech.Demo.Areas.Setup.ViewModels.Package;
using RTech.Demo.Areas.Setup.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTech.Demo.Areas.Setup.Controllers.Api.User
{
    public class StudentApiController : ApiController
    {
        SStudentInformation _studentInformationServices = null;
        SStudentLogin _studentLoginServices = null;
        Common.CommonServices _commonServices = null;
        public StudentApiController()
        {
            _studentInformationServices = new SStudentInformation();
            _studentLoginServices = new SStudentLogin();
            _commonServices = new Common.CommonServices();
        }

        [HttpPost]
        public KendoGridResult<List<UserSetupVm>> GetKendoGrid(KendoPageListArguments vm)
        {
            SStudentInformation service = new SStudentInformation();
            IQueryable<EStudentInformation> usersQuery;
            usersQuery = _studentInformationServices.List().Data.Where(x=> x.IsDeleted == false);
            int totalRowNum = usersQuery.Count();
            string searchField = vm.Filter.Filters.Count() <= 0 ? "" : vm.Filter.Filters[0].Field;
            string searchOp = vm.Filter.Filters.Count() <= 0 ? "" : vm.Filter.Filters[0].Operator;
            string searchValue = vm.Filter.Filters.Count() <= 0 ? "" : vm.Filter.Filters[0].Value;
            IQueryable<EStudentInformation> paginatedQuery;
            switch (searchField)
            {
                case "Name":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = usersQuery.Where(x => x.FirstName.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(vm.Skip).Take(vm.Take);
                    }
                    else
                    {
                        paginatedQuery = usersQuery.Where(x => x.FirstName == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(vm.Skip).Take(vm.Take);
                    }
                    break;

                case "IsDeleted":


                    bool Isdeleted = false;
                    if (searchValue.ToLower().Contains("y") || searchValue.ToLower().Contains("t"))
                    {

                        Isdeleted = true;
                    }
                    paginatedQuery = usersQuery.Where(x => x.IsDeleted == Isdeleted).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(vm.Skip).Take(vm.Take);

                    break;

                case "IsActive":

                    bool IsActive = false;
                    if (searchValue.ToLower().Contains("y") || searchValue.ToLower().Contains("t"))
                    {

                        IsActive = true;
                    }
                    paginatedQuery = (from c in _studentLoginServices.List().Data.Where(x => x.IsActive == IsActive)
                                      join d in usersQuery on c.StudentId equals d.Id
                                      select d).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(vm.Skip).Take(vm.Take);

                    break;
                case "BirthCountry":

                    string countryName = searchValue.Trim();
                    List<string> probableCountries = _commonServices.getCountryCodeList(countryName);

                    if (searchOp == "startswith")
                    {
                        paginatedQuery = usersQuery.Where(x => probableCountries.Contains(x.BirthCountry)).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(vm.Skip).Take(vm.Take);
                    }
                    else
                    {
                        string searchCountryCode = _commonServices.getCountryCodeFromCountryName(searchValue.Trim());

                        paginatedQuery = usersQuery.Where(x => x.BirthCountry == searchCountryCode.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(vm.Skip).Take(vm.Take);
                    }
                    break;


                default:
                    paginatedQuery = usersQuery.OrderByDescending(x => x.Id).ThenBy(x => x.FirstName).Skip(vm.Skip).Take(vm.Take);
                    break;
            }
            var userList = (from c in paginatedQuery.ToList()
                            join d in _studentLoginServices.List().Data on c.Id equals d.StudentId
                            select new UserSetupVm()
                            {
                                Id = c.Id,
                                Name = c.FirstName + " " + c.MiddleName + " " + c.LastName,
                                MiddleName = c.MiddleName,
                                LastName = c.LastName,
                                Address = c.Address,
                                BirthCountry = _commonServices.GetCountryName(c.BirthCountry),
                                Email = c.Email,
                                Gender = c.Gender,
                                GenderName = Enum.GetName(typeof(Gender), c.Gender),
                                Institute = c.Institute,
                                InstituteName = Enum.GetName(typeof(Institute), c.Institute),
                                Occupation = c.Occupation,
                                OccupationName = c.Occupation,
                                PhoneNumber = c.PhoneNumber,
                                IsDeleted = c.IsDeleted == true ? "Yes" : "No",
                                IsActive = d.IsActive == true ? "Yes" : "No"
                            }).ToList();
            return new KendoGridResult<List<UserSetupVm>>()
            {
                Data = userList,
                Status = ResultStatus.Ok,
                TotalCount = userList.Count()
            };
        }
        [HttpDelete]
        public ServiceResult<int> Delete(int Id)
        {

            var data = _studentInformationServices.List().Data.Where(x => x.Id == Id).FirstOrDefault();
            //data.IsDeleted = true;

            var result = _studentInformationServices.Remove(data);

            var studentLogin = _studentLoginServices.List().Data.Where(x => x.StudentId == data.Id).FirstOrDefault();
            _studentLoginServices.RemovePermanently(studentLogin);
            studentLogin.IsActive = false;
            studentLogin.IsDeleted = true;

            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Deleted Successfully",
                Status = ResultStatus.Ok
            };

        }


        [HttpGet]
        public ServiceResult<UserLoginCredentialsVm> GetUserLoginCredential(int userId)
        {
            UserLoginCredentialsVm loginCredential = (from c in _studentInformationServices.List().Data.ToList()
                                                      join d in _studentLoginServices.List().Data.ToList() on c.Id equals d.StudentId
                                                      select new UserLoginCredentialsVm()
                                                      {
                                                          Email = d.Email,
                                                          Id = c.Id,
                                                          Name = c.FirstName + " " + c.MiddleName + " " + c.LastName,
                                                          Password = d.Password
                                                      }).Where(x => x.Id == userId).FirstOrDefault();
            return new ServiceResult<UserLoginCredentialsVm>()
            {
                Data = loginCredential,
                Status = ResultStatus.Ok,
                Message = ""
            };
        }


        #region to asign mock test and package to user by admin

        [HttpGet]
        public ServiceResult<NaatiMockTestVm> GetNaatiMockTestInfo(int id)
        {
            SNaatiMockTest _naatiMockTestServices = new SNaatiMockTest();

            NaatiMockTestVm mockTest = (from c in _naatiMockTestServices.List().Data.Where(x => x.Id == id)
                                        select new NaatiMockTestVm()
                                        {
                                            Id = c.Id,
                                            Code = c.Code,
                                            Title = c.Title,
                                            LanguageTypeName = c.LanguageType.Name,
                                            Price = c.Price,
                                            Duration = c.Duration
                                        }).FirstOrDefault();

            return new ServiceResult<NaatiMockTestVm>()
            {
                Data = mockTest,
                Message = "",
                Status = ResultStatus.Ok
            };
        }


        [HttpGet]
        public ServiceResult<NaatiPackageVm> GetNaatiPackageInfo(int id)
        {
            SNaatiPackage _naatipackageServices = new SNaatiPackage();
            NaatiPackageVm package = (from c in _naatipackageServices.List().Data.Where(x => x.Id == id)
                                      select new NaatiPackageVm()
                                      {
                                          Id = c.Id,
                                          Code = c.Code,
                                          Price = c.Price,
                                          Name = c.Title,
                                          Duration = c.Duration
                                      }).FirstOrDefault();

            return new ServiceResult<NaatiPackageVm>()
            {
                Data = package,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        [HttpPost]
        public ServiceResult<List<GlobalDropdownVm>> GetMockTestAutoComplete(KendoAutoCompleteVm model)
        {
            List<GlobalDropdownVm> resultList = new List<GlobalDropdownVm>();

            SNaatiMockTest _naatiMockTestServices = new SNaatiMockTest();


            if (model != null)
            {
                string searchText = model.Filter.Filters.Count() > 0 ? model.Filter.Filters[0].Value : "";
                var list = _naatiMockTestServices.List().Data.ToList();
                if (searchText == null || searchText == "___")
                {
                    list = list.OrderBy(x => x.Title).Take(50).ToList();
                }
                else
                {
                    list = list.Where(x => x.Title.ToLower().Contains(searchText.Trim().ToLower())).OrderBy(x => x.Title).Take(50).ToList();
                }
                resultList = (from c in list.ToList()
                              select new GlobalDropdownVm()
                              {
                                  Id = c.Id,
                                  Name = c.Title,
                              }).ToList();
            }
            return new ServiceResult<List<GlobalDropdownVm>>()
            {
                Data = resultList,
                Status = ResultStatus.Ok
            };
        }

        [HttpPost]
        public ServiceResult<List<GlobalDropdownVm>> GetPakcageAutoComplete(KendoAutoCompleteVm model)
        {
            List<GlobalDropdownVm> resultList = new List<GlobalDropdownVm>();

            SNaatiPackage _naatipackageServices = new SNaatiPackage();


            if (model != null)
            {
                string searchText = model.Filter.Filters.Count() > 0 ? model.Filter.Filters[0].Value : "";
                var list = _naatipackageServices.List().Data.ToList();
                if (searchText == null || searchText == "___")
                {
                    list = list.OrderBy(x => x.Title).Take(50).ToList();
                }
                else
                {
                    list = list.Where(x => x.Title.ToLower().Contains(searchText.Trim().ToLower())).OrderBy(x => x.Title).Take(50).ToList();
                }
                resultList = (from c in list.ToList()
                              select new GlobalDropdownVm()
                              {
                                  Id = c.Id,
                                  Name = c.Title,
                              }).ToList();
            }
            return new ServiceResult<List<GlobalDropdownVm>>()
            {
                Data = resultList,
                Status = ResultStatus.Ok
            };
        }


        [HttpPost]
        public ServiceResult<List<GlobalDropdownVm>> GetStudentAutoComplete(KendoAutoCompleteVm model)
        {
            List<GlobalDropdownVm> resultList = new List<GlobalDropdownVm>();


            if (model != null)
            {
                string searchText = model.Filter.Filters.Count() > 0 ? model.Filter.Filters[0].Value : "";
                var list = _studentInformationServices.List().Data.ToList();
                if (searchText == null || searchText == "___")
                {
                    list = list.OrderBy(x => x.FirstName).Take(50).ToList();
                }
                else
                {
                    list = list.Where(x => x.FirstName.ToLower().Contains(searchText.Trim().ToLower())).OrderBy(x => x.FirstName).Take(50).ToList();
                }
                resultList = (from c in list.ToList()
                              select new GlobalDropdownVm()
                              {
                                  Id = c.Id,
                                  Name = c.FirstName + " " + c.MiddleName + " " + c.LastName,
                              }).ToList();
            }
            return new ServiceResult<List<GlobalDropdownVm>>()
            {
                Data = resultList,
                Status = ResultStatus.Ok
            };
        }


        [HttpPost]
        public ServiceResult<AssignMockTestAndPackageVm> Post(AssignMockTestAndPackageVm vm)
        {
            if (vm.StudentId != 0)
            {
                if (vm.MockTests != null)
                {
                    AssignMockTest(vm.StudentId, vm.MockTests);
                }
                if (vm.Packages != null)
                {
                    AssignPackages(vm.StudentId, vm.Packages);
                }

                return new ServiceResult<AssignMockTestAndPackageVm>()
                {
                    Data = vm,
                    Message = "Assigned Successfully",
                    Status = ResultStatus.Ok
                };
            }

            return new ServiceResult<AssignMockTestAndPackageVm>()
            {
                Data = vm,
                Message = "Sorry, Something went wrong !",
                Status = ResultStatus.processError
            };

        }

        private void AssignPackages(int studentId, List<NaatiPackageVm> packages)
        {
            SMockTestPurchasedPackages _mockTestPurchasedPackagesServices = new SMockTestPurchasedPackages();

            foreach (var item in packages)
            {
                AddPackagepaymentDetailForPackage(studentId, item);
                EMockTestPurchasedPackages packageData = new EMockTestPurchasedPackages()
                {
                    StudentId = studentId,
                    MockTestPackageId = item.Id,
                    IsUnscored = false,
                    PurchaseDate = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(item.Duration),
                    IsTestTaken = false
                };
                _mockTestPurchasedPackagesServices.Add(packageData);

            }
        }

        private void AssignMockTest(int studentId, List<NaatiMockTestVm> mockTests)
        {
            SMockTestPurchasedPackages _mockTestPurchasedPackagesServices = new SMockTestPurchasedPackages();

            foreach (var item in mockTests)
            {

                AddPackagepaymentDetailForMockTest(studentId, item);

                EMockTestPurchasedPackages packageData = new EMockTestPurchasedPackages()
                {
                    StudentId = studentId,
                    MockTestPackageId = item.Id,
                    IsUnscored = false,
                    PurchaseDate = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(item.Duration),
                    IsTestTaken = false
                };
                _mockTestPurchasedPackagesServices.Add(packageData);
            }

        }

        private void AddPackagepaymentDetailForMockTest(int studentId, NaatiMockTestVm item)
        {
            SPackagePaymentDetails _packagePaymentDetailsServices = new SPackagePaymentDetails();
            EPackagePaymentDetails model = new EPackagePaymentDetails()
            {
                UserId = studentId,
                PaymentFor = PaymentFor.NaatiMocktest,
                CourseCode = item.Code,
                OrderId = _commonServices.GenerateOrderId(),
                TransactionId = _commonServices.GenerateTransactionId(),
                TransactionDateTime = DateTime.Now,
                CardType = "Admin",
                CardNumber = "",
                Amount = item.Price,
                Currency = "",
                CardIssuer = "",
                ReceiptNumber = "",
                acquirerTransactionId = "",
                StartDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(item.Duration),
                NameOnCard = "",
                Country = "",
                ZipCode = ""
            };
            _packagePaymentDetailsServices.Add(model);
        }
        private void AddPackagepaymentDetailForPackage(int studentId, NaatiPackageVm item)
        {
            SPackagePaymentDetails _packagePaymentDetailsServices = new SPackagePaymentDetails();
            EPackagePaymentDetails model = new EPackagePaymentDetails()
            {
                UserId = studentId,
                PaymentFor = PaymentFor.NaatiPackage,
                CourseCode = item.Code,
                OrderId = _commonServices.GenerateOrderId(),
                TransactionId = _commonServices.GenerateTransactionId(),
                TransactionDateTime = DateTime.Now,
                CardType = "Admin",
                CardNumber = "",
                Amount = item.Price,
                Currency = "",
                CardIssuer = "",
                ReceiptNumber = "",
                acquirerTransactionId = "",
                StartDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(item.Duration),
                NameOnCard = "",
                Country = "",
                ZipCode = ""
            };
            _packagePaymentDetailsServices.Add(model);
        }

        #endregion
    }
}