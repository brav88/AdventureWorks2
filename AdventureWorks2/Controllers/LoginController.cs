using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdventureWorks.DatabaseHelper;
using System.Data.SqlClient;
using System.Data;
using AdventureWorks2.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AdventureWorks2.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public ActionResult Index()
        {
            ViewBag.Employees = false;
            ViewBag.Sales = false;

            return View();
        }

        [HttpPost]
        public ActionResult ValidateLogin(string txtEmail, string txtPassword)
        {
            User? user = GetUser(txtEmail, txtPassword);

            if (user != null)
            {
                List<UserAccess>? userAccessList = GetUserAccess(user.BusinessEntityID);

                string strUser = JsonConvert.SerializeObject(user);
                string strUserAccessList = JsonConvert.SerializeObject(userAccessList);

                HttpContext.Session.SetString("userSession", strUser);
                HttpContext.Session.SetString("userAccessListSession", strUserAccessList);

                return RedirectToAction(userAccessList[0].Action, userAccessList[0].Controller);
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
                    PhotoPath = ds.Rows[0]["PhotoPath"].ToString(),
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

        private List<UserAccess>? GetUserAccess(string businessEntityID)
        {
            List<UserAccess> strUserAccessList = new List<UserAccess>();

            DataTable ds = DatabaseHelper.ExecuteStoreProcedure("[dbo].[spGetUserAccess]", new List<SqlParameter>()
            {
                new SqlParameter("@BusinessEntityID", businessEntityID),                
            });

            foreach (DataRow row in ds.Rows)
            {
                strUserAccessList.Add(new UserAccess
                {
                    BusinessEntityID = row["BusinessEntityID"].ToString(),
                    Controller = row["Controller"].ToString(),
                    Action = row["Action"].ToString(),
                    DatabaseAction = row["DatabaseAction"].ToString(),
                });
            }

            return strUserAccessList;
        }

        public IActionResult GetCurrentSessionUserAccess()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("userAccessListSession")))
            {
                List<UserAccess>? userAccessList = JsonConvert.DeserializeObject<List<UserAccess>>(HttpContext.Session.GetString("userAccessListSession"));

                return Json(userAccessList);
            }

            return Ok();                
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
