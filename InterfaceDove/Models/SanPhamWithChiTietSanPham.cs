using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceDove.Models
{
    public class SanPhamWithChiTietSanPham:DonHang
    {
        NguoiMuaConnect repo = new NguoiMuaConnect();
        public List<ChiTietDonHang> DanhSachChiTietDonHang { get; set; } = new List<ChiTietDonHang>();
        public List<SanPham> DanhSachSanPham = new List<SanPham>();

        public SanPhamWithChiTietSanPham(DonHang donHang, List<ChiTietDonHang> chiTietDonHangs)
        {
            MaDonHang = donHang.MaDonHang;
            MaNguoiMua = donHang.MaNguoiMua;
            HoTen = donHang.HoTen;
            SoDienThoai = donHang.SoDienThoai;
            DiaChiNhanHang = donHang.DiaChiNhanHang;
            QuanHuyen = donHang.QuanHuyen;
            TinhThanh = donHang.TinhThanh;
            GhiChu = donHang.GhiChu;
            ThanhTien = donHang.ThanhTien;
            ThoiGianDatHang = donHang.ThoiGianDatHang;
            DanhSachChiTietDonHang = chiTietDonHangs;
            foreach (ChiTietDonHang ct in DanhSachChiTietDonHang)
            {
                SanPham sanPham = repo.GetSanPhamByID(ct.MaSanPham);
                DanhSachSanPham.Add(sanPham);
            }
        }
    }
}