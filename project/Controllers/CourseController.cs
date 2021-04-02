using project.DAL;
using project.Filter;
using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project.Controllers
{
    [NitishAuthentication]
    [FilterAuthorize(1, 2)]
    public class CourseController : Controller
    {
        private EducationDBContext db = new EducationDBContext();


        public ActionResult Course()
        {

            var course = (from s in db.Courses select s).ToList();
            ViewBag.courses = course;
            return View();
        }

    }
}