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
    public class FileManagerApiController : ApiController
    {
        SFolder _sfolderServices = null;

        public FileManagerApiController()
        {
            _sfolderServices = new SFolder();
        }


        public ServiceResult<List<EFolder>> Get()
        {
            var result = _sfolderServices.List().Data.ToList();
            return new ServiceResult<List<EFolder>>()
            {
                Data = result,
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<EFolder> Get(int id)
        {
            EFolder folder = _sfolderServices.List().Data.Where(x => x.Id == id).FirstOrDefault();
            return new ServiceResult<EFolder>()
            {
                Data = folder,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<EFolder> Post([FromBody]EFolder model)
        {
            return _sfolderServices.Add(model);
        }
        public ServiceResult<EFolder> Put([FromBody]EFolder model)
        {
            return _sfolderServices.Update(model);
        }
        public ServiceResult<int> Delete(int id)
        {
            var itemGroup = _sfolderServices.List().Data.Where(x => x.Id == id).FirstOrDefault();
            return _sfolderServices.Remove(itemGroup);
        }
        [HttpGet]
        public ServiceResult<IEnumerable<ItemGroupKendoTreeViewModel>> GetFolderGroupTreeLst()
        {
            var data = _sfolderServices.List().Data;
            IEnumerable<ItemGroupKendoTreeViewModel> iTreeViewData = (from c in data
                                                                      select new ItemGroupKendoTreeViewModel()
                                                                      {
                                                                          id = c.Id,
                                                                          parentId = c.ParentId,
                                                                          text = c.Name
                                                                      });
            if (iTreeViewData.Count() > 0)
            {
                iTreeViewData = BuildTree(iTreeViewData);
            }
            return new ServiceResult<IEnumerable<ItemGroupKendoTreeViewModel>>()
            {
                Data = iTreeViewData,
                Status = ResultStatus.Ok
            };
        }
        private IList<ItemGroupKendoTreeViewModel> BuildTree(IEnumerable<ItemGroupKendoTreeViewModel> source)
        {
            var groups = source.GroupBy(i => i.parentId);

            var roots = groups.FirstOrDefault(g => g.Key.HasValue == false).ToList();

            if (roots.Count > 0)
            {
                var dict = groups.Where(g => g.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.ToList());
                for (int i = 0; i < roots.Count; i++)
                    AddChildren(roots[i], dict);
            }

            return roots;
        }
        private void AddChildren(ItemGroupKendoTreeViewModel node, IDictionary<int, List<ItemGroupKendoTreeViewModel>> source)
        {
            if (source.ContainsKey(node.id))
            {
                node.items = source[node.id];
                for (int i = 0; i < node.items.Count; i++)
                    AddChildren(node.items[i], source);
            }
            else
            {
                node.items = new List<ItemGroupKendoTreeViewModel>();
            }
        }
    }

    public class ItemGroupKendoTreeViewModel
    {
        public int id { get; set; }
        public string text { get; set; }
        public int? parentId { get; set; }
        public bool hasChildren { get; set; }
        public string spriteCssClass { get { return "folder"; } }
        public List<ItemGroupKendoTreeViewModel> items { get; set; }
    }
}
