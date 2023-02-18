using Chinlike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chinlike.Controllers
{
    public class MauSacController : Controller
    {
        DataChinlikeDataContext db = new DataChinlikeDataContext();
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(db.MauSacs.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View();
        }
        [HttpPost]
        public ActionResult Create(Product mausac)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                db.Products.InsertOnSubmit(mausac);
                db.SubmitChanges();

                return RedirectToAction("Index", "MauSac");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var mausac = from tl in db.MauSacs where tl.MaMau == id select tl;
                return View(mausac.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                MauSac mausac = db.MauSacs.SingleOrDefault(n => n.MaMau == id);
                db.MauSacs.DeleteOnSubmit(mausac);
                db.SubmitChanges();

                return RedirectToAction("Index", "MauSac");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var mausac = from tl in db.MauSacs where tl.MaMau == id select tl;
                return View(mausac.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                MauSac mausac = db.MauSacs.SingleOrDefault(n => n.MaMau == id);

                UpdateModel(mausac);
                db.SubmitChanges();
                return RedirectToAction("Index", "MauSac");
            }
        }
    }
}