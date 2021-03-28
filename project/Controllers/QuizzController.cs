using project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using project.Models;
using project.Models.DTO;
using System.Web.Hosting;
using project.Shared;
using System.Dynamic;

namespace project.Controllers
{
    public class QuizzController : Controller
    {
        // GET: Quizz
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Course(int? page)
        {
            using (EducationDBContext db = new EducationDBContext())
            {
                var lst = (from c in db.Courses orderby c.Name select c).Select(o => new CourseDTO()
                {
                    Code = o.Code,
                    Describtion = o.Describtion,
                    Id = o.Id,
                    Image = string.IsNullOrEmpty(o.Image) ? "~/Assets/images/quizz/quizz-logo-default.jpg" : o.Image,
                    rate = (o.rate.HasValue ? (((float)o.rate / 5.0) * 100) : 0) + "%",
                    total_rate = o.total_rate.HasValue ? o.total_rate : 0,
                    Name = o.Name
                });


                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(lst.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult Subject(int courseId, int subjectId)
        {
            using (EducationDBContext db = new EducationDBContext())
            {
                var subject = (from s in db.Subjects where s.Id == subjectId select s).FirstOrDefault();
                var subjectDTO = AutoMap.Mapper.Map<SubjectDTO>(subject);
                subjectDTO.Image = string.IsNullOrEmpty(subjectDTO.Image) ? "~/Assets/images/quizz/subject__default.png" : subjectDTO.Image;
                subjectDTO.Rate = subjectDTO.Rate.HasValue? (((float)subjectDTO.Rate / 5.0) * 100) : 0;
                subjectDTO.Total_rate = subjectDTO.Total_rate.HasValue ? subjectDTO.Total_rate : 0;
                var lstLesson = (from l in db.Lessions where l.CurentSubjectId == subjectDTO.Id orderby l.Id select l).Select(l => new LessonDTO()
                {
                    Id = l.Id,
                    CurentSubjectId = l.CurentSubjectId,
                    Content = l.Content,
                    Describtion = l.Describtion,
                    Name = l.Name,
                    View = l.View,
                    Url = l.Url
                }).ToList();
                dynamic model = new ExpandoObject();
                model.subjectDTO = subjectDTO;
                model.lstLesson = lstLesson;
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult ListSubject(string id, int pageNum)
        {
            using (EducationDBContext db = new EducationDBContext())
            {
                int code = int.Parse(id);
                var lst = (from s in db.Subjects where s.CurrentCourseId == code orderby s.Id select s).Skip((pageNum - 1) * 6).Take(6).Select(o => new SubjectDTO()
                {
                    Code = o.Code,
                    Describtion = o.Describtion,
                    Id = o.Id,
                    Name = o.Name,
                    Image = o.Image,
                    CurrentCourseId = o.CurrentCourseId
                }).ToArray();
                return Json(new { data = lst, courseId = id }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}