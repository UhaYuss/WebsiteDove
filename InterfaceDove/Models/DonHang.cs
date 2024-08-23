using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterfaceDove.Models
{
    public class DonHang:NguoiMua
    {
        public int MaDonHang { get; set; }
        public string DiaChiNhanHang { get; set; }
        public int MaNguoiMua { get; set; }
        public string GhiChu { get; set; }
        public int ThanhTien { get; set; }
        public DateTime ThoiGianDatHang { get; set; }
    }
}