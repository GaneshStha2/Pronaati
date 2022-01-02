using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.MockTest.Controllers
{
    public class AudioUploadController : Controller
    {
        // GET: MockTest/AudioUpload
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult PostRecordedAudioVideo()
        {
            var path = Server.MapPath("/AudioFiles");
            foreach (string upload in Request.Files)
            {

                var file = Request.Files[upload];
                if (file == null) continue;

                file.SaveAs(Path.Combine(path, Request.Form[0]));
            }
            return Json(path);
        }

    }
}