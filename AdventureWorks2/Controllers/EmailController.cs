using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using AdventureWorks2.Models;
using System.IO;
using System.Globalization;

namespace AdventureWorks2.Controllers
{
    public class EmailController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(EmailModel model)
        {
            /*claseprogra5castrocarazo@gmail.com*/
            /*Admin$1234*/
            /*vxttcmdwibjyeeoy*/

            using (MailMessage mm = new MailMessage(model.Email, model.To))
            {
                mm.Subject = model.Subject;
                /*mm.Body = model.Body;*/
                /*if (model.Attachment.Length > 0)
                {
                    using (var stream = model.Attachment.OpenReadStream())
                    {
                        var attachment = new Attachment(stream, model.Attachment.FileName);
                        mm.Attachments.Add(attachment);
                    }
                }*/
                mm.IsBodyHtml = true;

                /**/
                using (var sr = new StreamReader("wwwroot/html/welcome.txt"))
                {
                    // Read the stream as a string, and write the string to the console.
                    string body =  sr.ReadToEnd().Replace("@CLIENTNAME", "SAMUEL");

                    mm.Body = body;
                }
                /**/


                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(model.Email, model.Password);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                ViewBag.Message = "Email sent.";
            }

            return View();
        }

        // GET: EmailController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmailController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmailController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmailController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmailController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
