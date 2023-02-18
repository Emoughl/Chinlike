using Chinlike.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chinlike.Controllers
{
    public class KindController : Controller
    {
        DataChinlikeDataContext data = new DataChinlikeDataContext();
        // GET: Kind
        private List<Product> Sanphammoi(int count)
        {
            return data.Products.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var products = Sanphammoi(20);
            return View(products.ToPagedList(pageNum, pageSize));
        }
        public ActionResult DanhMuc()
        {
            var danhmuc = from ta in data.DanhMucs select ta;
            return PartialView(danhmuc);
        }
        public ActionResult SanPhamTheoDanhMuc(int id, int? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var products = from ma in data.Products
                         join ta in data.Mains on ma.MaP equals ta.MaP
                         where ta.MaDM == id
                         select ma;
            return View(products.ToPagedList(pageNum, pageSize));

        }
        public ActionResult MauSac()
        {
            var mau = from mb in data.MauSacs select mb;
            return PartialView(mau);
        }
        public ActionResult SanPhamTheoMauSac(int id, int? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var products = from mb in data.Products
                         join tb in data.Maus on mb.MaP equals tb.MaP
                         where tb.MaMau == id
                         select mb;
            return View(products.ToPagedList(pageNum, pageSize));
        }
        public ActionResult KichCo()
        {
            var size = from mc in data.KichCos select mc;
            return PartialView(size);
        }
        public ActionResult SanPhamTheoKichCo(int id, int? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var products = from mc in data.Products
                           join tc in data.Sizes on mc.MaP equals tc.MaP
                           where tc.MaKC == id
                           select mc;
            return View(products.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Gia()
        {
            var gia = from md in data.Gias select md;
            return PartialView(gia);
        }
        public ActionResult SanPhamTheoGia(int id, int? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var products = from md in data.Products
                         join td in data.GiaDos on md.MaP equals td.MaP
                         where td.MaGia == id
                         select md;
            return View(products.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Search(String a)
        {
            var products = data.Products.Where(s => s.TenP.Contains(a)).ToList();
            return View(products);
        }
        public ActionResult Chitietsanpham(int id)
        {
            var products = from s in data.Products where s.MaP == id select s;
            return View(products.Single());
        }
    }
}