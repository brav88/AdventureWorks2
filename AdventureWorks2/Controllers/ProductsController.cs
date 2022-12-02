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

            return View();
        }

        public ActionResult LoadSubCategories(int categoryId)
        {
            ViewBag.CategoriesList = GetCatalog("spGetProductCategories", null);
            ViewBag.SubCategoriesList = GetCatalog("spGetSubProductCategory", new List<SqlParameter> { new SqlParameter("@ProductCategoryID", categoryId) });

            return View("Index");
        }

        public ActionResult LoadProducts(int subCategoryId)
        {
            ViewBag.CategoriesList = GetCatalog("spGetProductCategories", null);            

            List<Product> products = new List<Product>();
            foreach (DataRow item in DatabaseHelper.ExecuteStoreProcedure("spGetProducts", new List<SqlParameter> { new SqlParameter("@ProductSubcategoryID", subCategoryId) }).Rows)
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

        public List<Catalog> GetCatalog(string storeProcedure, List<SqlParameter>? list)
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
    }
}
