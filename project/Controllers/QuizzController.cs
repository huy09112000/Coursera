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
using project.Filter;
using System.Net.Http;

namespace project.Controllers
{
    [NitishAuthentication]
    [FilterAuthorize(1, 2)]
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
                subjectDTO.Rate = subjectDTO.Rate.HasValue ? (((float)subjectDTO.Rate / 5.0) * 100) : 0;
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

        public ActionResult Lesson(int subjectId, int lessonId)
        {
            using (EducationDBContext db = new EducationDBContext())
            {
                var lstQuizz = (from q in db.Quizzs where q.CurrentLessionId == lessonId orderby q.Id select q);

                var lstNumQues = (from qz in lstQuizz
                                  join qs in db.Questions
                                  on qz.Id equals qs.CurrentQuizzId
                                  orderby qz.Id
                                  group qs.Id by qz.Id into g
                                  select new { key = g.Key, count = g.Count() }).ToList();

                var lstquizzDTO = AutoMap.Mapper.Map<List<QuizzDTO>>(lstQuizz.ToList());
                for (int i = 0; i < lstquizzDTO.Count(); i++)
                {
                    lstquizzDTO.ElementAt(i).Image = string.IsNullOrEmpty(lstquizzDTO.ElementAt(i).Image) ? "~/Assets/images/quizz/quizz_default.png" : lstquizzDTO.ElementAt(i).Image;
                    lstquizzDTO.ElementAt(i).Level = lstquizzDTO.ElementAt(i).Level.HasValue ? lstquizzDTO.ElementAt(i).Level : 0;
                    lstquizzDTO.ElementAt(i).NumberQuestion = lstquizzDTO.ElementAt(i).NumberQuestion.HasValue ? lstquizzDTO.ElementAt(i).NumberQuestion : lstNumQues.ElementAtOrDefault(i)?.count;
                }
                ViewBag.subjectId = subjectId;
                ViewBag.lessonId = lessonId;
                return View(lstquizzDTO);

            }
        }
        public ActionResult BeforeTesting(int lessonId, int quizzId)
        {
            using (EducationDBContext db = new EducationDBContext())
            {
                var quizzDTO = AutoMap.Mapper.Map<QuizzDTO>((from qz in db.Quizzs where qz.Id == quizzId select qz).FirstOrDefault());
                quizzDTO.NumberQuestion = (from qs in db.Questions where qs.CurrentQuizzId == quizzDTO.Id select qs).Count();
                ViewBag.lessonId = lessonId;
                ViewBag.quizzId = quizzId;
                return View("Exam", quizzDTO);
            }
        }
        [HttpGet]
        [ActionName("exam")]
        public ActionResult OnTesting(int quizzId)
        {
            using (EducationDBContext db = new EducationDBContext())
            {
                var lst = (from qs in db.Questions where qs.CurrentQuizzId == quizzId orderby qs.Id select qs).ToList();
                var lstDTO = AutoMap.Mapper.Map<List<QuestionsGivenDTO>>(lst);

                lstDTO.ForEach(o => o.Answers = AutoMap.Mapper.Map<List<AnswerDTO>>((from a in db.Answers where a.CurrentQuestionId == o.Id select a).ToList()));
                return Json(new { data = lstDTO }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ActionName("result")]
        public ActionResult AfterSubmit(List<AnswerOnSubmitDTO> data, int id, string time)
        {
            int userId = Convert.ToInt32(Session["id"]);
            int correctAns = 0;
            double pointTotalCorrect = 0;
            double pointTotal = 0;
            data = data != null ? data : new List<AnswerOnSubmitDTO>();
            PointAfterSubmitDTO pointAfter;
            List<ResultDTO> ResultDTO = new List<ResultDTO>();
            double timeQuizz;
            double timeRemaind;
            double timeDo;
            List<QuestionsGivenDTO> lstDTO= new List<QuestionsGivenDTO>();

            //data handle
            using (EducationDBContext db = new EducationDBContext())
            {

                //get list ques
                var lst = (from qs in db.Questions where qs.CurrentQuizzId == id orderby qs.Id select qs).ToList();
                 lstDTO = AutoMap.Mapper.Map<List<QuestionsGivenDTO>>(lst);

                //time handle
                string[] str = time.Split(':');
                timeQuizz = (from q in db.Quizzs where q.Id == id select q).First().time * 60*60;
                timeRemaind = int.Parse(str[0])*60 + int.Parse(str[1]);
                timeDo = timeQuizz - timeRemaind;
                int currentIdQues = -1;
                //check question
                foreach (AnswerOnSubmitDTO item in data)
                {
                    var ansCorrect = (from an in db.Answers where an.CurrentQuestionId == item.Ques && an.IsCorrect select an).First();
                    double? point = (from q in db.Questions where q.Id == item.Ques select q).FirstOrDefault().Point;
                    if(currentIdQues != item.Ques)
                    {
                        pointTotal += point.HasValue ? point.Value : 0;
                        currentIdQues = item.Ques;
                    }
                    if (ansCorrect.Id == item.Ans)
                    {
                        pointTotalCorrect += point.HasValue ? point.Value : 0;
                        correctAns += 1;
                    }
                    ResultDTO.Add(new ResultDTO()
                    {
                        AnsChoosed = item.Ans,
                        CorrectAns = ansCorrect.Id,
                        QuesId = item.Ques,
                        QuizzId = id,
                    });
                }
                UserInfor user = (from u in db.UserInfors where u.Id == userId select u).FirstOrDefault();
                Quizz quizz = (from q in db.Quizzs where q.Id == id select q).FirstOrDefault();
                pointAfter = new PointAfterSubmitDTO()
                {
                    Quizz = quizz,
                    Score = pointTotalCorrect,
                    UserInfor = user,
                    CorrectAnswer = correctAns
                };
                Point score = (from p in db.points where p.UserInfor.Id == userId && p.Quizz.Id == id select p).FirstOrDefault();
                if (score != null)
                {
                    score.CorrectAnswer = pointAfter.CorrectAnswer;
                    score.Quizz = pointAfter.Quizz;
                    score.Score = pointAfter.Score;
                    score.UserInfor = pointAfter.UserInfor;
                }
                else
                {
                    db.points.Add(AutoMap.Mapper.Map<Point>(pointAfter));
                }
                db.SaveChanges();
            }
            return Json(new
            {
                lisQues=lstDTO,
                data = ResultDTO,
                timetotal = timeQuizz,
                timeDo = timeDo,
                point= pointTotalCorrect,
                pointTotal= pointTotal
            },
            JsonRequestBehavior.AllowGet) ;
        }
        [ActionName("examresult")]
        public ActionResult AfterExam(int id)
        {
            int userId = Convert.ToInt32(Session["id"]);
            using (EducationDBContext db = new EducationDBContext())
            {
                var quizzDTO = AutoMap.Mapper.Map<QuizzDTO>((from q in db.Quizzs where q.Id == id select q).FirstOrDefault());
                var point = AutoMap.Mapper.Map<PointAfterSubmitDTO>((from p in db.points where p.UserInfor.Id == userId && p.Quizz.Id == id select p).FirstOrDefault());
                QuizzResultDTO quizzResultDTO = new QuizzResultDTO()
                {
                    CorrectAnswer = point.CorrectAnswer,
                    Image = string.IsNullOrEmpty(quizzDTO.Image) ? "~/Assets/images/quizz/quizz_default.png" : quizzDTO.Image,
                    Id = quizzDTO.Id,
                    Level = quizzDTO.Level.HasValue ? quizzDTO.Level.Value : 0,
                    Name = quizzDTO.Name,
                    Score = point.Score,
                    time = quizzDTO.time
                };

                return View("AfterSubmit", quizzResultDTO);

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