using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceDove.Models
{
	public class ChiTietSanPham : SanPham
	{
		public List<DonHang> DanhSachDonHang { get; set; }
        public ChiTietSanPham(SanPham sanPham, List<DonHang> danhSachDonHang)
        {
            this.MaSanPham = sanPham.MaSanPham;
            this.TenSanPham = sanPham.TenSanPham;
            this.Gia = sanPham.Gia;
            this.MaDanhMuc = sanPham.MaDanhMuc;
            this.MoTa = sanPham.MoTa;
            this.SoLuong = sanPham.SoLuong;
            this.HinhAnhTruoc = sanPham.HinhAnhTruoc;
            this.HinhAnhSau = sanPham.HinhAnhSau;
            this.Sao = sanPham.Sao;
            this.LanCuoiCapNhat = sanPham.LanCuoiCapNhat;
            this.DanhSachDonHang = danhSachDonHang.OrderByDescending(x => x.ThoiGianDatHang).ToList();
        }
    }
}