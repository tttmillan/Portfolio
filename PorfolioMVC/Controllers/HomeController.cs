using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PorfolioMVC.Models;
using System.Configuration;
using System.Net;
using System.Net.Mail;


namespace PorfolioMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult PortfolioDetails ()
        {
            return View();
        }


        [HttpPost]
        public JsonResult ContactAjax(ContactViewModel cvm)
        {
            //You can make this whatever you want, it will be the body of the message sent
            string body = $"{cvm.Name} has sent you the following message:<br/> " +
                $"{cvm.Message} <strong>from the email address:</strong> {cvm.Email}";
            //Message Object
            MailMessage mm = new MailMessage(
            //FROM address - email must be on host -creds stored in Web.config
            ConfigurationManager.AppSettings["EmailUser"].ToString(),
            //TO - email doesn't have to be on host - creds stored in Web.config
            ConfigurationManager.AppSettings["EmailTo"].ToString(),
            //email subject
            cvm.Subject,
            //body of the email
            body);

            //allow HTML in email ( that is our formatting with br and strong tags above)
            mm.IsBodyHtml = true;
            //you can make the message be designated as high priority
            mm.Priority = MailPriority.High;
            //reply to the Person who filled out the form, not your domain email
            mm.ReplyToList.Add(cvm.Email);

            //configure the mail client - creds stored in web.config
            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());
            //configure the email credentials using values from web.config
            client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUser"].ToString(),
            ConfigurationManager.AppSettings["EmailPass"].ToString());

            try
            {
                //send email
                client.Send(mm);
            }
            catch (Exception ex)
            {
                //log error in ViewBag to be seen by admins
                ViewBag.Message = ex.StackTrace;
            }

            return Json(cvm);
        }
    }
}