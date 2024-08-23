using InterfaceDove.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Linq;
namespace InterfaceDove.Controllers
{
	public class AdminController : Controller
    {
		private AdminConnect repo = new AdminConnect();
		private SanPhamConnect sanPhamRepo = new SanPhamConnect();

		public ActionResult DangNhap()
		{
			if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
			{
				return RedirectToAction("Index");
			}
			return View();
		}

		[HttpPost]
		public ActionResult XacThuc()
		{
			string username = HttpContext.Request["username"].Trim();
			string password = HttpContext.Request["password"].Trim();
			if (repo.IsAdmin(username, password))
			{
				Session["UserRole"] = "Admin";
				return RedirectToAction("Index");
			}
			else
			{
				ViewBag.LoginFailMessage = "Tên tài khoản hoặc mật khẩu không chính xác, vui lòng kiểm tra lại";
				return View("DangNhap");
			}
		}

		public ActionResult DangXuat()
		{
			if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
			{
				Session["UserRole"] = null;
			}
			return RedirectToAction("DangNhap");
		}

		public ActionResult Index()
		{
			string userRole = Session["UserRole"] as string;
			if (userRole == null || userRole != "Admin")
			{
				return RedirectToAction("DangNhap");
			}
			ViewBag.SoLuongDonHang = repo.GetSoLuongDonHang();
			ViewBag.TongGiaTriDaBan = repo.GetTongGiaTriDaBan();
			return View();
		}

		public ActionResult QuanLyNguoiMua()
		{
			string userRole = Session["UserRole"] as string;
			if (userRole == null || userRole != "Admin")
			{
				return RedirectToAction("DangNhap");
			}
			List<Models.NguoiMua> l = repo.GetNguoiMuas();
			return View(l);
		}

		public ActionResult ChiTietNguoiMua(string maNguoiMua)
		{
			string userRole = Session["UserRole"] as string;
			if (userRole == null || userRole != "Admin")
			{
				return RedirectToAction("DangNhap");
			}
			NguoiMua nm = repo.GetNguoiMua(maNguoiMua);
			List<DonHang> list = repo.GetDonHangs(maNguoiMua);
			DonHangDaMua dhdm = new DonHangDaMua(nm, list);
			if (nm == null)
			{
				return RedirectToAction("QuanLyNguoiMua");
			}
			ViewBag.SoLuongDonHang = sanPhamRepo.GetSoLuongDonDaMua(nm.MaNguoiMua.ToString());
			ViewBag.TongGiaTriDaMua = sanPhamRepo.GetTongGiaTriDaMua(nm.MaNguoiMua.ToString());
			return View(dhdm);
		}

		public ActionResult DanhSachSanPham()
		{
			string userRole = Session["UserRole"] as string;
			if (userRole == null || userRole != "Admin")
			{
				return RedirectToAction("DangNhap");
			}
			List<SanPham> sanPhams = sanPhamRepo.GetSanPhams();
			return View(sanPhams);
		}

		[HttpPost]
		public ActionResult DanhSachSanPhamDaLoc(string searchTerm)
		{
			string userRole = Session["UserRole"] as string;
			if (userRole == null || userRole != "Admin")
			{
				return RedirectToAction("DangNhap");
			}
			List<SanPham> sanPhams = sanPhamRepo.GetSanPhams();

			if (!string.IsNullOrEmpty(searchTerm))
			{
				searchTerm = searchTerm.ToLower(); 

				sanPhams = sanPhams
					.Where(sp =>
						sp.MaSanPham.ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
						sp.TenSanPham.ToLower().Contains(searchTerm))
					.OrderBy(sp => sp.MaSanPham)
					.ToList();
			}

			return View("DanhSachSanPham", sanPhams);
		}

