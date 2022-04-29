using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Index()
        {
            return View(getAllProducts());
        }

        #region Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginInfo model)
        {
            if (ModelState.IsValid)
            {
                var isValidUser = IsValidUser(model);
                if (isValidUser != null && isValidUser.isAdmin == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (isValidUser != null && isValidUser.isAdmin == 0)
                {
                    return RedirectToAction("Product");
                }
                else
                {
                    ModelState.AddModelError("Failure", "Wrong Username and password combination !");
                    return View();
                }
            }
            else
            {
                return View(model);
            }
        }


        public LoginInfo IsValidUser(LoginInfo model)
        {
            using (var dataContext = new TempDb1Entities())
            {
                LoginInfo user = dataContext.LoginInfoes.Where(query => query.UN.Equals(model.UN) && query.Pwd.Equals(model.Pwd)).SingleOrDefault();
                Session["UserName"] = user.UN;
                if (user == null)
                    return null;
                else
                    return user;
            }
        }
        #endregion

        #region Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveRegisterDetails(LoginInfo registerDetails)
        {
            if (ModelState.IsValid)
            {
                using (var databaseContext = new TempDb1Entities())
                {
                    LoginInfo reglog = new LoginInfo();

                    reglog.UN = registerDetails.UN;
                    reglog.Pwd = registerDetails.Pwd;
                    reglog.CustomerId = Convert.ToInt32(DateTime.Now.Millisecond);

                    databaseContext.LoginInfoes.Add(reglog);
                    databaseContext.SaveChanges();
                }

                ViewBag.Message = "User Details Saved";
                //return View("Register");
            }
            else
            {
                ViewBag.Message = "User not Saved";
                //return View("Register", registerDetails);
            }
            return View("Login");
        }
        #endregion

        #region AddProductifAdmin
        public ActionResult Product()
        {
            return View(getAllProducts());
        }

        [HttpPost]
        public ActionResult SaveProductDetails(ProductInfo AddProduct)
        {
            if (ModelState.IsValid)
            {
                using (var databaseContext = new TempDb1Entities())
                {
                    Product prod = null;
                    if (AddProduct != null)
                    {
                        prod = new Product();
                        prod.Product_Name = AddProduct.Product_Name;
                        prod.Price = AddProduct.Price;
                        prod.url = AddProduct.Url;
                        prod.Description = AddProduct.Description;
                    }
                    databaseContext.Products.Add(prod);
                    databaseContext.SaveChanges();
                }

                ViewBag.Message = "Product Details Saved";
                //return View("Register");
            }
            else
            {
                ViewBag.Message = "Product Details not Saved";
                //return View("Register", registerDetails);
            }
            return RedirectToAction("Product","Account");
        }

        public List<ProductInfo> getAllProducts()
        {
            string mycmd = "select * from [product]";
            DataTable dt = null;
            List<ProductInfo> list = null;
            //data source=.;initial catalog=TempDb1;integrated security=True;MultipleActiveResultSets=True;
            string constring = "Server=.;initial catalog=TempDb1;integrated security=True;";
                //ConfigurationManager.ConnectionStrings["TempDb1Entities1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand(mycmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (dt = new DataTable())
                        {
                            sda.Fill(dt);

                        }
                    }
                }
            }

            list = new List<ProductInfo>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProductInfo mob = new ProductInfo();
                mob.slno = Convert.ToInt32(dt.Rows[i]["slNo"]);
                mob.Product_Name = dt.Rows[i]["Product_Name"].ToString();
                mob.Price = Convert.ToDecimal(dt.Rows[i]["price"]);
                mob.Url = dt.Rows[i]["url"].ToString();
                list.Add(mob);
            }
            return list;
        }
        #endregion

        #region CustomerLogindisplayProduct
        public ActionResult Catalogue()
        {
            return View();
        }
        #endregion

        #region AddtoCart
        
        public ActionResult SaveOrderDetails()
        {
            Product_Purchase_History prod = null;
            foreach (var item in (List<ProductInfo>)Session["cart"])
            {
                int sum = Convert.ToInt32(((List<ProductInfo>)Session["cart"]).Select(x => x.Price).Sum());
                int qty = ((List<ProductInfo>)Session["cart"]).Select(x => x.Product_Name).Count();
                using (var databaseContext = new TempDb1Entities())
                {
                    if (item != null)
                    {
                        prod = new Product_Purchase_History();
                        prod.SID = item.slno;
                        prod.Price = item.Price;
                        prod.url = item.Url;
                        prod.Description = item.Description;
                        prod.Purchase_Date = DateTime.Now;
                        prod.Price = item.Price;
                        prod.Quantity = Convert.ToInt32(Session["count"]);
                        prod.UserName = Session["UserName"] != null ? Convert.ToString(Session["UserName"]) : "ADMIN";
                        prod.Product = item.Product_Name;
                        prod.TotalAmount = sum;
                    }
                    databaseContext.Product_Purchase_History.Add(prod);
                    databaseContext.SaveChanges();
                }
            }
                
            
            ViewBag.Message = "Product Details Saved";
            return RedirectToAction("Logout", "Account");
        }

        public ActionResult Myorder()
        {
            return View((List<ProductInfo>)Session["cart"]);
        }
        public ActionResult Add(ProductInfo pI)
        {

            if (Session["cart"] == null)
            {
                List<ProductInfo> li = new List<ProductInfo>();
                li.Add(pI);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = 1;
            }
            else
            {
                List<ProductInfo> li = (List<ProductInfo>)Session["cart"];
                li.Add(pI);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }
            return RedirectToAction("Product", "Account");
        }
        #endregion

        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return Redirect("Account/Login");
            //return View("Login");
        }
    }
}