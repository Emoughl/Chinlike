using Chinlike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chinlike.Controllers
{
    public class SanPhamTheoDanhMucController : Controller
    {
        DataChinlikeDataContext db = new DataChinlikeDataContext();
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(db.Mains.ToList());
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
        public ActionResult Create(Main main)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                db.Mains.InsertOnSubmit(main);
                db.SubmitChanges();

                return RedirectToAction("Index", "SanPhamTheoDanhMuc");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var main = from tl in db.Mains where tl.MaMain == id select tl;
                return View(main.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                Main main = db.Mains.SingleOrDefault(n => n.MaMain == id);
                db.Mains.DeleteOnSubmit(main);
                db.SubmitChanges();

                return RedirectToAction("Index", "SanPhamTheoDanhMuc");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var main = from tl in db.Mains where tl.MaMain == id select tl;
                return View(main.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                Main main = db.Mains.SingleOrDefault(n => n.MaMain == id);

                UpdateModel(main);
                db.SubmitChanges();
                return RedirectToAction("Index", "SanPhamTheoDanhMuc");
            }
        }
    }
}