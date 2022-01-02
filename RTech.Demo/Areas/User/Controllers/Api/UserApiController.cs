using Riddhasoft.Services.Common;
using Riddhasoft.Services.User;
using Riddhasoft.User.Entity;
using RTech.Demo.Models;
using RTech.Demo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTech.Demo.Areas.User.Controllers.Api
{
    public class UserApiController : ApiController
    {
        SUser userServices = null;
        LocalizedString loc = null;
        public UserApiController()
        {
            userServices = new SUser();
            loc = new LocalizedString();
        }
        public ServiceResult<List<UserGridVm>> Get()
        {
            SUser service = new SUser();
            var userLst = (from c in service.List().Data.Where(x => x.Branch.CompanyId == RiddhaSession.CompanyId && x.IsDeleted==false)
                           select new UserGridVm()
                               {
                                   Id = c.Id,
                                   CompanyId = c.Branch.CompanyId,
                                   FullName = c.FullName,
                                   IsSuspended = c.IsSuspended,
                                   Name = c.Name,
                                   Password = c.Password,
                                   PhotoURL = c.PhotoURL,
                                   RoleId = c.RoleId
                               }).ToList();

            return new ServiceResult<List<UserGridVm>>()
            {
                Data = userLst,
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<EUser> Get(int id)
        {
            EUser user = userServices.List().Data.Where(x => x.Id == id).FirstOrDefault();
            return new ServiceResult<EUser>()
            {
                Data = user,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<EUser> Post([FromBody]UserVm model)
        {
            return new ServiceResult<EUser>()
            {
            };
        }
        public ServiceResult<EUser> Put([FromBody]UserVm model)
        {
            return new ServiceResult<EUser>()
            {
            };


        }
        [HttpDelete]
        public ServiceResult<int> Delete(int id)
        {
            return new ServiceResult<int>()
            {
            };
        }
        [HttpGet]
        public ServiceResult<List<DropdownViewModel>> GetRolesForDropdown()
        {
            string language = RiddhaSession.Language.ToString();
            int companyId = RiddhaSession.CompanyId;
            SUserRole roleServices = new SUserRole();
            List<DropdownViewModel> resultLst = (from c in roleServices.List().Data.Where(x => x.CompanyId == companyId).ToList()
                                                 select new DropdownViewModel()
                                                 {
                                                     Id = c.Id,
                                                     Name = language == "ne" &&
                                                     c.NameNp != null ? c.NameNp : c.Name
                                                 }
                                                   ).ToList();
            return new ServiceResult<List<DropdownViewModel>>()
            {
                Data = resultLst,
                Status = ResultStatus.Ok
            };
        }

        [HttpPost]
        public KendoGridResult<List<UserGridVm>> GetUserKendoGrid()
        {
            var companyId = RiddhaSession.CompanyId;
            SUser userService = new SUser();
            List<EUser> users = userService.List().Data.Where(x => x.Branch.CompanyId == companyId && x.IsDeleted==false).ToList();
            var userLst = (from c in users
                           select new UserGridVm()
                           {
                               Id = c.Id,
                               FullName = c.FullName==null?"":c.FullName,
                               IsSuspended = c.IsSuspended,
                               Name = c.Name,
                               Password = c.Password,
                               PhotoURL = c.PhotoURL==null?"":c.PhotoURL,
                               RoleId = c.RoleId,
                               RoleName = c.Role==null?"":c.Role.Name
                           }).ToList();
            return new KendoGridResult<List<UserGridVm>>()
            {
                Data = userLst,
                Status = ResultStatus.Ok,
            };
        }
    }

    public class UserGridVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public int CompanyId { get; set; }
        public string BranchName { get; set; }
        public string FullName { get; set; }
        public string PhotoURL { get; set; }
        public bool IsSuspended { get; set; }
        public int? EmpId { get; set; }
        public string EmpName { get; set; }
    }
    public class UserVm
    {
        public EUser User { get; set; }
        public int EmpId { get; set; }
    }
}
