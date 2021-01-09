using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MailSendWithFile.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Mail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Mail(HttpPostedFileBase dosya, string Name, string LastFirm, string City, string Phone, string Email)
        {
            string path = Server.MapPath("~/Content" + dosya.FileName);
            dosya.SaveAs(path);
            SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential cred = new NetworkCredential("@gmail.com", "sifre");
            mailClient.Credentials = cred;
            MailMessage contact = new MailMessage();
            contact.From = new MailAddress("@gmail.com");
            contact.Subject = "Dosya Yuklendi";
            contact.IsBodyHtml = true;
            contact.Body = " Ad : " + Name + " Son Çalışılan Firma : " + LastFirm + " Şehir : " + City + " Telefon : " + Phone + " Email : " + Email;
            mailClient.EnableSsl = true;
            Attachment a = new Attachment(dosya.InputStream, dosya.FileName);
            contact.Attachments.Add(a);
            contact.To.Add("@gmail.com");
            mailClient.Send(contact);
            return View();
        }
    }
}