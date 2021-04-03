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
    [FilterAuthorize(1)]
    public class CourseManagementController : Controller
    {
        EducationDBContext dba = new EducationDBContext();

        // GET: CourseManagement

        public ActionResult CreateCourse()
        {
            return View();
        }

        public ActionResult HomeCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCourse(Course newCourse)
        {
            using (EducationDBContext db = new EducationDBContext())
            {
                //get data then check data // then create and forward 
                db.Courses.Add(newCourse);
                db.SaveChanges();
            }

            //forward value of id to create subject
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CreateSubject()
        {
            //phai load ra list Course id 
            //dispose thuoc method nay
            ViewBag.CurrentCourseId = new SelectList(dba.Courses, "Id", "Name");
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            dba.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult CreateSubject(Subject newSubject)
        {

            using (EducationDBContext db = new EducationDBContext())
            {
                db.Subjects.Add(newSubject);
                db.SaveChanges();
            }
            //dang loi o line 71 phan View khi get select item
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CreateLession()
        {
            ViewBag.CurentSubjectId = new SelectList(dba.Subjects, "Id", "Name");
            return View();
        }


        [HttpPost]
        public ActionResult CreateLession(Lession newLession)
        {
            using (EducationDBContext db = new EducationDBContext())
            {
                db.Lessions.Add(newLession);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");

        }
        public ActionResult CreateQiuzz()
        {
            ViewBag.CurrentLessionId = new SelectList(dba.Lessions, "Id", "Name");
            return View();
        }


        [HttpPost]
        public ActionResult CreateQiuzz(Quizz newQuizz)
        {
            using (EducationDBContext db = new EducationDBContext())
            {
                db.Quizzs.Add(newQuizz);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

    }
}