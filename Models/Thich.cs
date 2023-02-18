using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chinlike.Models
{
    public class Thich
    {
        DataChinlikeDataContext db = new DataChinlikeDataContext();
        public int iMaP { set; get; }
        public string sTenP { set; get; }
        public string sAnh { set; get; }
        public string sDonGia { set; get; }
        public int iSoluong { set; get; }
        public Thich(int MaP)
        {
            iMaP = MaP;
            Product product = db.Products.Single(n => n.MaP == iMaP);
            sTenP = product.TenP;
            sAnh = product.Anh;
            sDonGia = product.Gia;
            iSoluong = 1;
        }
    }
}