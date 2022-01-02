using RTech.Demo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Controllers
{
    public class FileUploadController : Controller
    {
        [HttpPost]
        public JsonResult Index()
        {
            if (Request.Files.Count > 0)
            {
                //TODO: Delete Raw images and files before save scan file before save for antivirus security
                var File = Request.Files[0];
                var id = Guid.NewGuid();
                var fileSpec = @"/Images/File/" + id + "." + File.FileName.Split('.')[1];
                string filePath = Server.MapPath(@"/Images/File/") + id + "." + File.FileName.Split('.')[1];
                File.SaveAs(filePath);
                return new JsonResult() { Data = fileSpec };
            }
            else
            {
                return new JsonResult() { Data = null };

            }
        }
    }
}