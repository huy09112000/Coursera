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

    public class CourseController : Controller
    {
        private EducationDBContext db = new EducationDBContext();
        // GET: Course
        public bool checkAuthentication()
        {
            string absolutepath = HttpContext.Request.Url.AbsolutePath;
            //check authorize
            User user = (User)Session["account"];
            if (user == null)
            {
                return false;
            }
            foreach (var item in user.features)
            {
                if (item.Url.Equals(absolutepath))
                {
                    //if equal
                    return true;
                }
            }
            return false;
        }

        public ActionResult Course()
        {
            if (checkAuthentication())
            {
                var course = (from s in db.Courses select s).ToList();
                ViewBag.courses = course;
                return View();
            }
            else
            {
                ViewBag.MyMessage = "You need to login as a Teacher account or Student account to have permission to acces this page";
                return View("Error");
            }
        }
    }
}