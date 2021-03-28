using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace project
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { id = @"\d*" }
            );

            routes.MapRoute(
                name: "Quizz",
                url: "{controller}/{action}",
                defaults: new { controller = "Quizz", action = "Course" }

                );
            routes.MapRoute(
                name: "ListSubject",
                url: "quizz/listsubject/{id}",
                defaults: new { controller = "Quizz", action = "ListSubject"},
                constraints: new { id = @"\d+" }
                );
            routes.MapRoute(
                name:"Subject",
                url:"quizz/subject/{courseId}/{subjectId}",
                defaults: new {Controller = "Quizz", Action = "Subject"},
                constraints:new {courseId =@"\d+", subjectId = @"\d+"}  
                );
            routes.MapRoute(
                name:"Lesson",
                url:"quizz/lesson/{subjectId}/{lessonId}",
                defaults: new {Controller="Quizz",Action= "Lesson" },
                constraints: new { subjectId = @"\d+", lessonId = @"\d+"}
                );
        }
    }
}
