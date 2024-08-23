using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace InterfaceDove.Models
{
    public class SanPham
    {
        int maSanPham;
        string tenSanPham;
        int gia;
        int maDanhMuc;   
        string moTa;
        int soLuong;
        string hinhAnhTruoc;
        string hinhAnhSau;
        float sao;
        DateTime lanCuoiCapNhat;
        [Display (Name ="Mã SP")]
        public int MaSanPham { get => maSanPham; set => maSanPham = value; }
        [Display (Name = "Tên SP")]
        public string TenSanPham { get => tenSanPham; set => tenSanPham = value; }
        [Display(Name = "Giá")]
        public int Gia { get => gia; set => gia = value; }
        [Display(Name ="Mã Danh Mục")]
        public int MaDanhMuc { get => maDanhMuc; set => maDanhMuc = value; }
        [Display(Name = "Tên Danh Mục")]
        public string TenDanhMuc { get; set; }
        [Display(Name ="Mô tả")]
        public string MoTa { get => moTa; set => moTa = value; }
        [Display(Name = "Số lượng")]
        public int SoLuong { get => soLuong; set => soLuong = value; }
        [Display(Name = "Ảnh Mặt Trước")]
        public string HinhAnhTruoc { get => hinhAnhTruoc; set => hinhAnhTruoc = value; }
        [Display(Name = "Ảnh Mặt Sau")]
        public string HinhAnhSau { get => hinhAnhSau; set => hinhAnhSau = value; }
        [Display(Name = "Đánh giá")]
        public float Sao { get => sao; set => sao = value; }
        [Display(Name = "Lần cuối cập nhật")]
        public DateTime LanCuoiCapNhat { get => lanCuoiCapNhat; set => lanCuoiCapNhat = value; }

        public SanPham() { }
        public SanPham(SanPham a)
        {
            MaSanPham = a.MaSanPham;
            TenSanPham = a.TenSanPham;
            Gia = a.Gia;
            MaDanhMuc = a.MaDanhMuc;
            MoTa = a.MoTa;
            SoLuong = a.SoLuong;
            HinhAnhTruoc = a.HinhAnhTruoc;
            HinhAnhSau = a.HinhAnhSau;
            Sao = a.Sao;
            LanCuoiCapNhat = a.LanCuoiCapNhat;
        }

        [NotMapped]
        public HttpPostedFileBase HinhAnhTruocFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase HinhAnhSauFile { get; set; }
    }
}