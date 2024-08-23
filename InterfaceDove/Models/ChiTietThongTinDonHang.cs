using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceDove.Models
{
    public class ChiTietThongTinDonHang : DonHang
    {
        SanPhamConnect repo = new SanPhamConnect();
        public List<ChiTietDonHang> DanhSachChiTietDonHang { get; set; } = new List<ChiTietDonHang>();
        public List<string> DanhSachTenSanPham { get; set; } = new List<string>();

        public ChiTietThongTinDonHang(DonHang donHang, List<ChiTietDonHang> chiTietDonHangs)
        {
            MaDonHang = donHang.MaDonHang;
            MaNguoiMua = donHang.MaNguoiMua;
            SoDienThoai = donHang.SoDienThoai;
            DiaChiNhanHang = donHang.DiaChiNhanHang;
            GhiChu = donHang.GhiChu;
            ThanhTien = donHang.ThanhTien;
            ThoiGianDatHang = donHang.ThoiGianDatHang;
            DanhSachChiTietDonHang = chiTietDonHangs;
            foreach (ChiTietDonHang ct in DanhSachChiTietDonHang)
            {
                DanhSachTenSanPham.Add(repo.GetTenSanPhamById(ct.MaSanPham));
            }
        }
    }

}