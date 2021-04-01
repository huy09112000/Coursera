using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project.Controllers
{
    public class HomeController : Controller
    {

        //cac home controller se phai check exist session va check authorization tu list
        public ActionResult Index()
        {
            return View();
        }

        public bool checkAuthentication()
        {
            string absolutepath = HttpContext.Request.Url.AbsolutePath;
            //check authorize
            User user = (User)Session["account"];
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

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult ForgotPassword()
        {
            ViewBag.Message = "Forgot pw";
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Forgot pw";
            return View();
        }

    }
}