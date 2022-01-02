using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services
{
    public class SCompany
    {
        RiddhaDBContext db = null;
        private bool _proxy = true;
        public SCompany()
        {
            db = new RiddhaDBContext();
        }
        public SCompany(bool proxy)
        {
            this._proxy = proxy;
            db = new RiddhaDBContext();
            db.Configuration.ProxyCreationEnabled = false;
        }
        public Riddhasoft.Services.Common.ServiceResult<IQueryable<ECompany>> List()
        {
            return new Riddhasoft.Services.Common.ServiceResult<IQueryable<ECompany>>()
            {
                Data = db.Company,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public Riddhasoft.Services.Common.ServiceResult<ECompany> Add(ECompany model)
        {
            db.Company.Add(model);
            db.SaveChanges();
            db.Branch.Add(new EBranch() {Address=model.Address,AddressNp=model.AddressNp,Code=model.Code,CompanyId=model.Id,ContactNo=model.ContactNo,ContactPerson=model.ContactPerson,Email=model.Email,IsDeleted=model.IsDeleted,IsHeadOffice=true,LogoUrl=model.LogoUrl,Name=model.Name,NameNp=model.NameNp,PAN=model.PAN,WebUrl=model.WebUrl});
            db.SaveChanges();
            return new ServiceResult<ECompany>()
            {
                Data = model,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }
        public Riddhasoft.Services.Common.ServiceResult<ECompany> Update(ECompany model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            var branch = db.Branch.Where(x => x.CompanyId == model.Id).FirstOrDefault();
            branch.Address = model.Address;
            branch.AddressNp = model.AddressNp;
            branch.Code = model.Code;
            branch.ContactNo = model.ContactNo;
            branch.ContactPerson = model.ContactPerson;
            branch.Email = model.Email;
            branch.LogoUrl = model.LogoUrl;
            branch.Name = model.Name;
            branch.NameNp = model.NameNp;
            branch.PAN = model.PAN;
            branch.WebUrl = model.WebUrl;
            db.Entry(branch).State = EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ECompany>()
            {
                Data = model,
                Message = "UpdatedSuccess",
                Status = ResultStatus.Ok
            };
        }
        public Riddhasoft.Services.Common.ServiceResult<int> Remove(int  id)
        {
            try
            {
                var company = db.Company.Find(id);
                company.IsDeleted = true;
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();

                var branch = db.Branch.Where(x => x.CompanyId == id).FirstOrDefault();
                branch.IsDeleted = true;
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                return new ServiceResult<int>()
                {
                    Data = 1,
                    Message = "Removed Successfully",
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

        public ServiceResult<IQueryable<ECompanyLicense>> ListCompanyLicense()
        {
            return new ServiceResult<IQueryable<ECompanyLicense>>()
            {
                Data = db.CompanyLicense,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<ECompanyLicense> AddCompanyLicense(ECompanyLicense model)
        {
            db.CompanyLicense.Add(model);
            db.CompanyLicenseLog.Add(new ECompanyLicenseLog() { CompanyId = model.CompanyId, IssueDate = model.IssueDate, LicensePeriodInDays = model.LicensePeriodInDays});
            db.SaveChanges();
            return new ServiceResult<ECompanyLicense>()
            {
                Data = model,
                Message = "AddedSuccess",
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<ECompanyLicense> UpdateCompanyLicense(ECompanyLicense model,int renewDays)
        {
            db.Entry(model).State = EntityState.Modified;
            db.CompanyLicenseLog.Add(new ECompanyLicenseLog() { CompanyId = model.CompanyId, IssueDate = DateTime.Now, LicensePeriodInDays = renewDays});
            db.SaveChanges();
            return new ServiceResult<ECompanyLicense>()
            {
                Data = model,
                Message = "AddedSuccess",
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<IQueryable<EBranch>> ListBranch()
        {
            return new ServiceResult<IQueryable<EBranch>>()
            {
                Data = db.Branch,
                Status = ResultStatus.Ok
            };
        }
    }
}
