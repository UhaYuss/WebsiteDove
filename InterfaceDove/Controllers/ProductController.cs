using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InterfaceDove.Models;

namespace InterfaceDove.Controllers
{
    public class ProductController : Controller
    {
        SanPhamConnect sp = new SanPhamConnect();
        NguoiMuaConnect nmrepo = new NguoiMuaConnect();
        public ActionResult DanhMucSanPham(int id)
        {
            var sanPhamViewModel = new DanhMucViewModel();
            sanPhamViewModel.SanPhams = sp.getType(id);
            sanPhamViewModel.DanhMuc = sp.getDanhMucById(id);
            return View(sanPhamViewModel);
        }
        public ActionResult ShowSanPham(int id)
        {
            var showSP = sp.GetSanPhamWithRateById(id);

            var danhGiaList = sp.GetDanhGiaBySanPhamId(id);
            var allSP = sp.GetSanPhams();
            showSP.DanhGiaList = danhGiaList;
            showSP.SanPhams = allSP;
            return View(showSP);
        }
        public ActionResult AddDanhGia(int id, int maNM)
        {
            string userRole = Session["KHRole"] as string;

            if (userRole == null)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Id = id;
                ViewBag.MaNguoiMua = maNM;
                return View();
            }    
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDanhGia(int MaNguoiMua, int MaSanPham, string NoiDung, int Sao)
        {
            try
            {
                // Check if the user has already reviewed the product
                bool hasReviewed = sp.HasUserReviewedProduct(MaNguoiMua, MaSanPham);

                if (hasReviewed)
                {
                    // Redirect to some error page or show an error message
                    ViewBag.ErrorMessage = "You have already reviewed this product.";
                    return View();
                }
                int result = sp.AddDanhGia(MaNguoiMua, MaSanPham, NoiDung, Sao);

                if (result > 0)
                    return RedirectToAction("MultipleOrdersView", "Home");
                else
                    return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }


    }
}