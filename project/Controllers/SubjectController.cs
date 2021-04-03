using project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models;
using project.Filter;
using PagedList;

namespace project.Controllers
{
    [NitishAuthentication]
    public class SubjectController : Controller
    {
        private EducationDBContext db = new EducationDBContext();
        // GET: Subject
        public ActionResult Subject(int? page,int CurrentCourseId)
        {
            //int id = Convert.ToInt32(Request["CurrentCourseId"]);
            var subject = (from s in db.Subjects where s.CurrentCourseId == CurrentCourseId select s).ToList();
            foreach (var subjects in subject)
            {
                subjects.Rate = subjects.Rate.HasValue? (((float)subjects.Rate / 5.0) * 100) : 0;
            }
            ViewBag.id = CurrentCourseId;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(subject.ToPagedList(pageNumber, pageSize));
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