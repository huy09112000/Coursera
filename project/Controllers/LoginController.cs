using project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using project.DAL;
using System.Web.Mvc;
using System.Net.Mail;
using project.Shared;
using project.Models.DTO;
using System.Web.Security;

namespace project.Views.Home
{
    public class LoginController : Controller
    {
        // GET: Login

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Verify(string email, string pass)
        {
            if (ModelState.IsValid)
            {

                using (EducationDBContext db = new EducationDBContext())
                {
                    var user = (from u in db.Users where u.Email.Equals(email) && u.Password.Equals(pass) select u).FirstOrDefault();

                    if(user != null)
                    {
                        FormsAuthentication.SetAuthCookie(email, false);
                        UserDTO userDTO = AutoMap.Mapper.Map<UserDTO>(user);
                        Session["account"] = userDTO.Email;
                        Session["id"] = userDTO.Id;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //k ton tai
                        return View("Error");
                    }
                }
            }
            else
            {
                return View("Error");
            }
        }

        private string passwordRender()
        {
            //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var chars = "0123456789";
            var random = new Random();
            var result = new char[8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }
            return new string(result);
        }
        private string sendEmail(string mailTo, string emailBody)
        {
            try
            {
                /* link solution https://stackoverflow.com/questions/18503333/the-smtp-server-requires-a-secure-connection-or-the-client-was-not-authenticated
                 * ba việc cần làm trước khi gửi mail
                    -check đúng username password
                    -turn on less scure app: https://myaccount.google.com/lesssecureapps?rapt=AEjHL4Mix6WTKA8k7CaG99JTnHsI3xIG4h4j-EV-J882IDN90lo87KkIQy44hcXa61YfFq3vpiW6mltxbMKKjbhXiA3PUGTxNw
                    -tắt bảo mật 2 lớp của gmail
                 */

                //parameter is mail from and mail to (in this case i will send to myself )
                MailMessage mailMessage = new MailMessage("huynthe141627@fpt.edu.vn", mailTo);

                mailMessage.Subject = "Growth Education [Password recovery]";

                //content of email
                mailMessage.Body = "Your password has been change to: " + emailBody;

                SmtpClient smtpClient;
                smtpClient = new SmtpClient("smtp.gmail.com", 587);

                //specify username and password from ( mail from sender)
                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "huynthe141627@fpt.edu.vn",
                    Password = "huytu0203"
                };
                //to access https link
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                return "Success!!";
            }
            catch (Exception e)
            {
               return e.Message;
            }
        }

        [HttpPost]
        public ActionResult ForgotPass(User user)
        {
            //check exist
            if (ModelState.IsValid)
            {
                DataTable dataTable = new DataTable();

                dataTable = new DataProvider().executeQuery("SELECT * FROM [EducationDB].[dbo].[Users] where Email = '" + user.Email + "' ");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    //exist thi render new password va gui mail
                    //update database
                    string newPassWord = passwordRender();
                    new DataProvider().executeQuery("UPDATE [dbo].[Users] SET [Password] = '" + newPassWord + "' WHERE [Email] = '" + user.Email + "'");
                    //gui mail

                    string sendMailResult = sendEmail(user.Email, newPassWord);
                    if (sendMailResult != "Success!!")
                    {
                        ViewBag.MyMessage = sendMailResult;
                        return View("Error");
                    }

                    //return ve home page
                    return View("Login");
                }
                else
                {
                    //return loi
                    ViewBag.MyMessage = "Account doesn't exist";
                    return View("Error");

                }
            }
            else
            {
                ViewBag.MyMessage = "Validation data error";
                return View("Error");

            }
        }

        [HttpPost]
        public ActionResult Register(string name, string email, int mobile, string pass)
        {
            //moi lan tao moi la phai tao moi ca 2 bang
            using (EducationDBContext db = new EducationDBContext())
            {
                User principal = new User()
                {
                    Email = email,
                    Password = pass,
                    Role = 2,
                };

               UserInfor ui= new UserInfor()
                {
                    Name = name,
                    Phone = mobile
                };
               ui.Id= db.Users.Add(principal).Id;
                db.UserInfors.Add(ui);
                db.SaveChanges();
                return View("Contact");
            }

        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["account"] = string.Empty;
            return RedirectToAction("Index", "Home");
        }
    }
}