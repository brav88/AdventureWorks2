using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdventureWorks.DatabaseHelper;
using System.Data.SqlClient;
using System.Data;
using AdventureWorks2.Models;
using Newtonsoft.Json;

namespace AdventureWorks2.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidateLogin(string txtEmail, string txtPassword)
        {
            User? user = GetUser(txtEmail, txtPassword);

            if (user != null)
            {
                string strUser = JsonConvert.SerializeObject(user);

                HttpContext.Session.SetString("userSession", strUser);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = new ErrorHandler()
            {
                Title = "Invalid login",
                ErrorMessage = "Incorrect email or password",
                Path = "/Login"
            };

            return View("ErrorHandler");
        }

        private User? GetUser(string txtEmail, string txtPassword)
        {
            DataTable ds = DatabaseHelper.ExecuteStoreProcedure("[dbo].[spGetUser]", new List<SqlParameter>()
            {
                new SqlParameter("@email", txtEmail),
                new SqlParameter("@password", txtPassword)
            });

            if (ds.Rows.Count > 0)
            {
                User user = new User
                {
                    BusinessEntityID = ds.Rows[0]["BusinessEntityID"].ToString(),
                    Name = ds.Rows[0]["Name"].ToString(),
                    Email = txtEmail,
                    JobTitle = ds.Rows[0]["JobTitle"].ToString(),
                    HireDate = Convert.ToDateTime(ds.Rows[0]["HireDate"].ToString()).ToShortDateString(),
                    Department = ds.Rows[0]["Department"].ToString(),
                    /*PhotoPath = ds.Rows[0]["PhotoPath"].ToString(),*/
                    Address = ds.Rows[0]["Address"].ToString(),
                    VacationHours = Convert.ToInt16(ds.Rows[0]["VacationHours"]),
                    SickLeaveHours = Convert.ToInt16(ds.Rows[0]["SickLeaveHours"]),
                };

                return user;
            }
            else
            {
                return null;
            }
        }


        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
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

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View("Index");
        }

        // POST: LoginController/Edit/5
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

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
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
