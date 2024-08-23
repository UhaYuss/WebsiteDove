using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceDove.Models
{
    public class SanPhamWithDanhGia: SanPham
    {
        public List<DanhGia> DanhGiaList { get; set; }
        public List<SanPham> SanPhams { get; set; }
    }
}