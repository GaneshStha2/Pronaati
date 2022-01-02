using Riddhasoft.Services.Common;
using Riddhasoft.Services.User;
using Riddhasoft.Setup.Entity;
using Riddhasoft.Setup.Services;
using Riddhasoft.User.Entity;
using RTech.Demo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTech.Demo.Areas.Setup.Controllers.Api
{
    public class CompanyApiController : ApiController
    {
        SCompany companyServices = null;
        SUser userServices = null;
        LocalizedString loc = new LocalizedString();
        public CompanyApiController()
        {
            companyServices = new SCompany();
            userServices = new SUser();
        }
        public ServiceResult<List<CompanyGridVm>> Get()
        {
            List<CompanyGridVm> resultLst = new List<CompanyGridVm>();
            var userCompanyList = companyServices.List().Data.Where(x => x.IsDeleted == false).ToList();
            var userQuery = userServices.List().Data;
            foreach (var item in userCompanyList)
            {
                CompanyGridVm vm = new CompanyGridVm();
                vm.Id = item.Id;
                vm.Code = item.Code;
                vm.Name = item.Name;
                vm.Address = item.Address;
                vm.ContactNo = item.ContactNo;
                vm.ContactPerson = item.ContactPerson;
                vm.Email = item.Email;
                vm.PAN = item.PAN;
                vm.WebUrl = item.WebUrl;
                vm.LogoUrl = item.LogoUrl;
                vm.Status = getCompanyStatus(userQuery, item.Id);
                resultLst.Add(vm);
            }

            return new ServiceResult<List<CompanyGridVm>>()
            {
                Data = resultLst,
                Status = ResultStatus.Ok
            };
        }
        [HttpPost]
        public KendoGridResult<List<CompanyGridVm>> GetCompanyKendoGrid()
        {
            List<CompanyGridVm> resultLst = new List<CompanyGridVm>();
            var userCompanyList = companyServices.List().Data.Where(x => x.IsDeleted == false).ToList();
            var userQuery = userServices.List().Data;
            foreach (var item in userCompanyList)
            {
                CompanyGridVm vm = new CompanyGridVm();
                vm.Id = item.Id;
                vm.Code = item.Code;
                vm.Name = item.Name;
                vm.Address = item.Address;
                vm.ContactNo = item.ContactNo;
                vm.ContactPerson = item.ContactPerson;
                vm.Email = item.Email;
                vm.PAN = item.PAN;
                vm.WebUrl = item.WebUrl;
                vm.LogoUrl = item.LogoUrl;
                vm.Status = getCompanyStatus(userQuery, item.Id);
                resultLst.Add(vm);
            }
            return new KendoGridResult<List<CompanyGridVm>>()
            {
                Data = resultLst,
                Status = ResultStatus.Ok,
            };
        }
        private string getCompanyStatus(IQueryable<EUser> userQuery, int companyId)
        {
            var user = userQuery.Where(x => x.Branch.CompanyId == companyId && x.Branch.IsHeadOffice).FirstOrDefault();
            if (user == null)
            {
                return "New";
            }
            return user.IsSuspended == true ? "Suspended" : "Approved";
        }
        public ServiceResult<ECompany> Get(int id)
        {
            var company = companyServices.List().Data.Where(x => x.Id == id).FirstOrDefault();
            return new ServiceResult<ECompany>()
            {
                Data = company,
                Status = ResultStatus.Ok
            };
        }
        [HttpGet]
        public ServiceResult<DateTime> GetLicenseExpiryDate(int period, DateTime issueDate)
        {
            DateTime expiryDate = issueDate.Date.AddYears(period);
            return new ServiceResult<DateTime>()
            {
                Data = expiryDate,
                Status = ResultStatus.Ok
            };
        }
        [HttpGet]
        public bool CheckDuplicateSNo(string code)
        {
            return companyServices.List().Data.Where(x => x.Code == code).Any();
        }
        [HttpGet]
        public bool CheckDuplicateEmail(string email)
        {
            return companyServices.List().Data.Where(x => x.Email == email).Any();
        }
        public ServiceResult<ECompany> Post([FromBody]ECompany model)
        {
            var result = companyServices.Add(model);
            return new ServiceResult<ECompany>()
            {
                Data = result.Data,
                Message = loc.Localize(result.Message),
                Status = result.Status
            };
        }
        public ServiceResult<ECompany> Put([FromBody]ECompany model)
        {
            var result = companyServices.Update(model);
            return new ServiceResult<ECompany>()
            {
                Data = result.Data,
                Message = loc.Localize(result.Message),
                Status = result.Status
            };
        }
        [HttpDelete]
        public ServiceResult<int> Delete(int id)
        {
            var result = new ServiceResult<int>();
            var user = userServices.List().Data.Where(x => x.Branch.CompanyId == id && x.Branch.IsHeadOffice).FirstOrDefault();
            if (user == null)
            {
                result = companyServices.Remove(id);
            }
            else
            {
                if (user.IsSuspended == false)
                {
                    result.Data = 0;
                    result.Message = loc.Localize("Approved Company Can not be deleted ");
                    result.Status = ResultStatus.processError; ;
                }
                else
                {
                    result = companyServices.Remove(id);
                    if (result.Status == ResultStatus.Ok)
                    {
                        userServices.Remove(user);
                    }
                }
            }
            return new ServiceResult<int>()
            {
                Data = result.Data,
                Message = loc.Localize(result.Message),
                Status = result.Status
            };
        }
        [HttpGet]
        public ServiceResult<int> SuspendOrApproveCompany(int id)
        {
            var user = userServices.List().Data.Where(x => x.Branch.CompanyId == id && x.Branch.IsHeadOffice).FirstOrDefault();
            if (user == null)
            {
                return new ServiceResult<int>
                {
                    Data = 0,
                    Status = ResultStatus.Ok,
                    Message = "Register Company Login First"
                };
            }
            else
            {
                if (user.IsSuspended)
                {
                    user.IsSuspended = false;
                    userServices.Update(user);
                    return new ServiceResult<int>
                    {
                        Data = 1,
                        Status = ResultStatus.Ok,
                        Message = "Approved Successfully"
                    };
                }
                else
                {
                    user.IsSuspended = true;
                    userServices.Update(user);
                    return new ServiceResult<int>
                    {
                        Data = 1,
                        Status = ResultStatus.Ok,
                        Message = "Suspended Successfully"
                    };
                }
            }
        }
        [HttpPut]
        public ServiceResult<bool> UpdateCompanyProfile(ECompany model)
        {

            ECompany company = companyServices.List().Data.Where(x => x.Id == model.Id).FirstOrDefault();
            company.Address = model.Address;
            company.AddressNp = model.AddressNp;
            company.Code = model.Code;
            company.ContactNo = model.ContactNo;
            company.ContactPerson = model.ContactPerson;
            company.Email = model.Email;
            company.LogoUrl = model.LogoUrl;
            company.Name = model.Name;
            company.NameNp = model.NameNp;
            company.PAN = model.PAN;
            company.WebUrl = model.WebUrl;
            var result = companyServices.Update(company);
            return new ServiceResult<bool>()
            {
                Data = true,
                Message = loc.Localize(result.Message),
                Status = result.Status
            };
        }
        [HttpGet]
        public ServiceResult<CompanyLogin> GetCompanyLogin(int companyId)
        {
            CompanyLogin model = new CompanyLogin();
            var user = userServices.List().Data.Where(x => x.Branch.CompanyId == companyId && x.Branch.IsHeadOffice).FirstOrDefault();
            if (user != null)
            {
                model.CompanyId = user.Branch.CompanyId;
                model.UserName = user.Name;
                model.Password = user.Password;
            }
            return new ServiceResult<CompanyLogin>()
            {
                Data = model,
                Status = ResultStatus.Ok
            };
        }
        [HttpPost]
        public ServiceResult<EUser> RegisterCompanyLogin(CompanyLogin model)
        {
            var result = new ServiceResult<EUser>();
            var user = userServices.List().Data.Where(x => x.Branch.CompanyId == model.CompanyId && x.Branch.IsHeadOffice).FirstOrDefault();
            if (user == null)
            {
                var branch = companyServices.ListBranch().Data.Where(x => x.CompanyId == model.CompanyId && x.IsHeadOffice).FirstOrDefault();
                result = userServices.Add(new EUser() { BranchId = branch.Id, FullName = "Admin", IsDeleted = false, IsSuspended = false, Name = model.UserName, Password = model.Password, RoleId = null, UserType = UserType.User });
            }
            else
            {
                user.Name = model.UserName;
                user.Password = model.Password;
                result = userServices.Update(user);
            }
            return new ServiceResult<EUser>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }
        [HttpGet]
        public ServiceResult<CompanyLicense> GetCompanyLicense(int companyId)
        {
            var user = userServices.List().Data.Where(x => x.Branch.CompanyId == companyId && x.Branch.IsHeadOffice).FirstOrDefault();
            if (user == null)
            {
                return new ServiceResult<CompanyLicense>()
                {
                    Data = null,
                    Message = "Register company Login first to issue license",
                    Status = ResultStatus.processError
                };
            }
            CompanyLicense model = new CompanyLicense();
            var license = companyServices.ListCompanyLicense().Data.Where(x => x.CompanyId == companyId).FirstOrDefault();
            if (license != null)
            {
                model.CompanyId = license.CompanyId;
                model.ExpiryDate = license.ExpiryDate.ToString("yyyy/MM/dd");
                model.IssueDate = license.IssueDate.ToString("yyyy/MM/dd");
                model.LicensePeriodInDays = license.LicensePeriodInDays;
            }
            return new ServiceResult<CompanyLicense>()
            {
                Data = model,
                Status = ResultStatus.Ok
            };
        }
        [HttpPost]
        public ServiceResult<ECompanyLicense> IssueCompanyLicense(CompanyLicense model)
        {
            var result = new ServiceResult<ECompanyLicense>();
            var license = companyServices.ListCompanyLicense().Data.Where(x => x.CompanyId == model.CompanyId).FirstOrDefault();
            if (license == null)
            {
                result = companyServices.AddCompanyLicense(new ECompanyLicense() { CompanyId = model.CompanyId, ExpiryDate = model.ExpiryDate.ToDateTime(), IssueDate = model.IssueDate.ToDateTime(), LicensePeriodInDays = model.LicensePeriodInDays });
            }
            else
            {
                license.ExpiryDate = model.ExpiryDate.ToDateTime();
                license.LicensePeriodInDays = license.LicensePeriodInDays + model.RenewDays;
                result = companyServices.UpdateCompanyLicense(license, model.RenewDays);
            }
            return new ServiceResult<ECompanyLicense>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }
        [HttpGet]
        public ServiceResult<ECompany> GetCompanyProfile()
        {
            var company = companyServices.List().Data.Where(x => x.Id == RiddhaSession.CompanyId).FirstOrDefault();
            return new ServiceResult<ECompany>()
            {
                Data = company,
                Status = ResultStatus.Ok
            };
        }
        private bool checkValidString(string value)
        {
            return !(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value));
        }
    }
    public class CompanyGridVm
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameNp { get; set; }
        public string Address { get; set; }
        public string AddressNp { get; set; }
        public string ContactNo { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonNp { get; set; }
        public string Email { get; set; }
        public string WebUrl { get; set; }
        public string PAN { get; set; }
        public string LogoUrl { get; set; }
        public string Status { get; set; }
    }
    public class CompanyLicense
    {
        public int CompanyId { get; set; }
        public int LicensePeriodInDays { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public int RenewDays { get; set; }
    }
    public class CompanyLogin
    {
        public int CompanyId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
