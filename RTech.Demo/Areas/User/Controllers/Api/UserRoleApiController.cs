using Riddhasoft.Entity.User;
using Riddhasoft.Services.Common;
using Riddhasoft.Services.User;
using Riddhasoft.User.Entity;
using RTech.Demo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RTech.Demo.Extension;
namespace RTech.Demo.Areas.User.Controllers.Api
{
    public class UserRoleApiController : ApiController
    {
        SUserRole userRoleServices = null;
        LocalizedString loc = null;

        public UserRoleApiController()
        {
            userRoleServices = new SUserRole();
            loc = new LocalizedString();
        }
        public ServiceResult<List<UserRoleGridVm>> Get()
        {
            SUserRole service = new SUserRole();
            int companyId = RiddhaSession.CompanyId;
            if (companyId == null)
            {
                var owneruserRoleLst = (from c in service.List().Data.Where(x => x.CompanyId == null)
                                        select new UserRoleGridVm()
                                        {
                                            Id = c.Id,
                                            CompanyId = 0,
                                            Name = c.Name,
                                            NameNp = c.NameNp,
                                            Priority = c.Priority
                                        }).ToList();
                return new ServiceResult<List<UserRoleGridVm>>()
                {
                    Data = owneruserRoleLst,
                    Status = ResultStatus.Ok
                };
            }
            var userRoleLst = (from c in service.List().Data.Where(x => x.CompanyId == companyId)
                               select new UserRoleGridVm()
                                 {
                                     Id = c.Id,
                                     CompanyId = c.CompanyId,
                                     Name = c.Name,
                                     NameNp = c.NameNp,
                                     Priority = c.Priority
                                 }).ToList();

            return new ServiceResult<List<UserRoleGridVm>>()
            {
                Data = userRoleLst,
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<EUserRole> Get(int id)
        {
            EUserRole userRole = userRoleServices.List().Data.Where(x => x.Id == id).FirstOrDefault();
            return new ServiceResult<EUserRole>()
            {
                Data = userRole,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }
        [HttpGet]
        public ServiceResult<int> GetDataVisibilityLevel(int roleId)
        {
            int dataVisibilityLevel = userRoleServices.GetDataVisibilityLevel(roleId).Data;
            return new ServiceResult<int>()
            {
                Data = dataVisibilityLevel,

            };
        }
        [HttpPost]
        public ServiceResult<EUserRole> Post([FromBody]EUserRole model)
        {
            model.CompanyId = RiddhaSession.CompanyId;
            var result = userRoleServices.Add(model);
            return new ServiceResult<EUserRole>()
            {
                Data = result.Data,
                Message = loc.Localize(result.Message),
                Status = result.Status
            };
        }
        [HttpPost]
        public ServiceResult<EUserGroupDataVisibility> CreateDataVisibility([FromBody]EUserGroupDataVisibility model)
        {
            var result = userRoleServices.UpdateDataVisibility(model);
            return new ServiceResult<EUserGroupDataVisibility>()
            {
                Message = loc.Localize(result.Message),
                Status = result.Status
            };
        }
        [HttpGet]
        public ServiceResult<string[]> GetRolePermissions(int roleId)
        {
            var actionRights = userRoleServices.ListActionRights().Data.Where(x => x.RoleId == roleId);
            string[] result = (from c in actionRights
                               select c.MenuAction.ActionCode).ToArray();
            return new ServiceResult<string[]>()
            {
                Data = result,
                Status = ResultStatus.Ok
            };

        }
        [HttpPost]
        public ServiceResult<PermissionViewModel> PermitRoles(PermissionViewModel model)
        {
            List<EUserGroupMenuRight> menuRights = new List<EUserGroupMenuRight>();
            List<EUserGroupActionRight> actionRights = new List<EUserGroupActionRight>();
            if (model.TreeViewLst != null)
            {
                menuRights = (from c in model.TreeViewLst.Where(x => x.hasChildren == false)
                              join d in userRoleServices.ListMenus().Data on c.parentId equals d.Code
                              select new EUserGroupMenuRight()
                              {
                                  MenuId = d.Id,
                                  RoleId = model.RoleId
                              }).DistinctBy(x => x.MenuId).ToList();
                actionRights = (from c in model.TreeViewLst.Where(x => x.hasChildren == false)
                                join d in userRoleServices.ListMenuActions().Data on c.id equals d.ActionCode
                                select new EUserGroupActionRight()
                                {
                                    MenuActionId = d.Id,
                                    RoleId = model.RoleId
                                }).ToList();
            }
            var result = userRoleServices.PermitRoles(menuRights, actionRights, model.RoleId);
            return new ServiceResult<PermissionViewModel>()
            {
                Message = loc.Localize(result.Message),
                Status = result.Status
            };
        }

        public ServiceResult<EUserRole> Put([FromBody]EUserRole model)
        {
            model.CompanyId = RiddhaSession.CompanyId;
            var result = userRoleServices.Update(model);
            return new ServiceResult<EUserRole>()
            {
                Data = result.Data,
                Message = loc.Localize(result.Message),
                Status = result.Status
            };
        }
        [HttpDelete]
        public ServiceResult<int> Delete(int id)
        {
            var userRole = userRoleServices.List().Data.Where(x => x.Id == id).FirstOrDefault();
            var result = userRoleServices.Remove(userRole);
            return new ServiceResult<int>()
            {
                Data = result.Data,
                Message = loc.Localize(result.Message),
                Status = result.Status
            };
        }
    }

    public class UserRoleGridVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameNp { get; set; }
        public int Priority { get; set; }
        public int CompanyId { get; set; }
    }
    public class PermissionViewModel
    {
        public int RoleId { get; set; }
        public List<KendoTreeViewModel> TreeViewLst { get; set; }
    }
    public class KendoTreeViewModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public bool hasChildren { get; set; }
        public string parentId { get; set; }
    }

}
