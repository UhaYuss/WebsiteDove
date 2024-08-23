using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InterfaceDove.Models;
namespace InterfaceDove.Controllers
{
    public class HomeController : Controller
    {
        NguoiMuaConnect repo = new NguoiMuaConnect();
        SanPhamConnect sprepo = new SanPhamConnect();
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XacThucDangKy()
        {
            ViewBag.UserExistMessage = null;
            ViewBag.CreateUserFailMessage = null;
            ViewBag.CreateUserSuccessMessage = null;
            string soDienThoai = HttpContext.Request["SoDienThoai"].Trim();
            string matKhau = HttpContext.Request["MatKhau"];
            string matKhauNhapLai = HttpContext.Request["MatKhauNhapLai"];
            string hoTen = HttpContext.Request["HoTen"];
            string email = HttpContext.Request["Email"];
            string diaChi = HttpContext.Request["DiaChi"];
            if (repo.IsUserExist(soDienThoai))
            {
                ViewBag.UserExistMessage = "Số điện thoại đã được đăng ký";
                return View("DangKy");
            }

            NguoiMua nguoiMua = new NguoiMua
            {
                SoDienThoai = soDienThoai,
                MatKhau = matKhau,
                HoTen = hoTen,
                Email = email,
                DiaChi = diaChi
            };

            int rowEffected = repo.AddNguoiMua(nguoiMua);
            if (rowEffected > 0)
            {
                ViewBag.CreateUserSuccessMessage = "Tài khoản đã được tạo thành công, đăng nhập để sử dụng";
                return View("DangNhap");
            }
            else
            {
                ViewBag.CreateUserFailMessage = "Tạo tài khoản thất bại, vui lòng thử lại";
                return View("DangKy");
            }
        }

