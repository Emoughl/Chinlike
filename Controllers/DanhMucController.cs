using Chinlike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chinlike.Controllers
{
    public class DanhMucController : Controller
    {
        DataChinlikeDataContext db = new DataChinlikeDataContext();
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(db.DanhMucs.ToList());
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
        public ActionResult Create(Product danhmuc)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                db.Products.InsertOnSubmit(danhmuc);
                db.SubmitChanges();

                return RedirectToAction("Index", "DanhMuc");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var danhmuc = from tl in db.DanhMucs where tl.MaDM == id select tl;
                return View(danhmuc.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                DanhMuc danhmuc = db.DanhMucs.SingleOrDefault(n => n.MaDM == id);
                db.DanhMucs.DeleteOnSubmit(danhmuc);
                db.SubmitChanges();

                return RedirectToAction("Index", "DanhMuc");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var danhmuc = from tl in db.DanhMucs where tl.MaDM == id select tl;
                return View(danhmuc.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                DanhMuc danhmuc = db.DanhMucs.SingleOrDefault(n => n.MaDM == id);

                UpdateModel(danhmuc);
                db.SubmitChanges();
                return RedirectToAction("Index", "DanhMuc");
            }
        }
    }
}