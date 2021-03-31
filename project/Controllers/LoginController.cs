using project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using project.DAL;
using System.Web.Mvc;
using System.Net.Mail;

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

        [HttpPost]
        public ActionResult Verify(User acc)
        {
            if (ModelState.IsValid)
            {
                DataTable dataTable = new DataTable();

                dataTable = new DataProvider().executeQuery("SELECT * FROM [EducationDB].[dbo].[Users] where Email = '" + acc.Email + "' and Password = '" + acc.Password + "'");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    //exist thi load them list<authorization> cho object roi sau do tạo session để lơi data
                    Session["account"] = acc;
                    return View("Contact");
                }
                else
                {
                    //k ton tai
                    return View("Error");
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
        private void sendEmail(string mailTo, string emailBody)
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                    sendEmail(user.Email, newPassWord);

                    //return ve home page
                    return View("Contact");
                }
                else
                {
                    //return loi
                    return View("Error");
                }
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            //moi lan tao moi la phai tao moi ca 2 bang
            new DataProvider().executeNonQuery("INSERT INTO [dbo].[Users]  ([Email] ,[Password] ,[Role],[Avatar])VALUES('" + user.Email + "','" + user.Password + "','3','')");
            return View("Contact");
            //dang gap vấn đề ở chỗ declare mutiple model cho layout.html page
        }
    }
}