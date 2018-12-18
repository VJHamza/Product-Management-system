using System;
using System.Web.Mvc;
using System.Collections.Generic;
using MyDAL;
using Entities;

namespace Assignment5.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [HttpGet]
        public ActionResult Edit(int id)
        {
            DBManager db = new DBManager();
            List<TypeDTO> ls;
            ls = db.getAllType();
            ViewBag.lis = ls;
            List<ProductDTO> list = db.getProductById(id);
            return View("EditProduct", list);
        }
        [HttpPost]
        public ActionResult UpdateProduct(int id)
        {
            DBManager db = new DBManager();
            ProductDTO pd = new ProductDTO();
            int prodID = Convert.ToInt16(Request["id"]);
            string picName = Convert.ToString(Request["PictureName"]);
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
                else
                {
                    pd.picURL = picName;
                }
            }

            pd.updatedOn = DateTime.Now;
            pd.updatedBy = Convert.ToInt32(Session["ID"]);
            pd.IsActive = 1;
            int count = db.updateProductById(pd, id);
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
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ProductDTO p = new ProductDTO();
            DBManager db = new DBManager();
            List<ProductDTO> list = db.getProductById(id);
            p.productId = list[0].productId;
            p.typeid = list[0].typeid;
            p.typeName = list[0].typeName;
            p.description = list[0].description;
            p.picURL = list[0].picURL;
            p.updatedOn = DateTime.Now;
            p.updatedBy = Convert.ToInt32(Session["ID"]);
            p.IsActive = 0;
            int count = db.deleteProductById(p, id);
            if (count > 0)
            {
                TempData["MSG"] = "Success";
                return Redirect("~/Home/AdminViewProducts");
            }
            else
            {
                TempData["MSG"] = "Failure";
                return Redirect("~/Home/AdminViewProducts");

            }
        }
    }
}