using project.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.DAL;

namespace project.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }


        [HttpPost]
        public ActionResult Verify(User acc)
        {
            DataTable dataTable = new DataTable();

            dataTable = new DataProvider().executeQuery("SELECT * FROM [EducationDB].[dbo].[Users] where Email = '" + acc.Email + "' and Password = '" + acc.Password + "'");
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return View("Create");
            }
            else
            {
                return View("Error");
            }
        }
    }
}