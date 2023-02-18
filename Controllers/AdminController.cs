using Chinlike.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chinlike.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DataChinlikeDataContext db = new DataChinlikeDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            var tendn = f["txtuser"];
            var matkhau = f["txtpass"];
            if (String.IsNullOrEmpty(tendn))
                ViewData["Loi1"] = "Vui lòng nhập tên đăng nhập";
            else if (String.IsNullOrEmpty(matkhau))
                ViewData["Loi2"] = "Vui lòng nhập mật khẩu";
            else
            {
                var ad = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không hợp lệ";
            }

            return View();
        }
        public ActionResult Sanpham(int? page)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                int pagesize = 7;
                int pagenum = (page ?? 1);
                return View(db.Products.ToList().OrderByDescending(n => n.MaP).ToPagedList(pagenum, pagesize));
            }
        }
        public ActionResult Chitietsanpham(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var products  = from s in db.Products where s.MaP == id select s;
                return View(products.SingleOrDefault());
            }
        }
        [HttpGet]
        public ActionResult Xoasanpham(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var products = from s in db.Products where s.MaP == id select s;
                return View(products.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Xoasanpham")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                Product product = db.Products.SingleOrDefault(n => n.MaP == id);
                db.Products.DeleteOnSubmit(product);
                db.SubmitChanges();
                return RedirectToAction("Sanpham", "Admin");
            }
        }
        [HttpGet]
        public ActionResult Themmoisanpham()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
                ViewBag.MaMau = new SelectList(db.MauSacs.ToList().OrderBy(n => n.TenMau), "MaMau", "TenMau");
                ViewBag.MaKC = new SelectList(db.KichCos.ToList().OrderBy(n => n.TenKC), "MaKC", "TenKC");

                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoisanpham(Product product, HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                if (fileUpload == null)
                {
                    ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                    return View();
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        var path = Path.Combine(Server.MapPath("~/assets/img/ImagesBody"), fileName);
                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {
                            fileUpload.SaveAs(path);
                        }
                        product.Anh = fileName;
                        db.Products.InsertOnSubmit(product);
                        db.SubmitChanges();
                    }
                    return RedirectToAction("Sanpham", "Admin");
                }
            }
        }

        public ActionResult Suasanpham(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                Product product = db.Products.SingleOrDefault(n => n.MaP == id);
                return View(product);
            }
        }
        [HttpPost, ActionName("Suasanpham")]
        public ActionResult Xacnhansua(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                Product product = db.Products.SingleOrDefault(n => n.MaP == id);
                UpdateModel(product);
                db.SubmitChanges();
                return RedirectToAction("Sanpham", "Admin");
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suasanpham(Product product, HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/assets/img/ImagesBody"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    product.Anh = fileName;
                    db.Products.InsertOnSubmit(product);
                    db.SubmitChanges();
                }
                return RedirectToAction("Sanpham");
            }
        }
    }
}