using AdventureWorks.DatabaseHelper;
using AdventureWorks2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection.Emit;

namespace AdventureWorks2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("userSession")))
            {
                ViewBag.User = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userSession"));

                ViewBag.Employees = true;
                ViewBag.Sales = true;

                return View();
            }
            else
            {
                ViewBag.Error = new ErrorHandler()
                {
                    Title = "You must need to login to access this page",
                    ErrorMessage = "Please login",
                    Path = "/Login"
                };

                return View("ErrorHandler");
            }
        }

        [HttpPost]
        public ActionResult UpdatePhoto(IFormFile photo)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("userSession")))
            {
                ViewBag.User = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userSession"));

                string photoPath = Path.Combine("img\\", ViewBag.User.BusinessEntityID + new FileInfo(photo.FileName).Extension);

                using (var stream = new FileStream(Directory.GetCurrentDirectory() + "\\wwwroot\\" + photoPath, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }

                DatabaseHelper.ExecStoreProcedure("[dbo].[spUpdatePhotoPath]", new List<SqlParameter>()
                {
                    new SqlParameter("@photoPath", photoPath),
                    new SqlParameter("@businessEntityID", ViewBag.User.BusinessEntityID)
                });

                ViewBag.User.PhotoPath = photoPath;

                return View("Index");
            }
            else
            {
                ViewBag.Error = new ErrorHandler()
                {
                    Title = "You must need to login to access this page",
                    ErrorMessage = "Please login",
                    Path = "/Login"
                };

                return View("ErrorHandler");
            }
        }

        public ActionResult DeletePhoto()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("userSession")))
            {
                ViewBag.User = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userSession"));

                string photoPath = "img\\0.jpg";

                DatabaseHelper.ExecStoreProcedure("[dbo].[spUpdatePhotoPath]", new List<SqlParameter>()
                {
                    new SqlParameter("@photoPath", photoPath),
                    new SqlParameter("@businessEntityID", ViewBag.User.BusinessEntityID)
                });

                ViewBag.User.PhotoPath = photoPath;

                return View("Index");
            }
            else
            {
                ViewBag.Error = new ErrorHandler()
                {
                    Title = "You must need to login to access this page",
                    ErrorMessage = "Please login",
                    Path = "/Login"
                };

                return View("ErrorHandler");
            }
        }

        public ActionResult GetSales()
        {
            DataTable ds = DatabaseHelper.ExecuteStoreProcedure("[dbo].[spGetSales]", null);

            List<List<int>> seriesList = new List<List<int>>();
            List<int> labels = new List<int>();
            List<int> series1 = new List<int>();

            foreach (DataRow row in ds.Rows)
            {
                labels.Add(Convert.ToInt32(row["Year"]));
                series1.Add(Convert.ToInt32(row["SumOfSales"]));
            }
            
            seriesList.Add(series1);            

            SalesData data = new SalesData()
            {
                labels = labels,
                series = seriesList
            };

            return Json(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}