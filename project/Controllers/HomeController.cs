using project.DAL;
using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project.Controllers
{
    public class HomeController : Controller
    {

        //cac home controller se phai check exist session va check authorization tu list
        public ActionResult Index()
        {
            return View();
        }
      
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public JsonResult search(string key)
        {
            using (EducationDBContext db = new EducationDBContext() )
            {
                var lst = (from l in db.Lessions where l.Name.Contains(key) select l).ToList();

                return Json(new { data = lst}, JsonRequestBehavior.AllowGet);

            }
        }

    }
}