using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceDove.Models
{
    public class ChiTietDonHang
    {
        public int MaChiTietDH { get; set; }
        public int MaDonHang { get; set; }
        public int MaSanPham { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }

        public virtual DonHang DonHang { get; set; }
        public virtual SanPham SanPham { get; set; }
        public GioHang gioHang { get; set; }
        public ChiTietDonHang() { }
    }
}