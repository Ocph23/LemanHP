using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LemanWeb.Controllers
{
    public class DownloadController : Controller
    {
        // GET: Download
        public ActionResult Index()
        {
            return View();
        }


        public FileResult Download()
        {
            string rootpath = Server.MapPath("~/AndroidApp/com.ocph23.LemanHP-Signed.apk");
            byte[] fileBytes = System.IO.File.ReadAllBytes(rootpath);
            string fileName = "LemanHP.apk";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}