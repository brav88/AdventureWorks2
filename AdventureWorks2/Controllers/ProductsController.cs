using AdventureWorks.DatabaseHelper;
using AdventureWorks2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AdventureWorks2.Controllers
{
    public class ProductsController : Controller
    {
        // GET: ProductsController
        public ActionResult Index()
        {
            ViewBag.CategoriesList = GetCatalog("spGetProductCategories", null);
            ViewBag.SubCategoriesList = new List<Catalog>();
            ViewBag.ProductsList = new List<Product>();

            return View();
        }

        public List<Catalog> GetCatalog(string storeProcedure, List<SqlParameter> list)
        {
            List<Catalog> catalogList = new List<Catalog>();

            foreach (DataRow item in DatabaseHelper.ExecuteStoreProcedure(storeProcedure, list).Rows)
            {
                catalogList.Add(new Catalog
                {
                    Id = Convert.ToUInt16(item["id"]),
                    Desc = item["desc"].ToString()
                });
            }

            return catalogList;
        }

        public ActionResult LoadSubCategories(int categoryId)
        {
            ViewBag.CategoriesList = GetCatalog("spGetProductCategories", null);
            ViewBag.ProductsList = new List<Product>();

            ViewBag.SubCategoriesList = GetCatalog("spGetSubProductCategory", new List<SqlParameter> {
                new SqlParameter("@ProductCategoryID", categoryId)
            });

            return View("Index");
        }

        public ActionResult LoadProducts(int subCategoryId)
        {
            List<Product> products = new List<Product>();

            ViewBag.CategoriesList = GetCatalog("spGetProductCategories", null);
            ViewBag.SubCategoriesList = new List<Catalog>();

            DataTable ds = DatabaseHelper.ExecuteStoreProcedure("spGetProducts", new List<SqlParameter> {
                new SqlParameter("@ProductSubcategoryID", subCategoryId)
            });

            foreach (DataRow item in ds.Rows)
            {
                products.Add(new Product
                {
                    Name = item["Name"].ToString(),
                    Color = item["Color"].ToString(),
                    ProductNumber = item["ProductNumber"].ToString(),
                    ListPrice = Convert.ToDecimal(item["ListPrice"]),
                });
            }

            ViewBag.ProductsList = products;

            return View("Index");
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int option)
        {
            return View();
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
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

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductsController/Edit/5
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

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
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
