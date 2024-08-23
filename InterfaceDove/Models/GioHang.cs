using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterfaceDove.Models
{
    public class GioHang
    {
        public int MaGH { get; set; }
        public int MaNM { get; set; }
        public int MaSP { get; set; }
        public int MaDM { get; set; }
        [Display(Name = "Số lượng")]
        public int SL { get; set; }
        [Display(Name = "Ảnh Mặt Trước")]
        public string HinhAnhTruoc { get ; set ; }
        [Display(Name = "Tên SP")]
        public string TenSanPham { get; set; }
        [Display(Name = "Tên Danh Mục")]
        public string TenDanhMuc { get; set; }
        [Display(Name = "Giá")]
        public int Gia { get; set; }
        [Display(Name = "Tổng tiền")]
        public int TT { get; set; }

    }
}