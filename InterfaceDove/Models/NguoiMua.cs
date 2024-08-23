using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterfaceDove.Models
{
    public class NguoiMua
    {
        [Display (Name = "Mã người mua")]
        public int MaNguoiMua { get; set; }

        [Display(Name = "SDT")][StringLength(10)]
        public string SoDienThoai { get; set; }

        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }

        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime NgayTao { get; set; }
        [Display(Name = "Tỉnh/Thành phố")]
        public string TinhThanh { get; set; }

        [Display(Name = "Quận/Huyện")]
        public string QuanHuyen { get; set; }
    }
}