        public ActionResult DangNhap()
        {
            if (Session["KHRole"] != null && Session["KHRole"].ToString() == "NguoiMua")
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
            NguoiMuaConnect repo = new NguoiMuaConnect();
            if (repo.IsNguoiMua(username, password))
            {
                int maNguoiMua = repo.LayIdNguoiMua(username);
                Session["IdKH"] = maNguoiMua;
                Session["KHRole"] = username;
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
            Session["KHRole"] = null;
            Session["IdKH"] = null;
            return RedirectToAction("DangNhap", "Home");
        }
        public ActionResult ThongTinNguoiMua(int maNguoiMua)
        {
            if (Session["KHRole"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                NguoiMua dbnm = repo.GetNguoiMua(maNguoiMua);
                return View(dbnm);
            }
        }
        public ActionResult LuuThongTinChinhSua(int maNguoiMua)
        {
            if (Session["KHRole"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                NguoiMua dbnm = repo.GetNguoiMua(maNguoiMua);
                return View(dbnm);
            }
        }
        [HttpPost]
        public ActionResult LuuThongTinChinhSua(NguoiMua nguoiMua)
        {
            if (ModelState.IsValid)
            {
                NguoiMuaConnect nm = new NguoiMuaConnect();
                int rowEffected = nm.EditThongTin(nguoiMua);

                if (rowEffected > 0)
                {
                    ViewBag.EditSuccessMessage = "Chỉnh sửa thông tin thành công";
                }
                else
                {
                    ViewBag.EditFailMessage = "Chỉnh sửa thông tin thất bại";
                }
            }

            return View("LuuThongTinChinhSua", nguoiMua);
        }
        public ActionResult Index()
        {
            SanPhamConnect sp = new SanPhamConnect();
            List<SanPham> lsp = sp.GetSanPhams().OrderByDescending(s => s.Sao).Take(12).ToList();
            return View(lsp);
        }

        public ActionResult TatCaSanPham()
        {
            SanPhamConnect sp = new SanPhamConnect();
            List<SanPham> lsp = sp.GetSanPhams();
            return View(lsp);
        }
        public ActionResult SearchPD(string name)
        {
            SanPhamConnect sp = new SanPhamConnect();
            List<SanPham> lsp = sp.GetSanPhamByName(name);
            return View(lsp);

        }
        public ActionResult ShowGH()
        {
            if (Session["KHRole"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                NguoiMuaConnect nm = new NguoiMuaConnect();
                int maNM = (int)Session["IdKH"];
                List<GioHang> ctdh = nm.GetGioHangs(maNM);
                return View(ctdh);
            }
        }

        [HttpPost]
        public ActionResult AddSPtoCart(int mSP)
        {
            if (Session["IdKH"] != null)
            {
                int maNM = (int)Session["IdKH"];
                repo.AddOrUpdateProductToCart(maNM, mSP);
                return RedirectToAction("TatCaSanPham");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult UpdateQuantity()
        {
            if (Session["IdKH"] != null)
            {
                int newQuan = Convert.ToInt32(HttpContext.Request.Form["quantity"]);
                int idProduct = Convert.ToInt32(HttpContext.Request.Form["id"]);
                int maNM = (int)Session["IdKH"];
                repo.UpdateQuantity(maNM, idProduct, newQuan);
                return RedirectToAction("ShowGH");
            }
            else { return RedirectToAction("Index"); }

        }
        [HttpPost]
        public ActionResult DeleteProductSL()
        {
            if (Session["IdKH"] != null)
            {
                int idProduct = Convert.ToInt32(HttpContext.Request.Form["id"]);
                int maNM = (int)Session["IdKH"];
                repo.DeleteProduct(maNM, idProduct);
                return RedirectToAction("ShowGH");
            }
            else { return RedirectToAction("Index"); }

        }
        public ActionResult DeleteProduct(int id)
        {
            if (Session["IdKH"] != null)
            {
                int maNM = (int)Session["IdKH"];
                repo.DeleteProduct(maNM, id);
                return RedirectToAction("ShowGH");
            }
            else { return RedirectToAction("Index"); }

        }
        public ActionResult DeleteAllProduct()
        {
            if (Session["IdKH"] != null)
            {
                int maNM = (int)Session["IdKH"];
                repo.DeleteAllProduct(maNM);
                return RedirectToAction("ShowGH");
            }
            else { return RedirectToAction("Index"); }

        }
        public ActionResult MultipleOrdersView()
        {
            string userRole = Session["KHRole"] as string;
            if (userRole == null)
            {
                return RedirectToAction("Index");
            }

            int maNM = (int)Session["IdKH"];
            List<DonHang> danhSachDonHang = sprepo.GetDonHangsByUserId(maNM);

            List<SanPhamWithChiTietSanPham> danhSachSanPhamWithChiTiet = new List<SanPhamWithChiTietSanPham>();

            foreach (DonHang dh in danhSachDonHang)
            {
                List<ChiTietDonHang> chiTietDonHangs = repo.GetChiTietDonHangByMaDonHang(dh.MaDonHang);
                SanPhamWithChiTietSanPham sanPhamWithChiTiet = new SanPhamWithChiTietSanPham(dh, chiTietDonHangs);
                danhSachSanPhamWithChiTiet.Add(sanPhamWithChiTiet);
            }

            return View(danhSachSanPhamWithChiTiet);
        }
        public ActionResult ThanhToan()
        {
            string userRole = Session["KHRole"] as string;
            if (userRole == null)
            {
                return RedirectToAction("Index");
            }
            int maNguoiMua = (int)Session["IdKH"];
            NguoiMua nguoiMua = repo.GetNguoiMua(maNguoiMua);
            List<GioHang> gioHangs = repo.GetGioHangs(maNguoiMua);
            GioHangWithDonHang gioHangWithDonHang = new GioHangWithDonHang(nguoiMua, gioHangs);
            return View(gioHangWithDonHang);
        }
        public ActionResult AddDonHang()
        {
            string userRole = Session["KHRole"] as string;
            if (userRole == null)
            {
                return RedirectToAction("Index");
            }

            int maNM = (int)Session["IdKH"];
            List<GioHang> ghs = repo.GetGioHangs(maNM);
            NguoiMua nm = repo.GetNguoiMua(maNM);
            repo.InsertDonHangAndChiTietDonHang(nm, ghs);
            repo.DeleteAllProduct(maNM);
            return RedirectToAction("Index");
        }


        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {

            return View();
        }

    }
}