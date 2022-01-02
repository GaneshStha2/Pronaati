using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity;
using Riddhasoft.Setup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTech.Demo.Areas.Setup.Controllers.Api
{
    public class FilesApiController : ApiController
    {

        SFiles _filesServices = null;
        SFolder _folderServices = null;
        Common.CommonServices _commonServices = null;
        public FilesApiController()
        {
            _filesServices = new SFiles();
            _folderServices = new SFolder();
            _commonServices = new Common.CommonServices();
        }

        public ServiceResult<List<GlobalDropdownVm>> GetFolders()
        {

            var result = (from c in _folderServices.List().Data
                          select new GlobalDropdownVm()
                          {

                              Id = c.Id,
                              Name = c.Name,
                          }).ToList();
            return new ServiceResult<List<GlobalDropdownVm>>()
            {
                Data = result,
                Status = ResultStatus.Ok
            };

        }


        public ServiceResult<List<FilesVm>> Get()
        {
            var result = (from c in _filesServices.List().Data
                          select new FilesVm()
                          {
                              Id = c.Id,
                              Name = c.Name,
                              FolderId = c.FolderId,
                              URL = c.URL
                          }).ToList();
            return new ServiceResult<List<FilesVm>>()
            {

                Data = result,
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<List<FilesVm>> GetFilesByFolder(int FolderId)
         {

            var result = (from c in _filesServices.List().Data.Where(x => x.FolderId == FolderId).ToList()
                          select new FilesVm()
                          {
                              Id = c.Id,
                              Name = c.Name,
                              FolderId = c.FolderId,
                              FileExtension = Common.CommonServices.GetExtension(c.URL),
                              FileExtensionIconPath = Common.CommonServices.GetIconForFile(c.URL),
                              URL = c.URL
                          }).ToList();
            return new ServiceResult<List<FilesVm>>()
            {

                Data = result,
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<EFiles> Post(EFiles model)
        {

            var result = _filesServices.Add(model);

            return result;
        }

        public ServiceResult<EFiles> Put(EFiles model)
        {

            var result = _filesServices.Update(model);
            return result;
        }


        [HttpDelete]
        public ServiceResult<int> Delete(int Id)
        {

            var data = _filesServices.List().Data.Where(x => x.Id == Id).FirstOrDefault();
            var result = _filesServices.Remove(data);
            return result;
        }

    }

    public class GlobalDropdownVm
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class KendoAutoCompleteVm : EKendoAutoComplete
    {
        public string SearchParam { get; set; }
    }

    public class FilesVm
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int FolderId { get; set; }
        public string URL { get; set; }
        public string FileExtension { get; set; }
        public string FileExtensionIconPath { get; set; }
    }
}
