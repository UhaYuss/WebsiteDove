using System.Collections.Generic;

namespace InterfaceDove.Models
{
    public class GioHangWithDonHang : DonHang
    {
        SanPhamConnect repo = new SanPhamConnect();
        public List<ChiTietDonHang> Gh { get; set; }

        public GioHangWithDonHang(NguoiMua nm, List<GioHang> gioHangs)
        {
            MaNguoiMua = nm.MaNguoiMua;
            SoDienThoai = nm.SoDienThoai;
            DiaChiNhanHang = nm.DiaChi;
            TinhThanh = nm.TinhThanh;
            QuanHuyen = nm.QuanHuyen;

            Gh = new List<ChiTietDonHang>();

            foreach (GioHang gioHangItem in gioHangs)
            {
                SanPham sanPham = repo.GetSanPhamById(gioHangItem.MaSP);
                ChiTietDonHang chiTietDonHang = new ChiTietDonHang
                {
                    MaSanPham = gioHangItem.MaSP,
                    SoLuong = gioHangItem.SL,
                    DonGia = gioHangItem.Gia,
                    SanPham = sanPham,
                };

                chiTietDonHang.DonHang = this;
                chiTietDonHang.gioHang = new GioHang();

                Gh.Add(chiTietDonHang);
            }
        }
    }
}
