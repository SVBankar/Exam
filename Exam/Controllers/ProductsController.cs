using Exam.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Exam;Integrated Security=True;";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Select * from ProductTable";

            List<Products> pd = new List<Products>();

            try
            {
                SqlDataReader dr = cmd.ExecuteReader();

                while(dr.Read())
                {
                    pd.Add(new Products { ProductId = (int)dr["ProductId"], ProductName = dr["ProductName"].ToString(), Rate = (long)dr.GetDecimal(2), Description = dr["Description"].ToString(), CategoryName = dr["CategoryName"].ToString() });

                }
                dr.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            cn.Close();


            return View(pd);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id = 1)
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Exam;Integrated Security=True;";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.CommandText = "Select * from ProductTable where ProductId = @ProductId";
            cmd.Parameters.AddWithValue("@ProductId", id);

            SqlDataReader dr = cmd.ExecuteReader();

            Products obj = null;

            if(dr.Read())
            {
                obj = new Products { ProductId = id, ProductName = dr.GetString(1), Rate = (long)dr.GetDecimal(2), Description = dr.GetString(3), CategoryName = dr.GetString(4) };
            }
            else
            {
                ViewBag.ErrorMessage = "Product Not Found";
            }
            cn.Close();
            return View(obj);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Products pd)
        {
            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Exam;Integrated Security=True;";
                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "Update ProductTable set  ProductName = @ProductName, Rate = @Rate, Description = @Description, CategoryName = @CategoryName Where ProductId = @ProductId";

                cmd.Parameters.AddWithValue("@ProductId", pd.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", pd.ProductName);
                cmd.Parameters.AddWithValue("@Rate", pd.Rate);
                cmd.Parameters.AddWithValue("@Description", pd.Description);
                cmd.Parameters.AddWithValue("@CategoryName", pd.CategoryName);


                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("No error");
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    cn.Close();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
