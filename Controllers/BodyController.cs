using Chinlike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Chinlike.Controllers
{
    public class BodyController : Controller
    {
        DataChinlikeDataContext data = new DataChinlikeDataContext();
        private List<Product> Laysanphammoi(int count)
        {
            return data.Products.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        // GET: Body
        public ActionResult Index(int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var sanphammoi = Laysanphammoi(22);
            return View(sanphammoi.ToPagedList(pageNum, pageSize));
        }
        public ActionResult SPHot()
        {
            var sphot = from na in data.SPHots select na;
            return PartialView(sphot);
        }
        public ActionResult IDHot(int id, int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var sanpham = from a in data.Products
                          join na in data.IdHots on a.MaP equals na.MaP
                          where na.MaSPHot == id
                          select a;
            return View(sanpham.ToPagedList(pageNum, pageSize));
        }
        public ActionResult New()
        {
            var news = from nb in data.News select nb;
            return PartialView(news);
        }
        public ActionResult IDNew(int id, int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var news = from b in data.Products
                       join nb in data.IDNews on b.MaP equals nb.MaP
                       where nb.MaNew == id
                       select b;
            return View(news.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Chitietsanpham(int id)
        {
            var sanpham = from s in data.Products where s.MaP == id select s;
            return View(sanpham.Single());
        }
    }
}