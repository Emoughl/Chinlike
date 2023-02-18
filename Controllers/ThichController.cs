using Chinlike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chinlike.Controllers
{
    public class ThichController : Controller
    {
        DataChinlikeDataContext db = new DataChinlikeDataContext();
    public List<Thich> Laysanphamthich()
    {
        List<Thich> lstThich = Session["Thich"] as List<Thich>;
        if (lstThich == null)
        {
            lstThich = new List<Thich>();
            Session["Thich"] = lstThich;
        }
        return lstThich;
    }
    public ActionResult ThemSanPhamThich(int iMaP, string strURL)
    {
        List<Thich> lstThich = Laysanphamthich();
        Thich sanpham = lstThich.Find(n => n.iMaP == iMaP);
        if (sanpham == null)
        {
            sanpham = new Thich(iMaP);
            lstThich.Add(sanpham);
            return Redirect(strURL);
        }
        else
        {
            sanpham.iSoluong++;
            return Redirect(strURL);
        }
    }
    private int TongSoLuong()
    {
        int iTongSoLuong = 0;
        List<Thich> lstThich = Session["Thich"] as List<Thich>;
        if (lstThich != null)
        {
            iTongSoLuong = lstThich.Sum(n => n.iSoluong);
        }
        return iTongSoLuong;
    }
    public ActionResult Thich()
    {
        List<Thich> lstThich = Laysanphamthich();
        if (lstThich.Count == 0)
        {
            return RedirectToAction("Index", "Kind");
        }
        ViewBag.Tongsoluong = TongSoLuong();
        return View(lstThich);
    }
    public ActionResult ThichPartial()
    {
        ViewBag.Tongsoluong = TongSoLuong();
        return PartialView();
    }
    public ActionResult XoaThich(int id)
    {
        List<Thich> lstThich = Laysanphamthich();
        Thich sanpham = lstThich.SingleOrDefault(n => n.iMaP == id);
        if (sanpham != null)
        {
            lstThich.RemoveAll(n => n.iMaP == id);
            return RedirectToAction("Thich");

        }
        if (lstThich.Count == 0)
        {
            return RedirectToAction("Index", "Kind");
        }
        return RedirectToAction("Thich");
    }
    public ActionResult Xoatatcasanphamthich()
    {
        List<Thich> lstThich = Laysanphamthich();
        lstThich.Clear();
        return RedirectToAction("Index", "Kind");
    }
}
}
