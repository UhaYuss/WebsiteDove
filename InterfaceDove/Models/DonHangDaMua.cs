using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceDove.Models
{
    public class DonHangDaMua : NguoiMua
    {
        public List<DonHang> DonHangDaMuaList { get; set; }
        public DonHangDaMua(NguoiMua nguoiMua, List<DonHang> donHangList)
        {
            this.MaNguoiMua = nguoiMua.MaNguoiMua;
            this.SoDienThoai = nguoiMua.SoDienThoai;
            this.MatKhau = nguoiMua.MatKhau;
            this.HoTen = nguoiMua.HoTen;
            this.Email = nguoiMua.Email;
            this.DiaChi = nguoiMua.DiaChi;
            this.NgayTao = nguoiMua.NgayTao;
            this.DonHangDaMuaList = donHangList;
        }
    }

}