		public ActionResult ThemSanPham()
		{
			string userRole = Session["UserRole"] as string;
			if (userRole == null || userRole != "Admin")
			{
				return RedirectToAction("DangNhap");
			}
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddSanPham(SanPham sanPham)
		{
			string userRole = Session["UserRole"] as string;
			if (userRole == null || userRole != "Admin")
			{
				return RedirectToAction("DangNhap");
			}
			if (ModelState.IsValid)
			{
				try
				{
					if (sanPham.HinhAnhTruocFile != null && sanPham.HinhAnhTruocFile.ContentLength > 0)
					{
						var fileNameTruoc = Path.GetFileName(sanPham.HinhAnhTruocFile.FileName);
						var filePathTruoc = Path.Combine(Server.MapPath("~/img/Product"), fileNameTruoc);
						sanPham.HinhAnhTruocFile.SaveAs(filePathTruoc);
						sanPham.HinhAnhTruoc = fileNameTruoc;
					}
					if (sanPham.HinhAnhSauFile != null && sanPham.HinhAnhSauFile.ContentLength > 0)
					{
						var fileNameSau = Path.GetFileName(sanPham.HinhAnhSauFile.FileName);
						var filePathSau = Path.Combine(Server.MapPath("~/img/Product"), fileNameSau);
						sanPham.HinhAnhSauFile.SaveAs(filePathSau);
						sanPham.HinhAnhSau = fileNameSau;
					}
					sanPham.Sao = 0;
					sanPham.LanCuoiCapNhat = DateTime.Now;
					sanPhamRepo.AddSanPham(sanPham);
					return RedirectToAction("DanhSachSanPham");
				}
				catch
				{
					ModelState.AddModelError("", "Đã xảy ra lỗi khi thêm sản phẩm. Vui lòng thử lại.");
				}
			}
			return View("ThemSanPham", sanPham);
		}
		
		public ActionResult EditSanPham(int id)
		{
			string userRole = Session["UserRole"] as string;
			if (userRole == null || userRole != "Admin")
			{
				return RedirectToAction("DangNhap");
			}
			SanPham sanPham = sanPhamRepo.GetSanPhamById(id);
			if (sanPham == null)
			{
				return View("DanhSachSanPham");
			}
			return View(sanPham);
		}

		[HttpPost]
		public ActionResult UpdateSanPham(SanPham sanPham)
		{
			string userRole = Session["UserRole"] as string;
			if (userRole == null || userRole != "Admin")
			{
				return RedirectToAction("DangNhap");
			}

			try
			{
				if (sanPham.HinhAnhTruocFile != null && sanPham.HinhAnhTruocFile.ContentLength > 0)
				{
					var fileNameTruoc = Path.GetFileName(sanPham.HinhAnhTruocFile.FileName);
					var filePathTruoc = Path.Combine(Server.MapPath("~/img/Product"), fileNameTruoc);
					if (System.IO.File.Exists(filePathTruoc))
					{
						ModelState.AddModelError("", "Tên file đã tồn tại. Vui lòng chọn một tên file khác.");
						return View("EditSanPham", sanPham);
					}

					sanPham.HinhAnhTruocFile.SaveAs(filePathTruoc);
					sanPham.HinhAnhTruoc = fileNameTruoc;
				}

				if (sanPham.HinhAnhSauFile != null && sanPham.HinhAnhSauFile.ContentLength > 0)
				{
					var fileNameSau = Path.GetFileName(sanPham.HinhAnhSauFile.FileName);
					var filePathSau = Path.Combine(Server.MapPath("~/img/Product"), fileNameSau);					
					if (System.IO.File.Exists(filePathSau))
					{
						ModelState.AddModelError("", "Tên file đã tồn tại. Vui lòng chọn một tên file khác.");
						return View("EditSanPham", sanPham);
					}
					sanPham.HinhAnhSauFile.SaveAs(filePathSau);
					sanPham.HinhAnhSau = fileNameSau;
				}

				sanPhamRepo.UpdateSanPham(sanPham);
				return RedirectToAction("DanhSachSanPham");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", $"Đã xảy ra lỗi khi cập nhật sản phẩm: {ex.Message}");
				return View("EditSanPham", sanPham);
			}
		}

		public ActionResult ThongKeSanPham(int id)
		{
			string userRole = Session["UserRole"] as string;
			if (userRole == null || userRole != "Admin")
			{
				return RedirectToAction("DangNhap");
			}
			SanPham sanPham = sanPhamRepo.GetSanPhamById(id);
			List<DonHang> dsdh = repo.GetDanhSachDonDaBanCuaSanPham(id);
			ChiTietSanPham ctsp = new ChiTietSanPham(sanPham, dsdh);
			if (sanPham == null)
			{
				return View("DanhSachSanPham");
			}
			ViewBag.TongGiaTriDaBanCuaSanPham = sanPhamRepo.GetTongGiaTriDaBanCuaSanPham(id);
			ViewBag.SoLuongSanPhamDaBan = sanPhamRepo.GetSoLuongSanPhamDaBan(id);
			return View(ctsp);
		}

		public ActionResult DanhSachDonHang()
		{
			string userRole = Session["UserRole"] as string;
			if (userRole == null || userRole != "Admin")
			{
				return RedirectToAction("DangNhap");
			}
			List<DonHang> list = repo.GetDonHangs().OrderBy(dh => dh.ThoiGianDatHang).ToList();
			return View(list);
		}

		public ActionResult ChiTietDonHangView(int id)
		{
			string userRole = Session["UserRole"] as string;
			if (userRole == null || userRole != "Admin")
			{
				return RedirectToAction("DangNhap");
			}
			List<ChiTietDonHang> chiTietDonHangs = sanPhamRepo.GetChiTietDonHangByMaDonHang(id);
			DonHang dh = sanPhamRepo.GetDonHangById(id);
			ChiTietThongTinDonHang ct = new ChiTietThongTinDonHang(dh, chiTietDonHangs);
			return View(ct);
		}

		public ActionResult DanhSachDanhGia()
		{
			string userRole = Session["UserRole"] as string;
			if (userRole == null || userRole != "Admin")
			{
				return RedirectToAction("DangNhap");
			}
			List<DanhGia> l = sanPhamRepo.GetDanhGias();
			return View(l);
		}
		public ActionResult DeleteDanhGia(int id)
		{
			string userRole = Session["UserRole"] as string;
			if (userRole == null || userRole != "Admin")
			{
				return RedirectToAction("DangNhap");
			}
			sanPhamRepo.DeleteDanhGia(id);
			return RedirectToAction("DanhSachDanhGia");
		}

	}
}