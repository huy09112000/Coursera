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
            //if (checkAuthentication())
            //{
            var course = (from s in db.Courses select s).ToList();
            foreach (Course item in course)
            {
                item.rate = item.rate.HasValue? (((float)item.rate / 5.0) * 100) : 0;
            }
            ViewBag.courses = course;
            return View();
            //}
            //else
            //{
            //    ViewBag.MyMessage = "You need to login as a Teacher account or Student account to have permission to acces this page";
            //    return View("Error");
            //}
        }

    }
}