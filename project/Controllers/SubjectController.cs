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

        //load va get value rate ve
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

        [HttpPost]
        public ActionResult Subject(int subjecId)
        {
            //get new rate
            int UserRate = Convert.ToInt32(Request["new_rating_star"]);
            //cal and response: totalrate+1

            //get value
            var recordeSubjectId = (from s in db.Subjects where s.Id == subjecId select s).ToList();
            Subject currentSubject = recordeSubjectId[0];

            double newSubjectRate = Convert.ToDouble((currentSubject.Total_rate * currentSubject.Rate + UserRate) / (currentSubject.Total_rate + 1));

            //update
            var result = db.Subjects.SingleOrDefault(b => b.Id == currentSubject.Id);
            if (result != null)
            {
                result.Rate = newSubjectRate;
                result.Total_rate = result.Total_rate + 1;
                db.SaveChanges();
            }
            return RedirectToAction("Index","Home");
        }
    }
}