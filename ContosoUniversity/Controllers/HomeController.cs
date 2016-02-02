using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult sendEmail()
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("zcoverride@gmail.com","Put here your name");
            mail.To.Add("zc92@live.com");
            mail.Subject = "Test Send Email";
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = "This is the body for email";
            mail.Body = htmlBody;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("zcoverride@gmail.com", "tokiohotel");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

            return RedirectToAction("");
        }
    }
}
