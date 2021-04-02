using project.DAL;
using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project.Controllers
{
    public class SearchController : Controller
    {
        private EducationDBContext db = new EducationDBContext();
        // GET: Search
        public ActionResult Index(string search)
        {
            var result = (from r in db.Lessions select r);
            if (!String.IsNullOrEmpty(search))
            {
                result = result.Where(r => r.Name.Contains(search));
            }
            return View(result);
        }
    }
}