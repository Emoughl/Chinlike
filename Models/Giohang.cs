using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chinlike.Models
{
    public class Giohang
    {
        DataChinlikeDataContext db = new DataChinlikeDataContext();
        public int iMaP { set; get; }
        public string sTenP { set; get; }
        public string sAnh { set; get; }
        public Double dDonGia { set; get; }
        public int iSoLuong { set; get; }
        public Double dThanhtien
        {
            get { return iSoLuong * dDonGia; }

        }
        public Giohang(int MaP)
        {
            iMaP = MaP;
            Product product = db.Products.Single(n => n.MaP == iMaP);
            sTenP = product.TenP;
            sAnh = product.Anh;
            dDonGia = double.Parse(product.Gia.ToString());
            iSoLuong = 1;
        }
    }
}
