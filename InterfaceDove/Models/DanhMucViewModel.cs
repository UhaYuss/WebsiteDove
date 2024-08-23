using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceDove.Models
{
    public class DanhMucViewModel
    {
        public List<SanPham> SanPhams { get; set; }
        public DanhMuc DanhMuc { get; set; }
        public SanPham SanPham { get; set; }
    }
}