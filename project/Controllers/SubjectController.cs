using project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models;
using project.Filter;

namespace project.Controllers
{
    [NitishAuthentication]
    public class SubjectController : Controller
    {
        private EducationDBContext db = new EducationDBContext();
        // GET: Subject
        public ActionResult Subject()
        {
            int id = Convert.ToInt32(Request["CurrentCourseId"]);
            var subject = (from s in db.Subjects where s.CurrentCourseId == id select s).ToList();
            foreach (var subjects in subject)
            {
                ViewBag.subjects = subject;
            }
            ViewBag.subjects = subject;
            return View();
        }
    }
}