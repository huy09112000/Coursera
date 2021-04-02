using project.DAL;
using project.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project.Controllers
{
    [NitishAuthentication]

    public class LessonController : Controller
    {
        private EducationDBContext db = new EducationDBContext();
        // GET: Lesson
        public ActionResult Lesson()
        {
            int id = Convert.ToInt32(Request["CurentSubjectId"]);
            var lesson = (from s in db.Lessions where s.CurentSubjectId == id select s).ToList();
            foreach (var lessons in lesson)
            {
                ViewBag.lessons = lesson;
            }
            ViewBag.lessons = lesson;
            return View();
        }

        public ActionResult Content()
        {
            int id = Convert.ToInt32(Request["Id"]);
            var content = (from s in db.Lessions where s.Id == id select s).ToList();
            foreach (var contents in content)
            {
                ViewBag.contents = content;
            }
            ViewBag.contents = content;
            return View();
        }
    }
}