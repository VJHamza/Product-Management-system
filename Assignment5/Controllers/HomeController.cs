using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyDAL;
using Entities;


namespace Assignment5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Assign5()
        {
            ViewBag.Name = Convert.ToString(Session["Name"]);
            return View("Assign5");
        }
        public ActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        public ActionResult ValidateLogin()
        {
            DBManager db = new DBManager();
            UserDTO userobj = new UserDTO();
            var login = Request.Form["username"];
            var password = Request.Form["password"];
            userobj = db.Validate(login, password);
            if (userobj != null)
            {
                Session["ID"] = userobj.userID;
                Session["Name"] = userobj.name;
                Session["login"] = userobj.login;
                Session["isAdmin"] = userobj.IsAdmin;
                if (Convert.ToInt32(Session["isAdmin"]) == 1)
                {
                    return Redirect("~/Home/AdminViewProducts");

                }
                else
                {
                    return Redirect("~/Home/userViewProducts");
                }
            }
            else
            {
                ViewBag.msg = "Invalid USer";
                //ViewBag.Login = userobj.name;
            }
            return View("Login");

        }

        [HttpGet]
        public ActionResult SignUp()
        {

            return View("SignUp");
        }
        [HttpPost]
        public ActionResult UpdateUser()
        {
            UserDTO usr = new UserDTO();
            usr.name = Request.Form["name"]; //Extract data from Request.Form by providing field 'name'
            usr.password = Request.Form["password"]; //Extract data from Request directly by providing field 'name'
            usr.login = Request.Form["login"];
            var login = Request.Form["login"];
            string picName = Convert.ToString(Request["picname"]);
            
            var uniqueName = "";
           



            if (Request.Files["picture"] != null)
            {

                var file = Request.Files["picture"];
                if (file.FileName != "")
                {
                    var ext = System.IO.Path.GetExtension(file.FileName);
                    //uniqueName
                    uniqueName = Guid.NewGuid().ToString() + ext;
                    //get path of our folder
                    var rootPath = Server.MapPath("~/UploadFile");
                    var fileSavePath = System.IO.Path.Combine(rootPath, uniqueName);

                    file.SaveAs(fileSavePath);
                    usr.picURL = uniqueName;

                }
                else
                {
                    usr.picURL = picName;
                }

            }
            usr.IsAdmin = 0;
            usr.IsActive = 1;
            usr.createdOn = DateTime.Now;
            DBManager db = new DBManager();
            int result = db.updateUser(usr,login);
            if (result > 0)
            {
                    return Redirect("~/Home/AdminViewProducts");   
            }
            else
            {
                return Redirect("~/Home/AdminViewProducts");
            }
        }

        [HttpPost]
        public ActionResult SaveUser()
        {
            UserDTO usr = new UserDTO();
            usr.name = Request.Form["name"]; //Extract data from Request.Form by providing field 'name'
            usr.password = Request.Form["password"]; //Extract data from Request directly by providing field 'name'
            usr.login = Request.Form["login"];
            string picName = Convert.ToString(Request["picname"]);
            var uniqueName = "";
            if (Request.Files["picture"] != null)
            {

                var file = Request.Files["picture"];
                if (file.FileName != "")
                {
                    var ext = System.IO.Path.GetExtension(file.FileName);
                    //uniqueName
                    uniqueName = Guid.NewGuid().ToString() + ext;
                    //get path of our folder
                    var rootPath = Server.MapPath("~/UploadFile");
                    var fileSavePath = System.IO.Path.Combine(rootPath, uniqueName);

                    file.SaveAs(fileSavePath);
                    usr.picURL = uniqueName;

                }
                else
                {
                    usr.picURL =picName;
                }

            }
            usr.IsAdmin = 0;
            usr.IsActive = 1;
            usr.createdOn = DateTime.Now;
            DBManager db = new DBManager();
            int result = db.DataInsert(usr);
            if (result > 0)
            {
                return Redirect("~/Home/SignUp");
            }
            else
            {
                ViewBag.msg = "User can not be regitered. Try Again.";
                return View("SignUp");
            }

            
        }
        public ActionResult UpdateProfile()
        {
            UserDTO us = new UserDTO();
            DBManager db = new DBManager();
            int id = Convert.ToInt32(Session["ID"]);
            List<UserDTO> list = db.getUserById(id);

            return View("UpdateProfile",list);
        }
        [HttpGet]
        public ActionResult AdminViewProducts()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/home/Login");
            }
            else if (Convert.ToInt32(Session["isAdmin"]) == 0)
            {
                string v = Convert.ToString(Session["login"]);
                int val = Convert.ToInt32(Session["isAdmin"]);
                DBManager db = new DBManager();
                List<ProductDTO> list;
                list = db.getAllProducts();
                return View("ViewProduct", list);
            }
            else
            {
                DBManager db = new DBManager();
                List<ProductDTO> list;
                list = db.getAllProducts();
                TempData["msg"] = "Success";
                return View("AdminViewProducts", list);
            }

        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            if (Session["Login"] == null)
            {
                return Redirect("~/Home/Login");
            }
            else
            {
                DBManager db = new DBManager();
                var list = db.getAllType();
                return View("AddProduct", list);
            }
        }
        [HttpPost]
        public ActionResult SaveProduct()
        {
            DBManager db = new DBManager();
            ProductDTO pd = new ProductDTO();
            pd.name = Request.Form["name"]; //Extract data from Request.Form by providing field 'name'
            pd.price = Convert.ToDouble(Request.Form["price"]); //Extract data from Request directly by providing field 'name'
            pd.typeid = Convert.ToInt32(Request.Form["type"]);

            pd.description = Request.Form["description"];
            var uniqueName = "";
            if (Request.Files["picture"] != null)
            {
                var file = Request.Files["picture"];
                var fileName = file.FileName;
                if (fileName != "")
                {
                    var ext = System.IO.Path.GetExtension(fileName);
                    uniqueName = Guid.NewGuid().ToString() + ext;
                    var rootPath = Server.MapPath("~/UploadFile");
                    var fileSavePath = System.IO.Path.Combine(rootPath, uniqueName);
                    file.SaveAs(fileSavePath);
                    pd.picURL = uniqueName;
                }
            }
            pd.updatedOn = DateTime.Now;
            pd.updatedBy = Convert.ToInt32(Session["ID"]);
            pd.IsActive = 1;
            int count = db.saveProduct(pd);
            if (count >= 0)
            {
                ViewBag.msg = "Success";
                return Redirect("~/Home/AdminViewProducts");
            }
            else
            {
                ViewBag.msg = "Error";
                return Redirect("~/Home/AdminViewProducts");
            }
        }
        public ActionResult logout()
        {
            Session["ID"] = null;
            Session["login"] = null;
            Session["isAdmin"] = null;
            return Redirect("~/Home/Login");
        }


        public ActionResult userViewProducts()
        {
            DBManager db = new DBManager();
            var list = db.getAllProducts();
            return View("viewProduct", list);
        }
    }
}