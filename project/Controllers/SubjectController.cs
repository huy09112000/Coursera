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
    }
}