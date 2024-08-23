using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace InterfaceDove.Models
{
    public class DanhGia:NguoiMua
    {
        public int MaPhanHoi { get; set; }
        public int MaNguoiMua { get; set; }
        public int MaSanPham { get; set; }
        [Display(Name = "Nội Dung")]
        public string NoiDung { get; set; }
        [Display(Name = "Sao")]
        public int Sao { get; set; }
    }
}