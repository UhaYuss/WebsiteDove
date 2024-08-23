using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InterfaceDove.Models
{
    public class NguoiMuaConnect : DbConnectionBase
    {
        public bool IsUserExist(string phoneNumber)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM NguoiMua WHERE SoDienThoai = @phoneNumber";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                cmd.Connection = connection;
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public int AddNguoiMua(NguoiMua nm)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();
                string sql = "INSERT INTO NguoiMua (SoDienThoai, HoTen, Email, DiaChi, MatKhau, NgayTao) " +
                             "VALUES (@SoDienThoai, @HoTen, @Email, @DiaChi, @MatKhau, @NgayTao)";
                cmd.CommandText = sql;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@SoDienThoai", nm.SoDienThoai);
                cmd.Parameters.AddWithValue("@HoTen", nm.HoTen);
                cmd.Parameters.AddWithValue("@Email", nm.Email);
                cmd.Parameters.AddWithValue("@DiaChi", nm.DiaChi);
                cmd.Parameters.AddWithValue("@MatKhau", nm.MatKhau);
                cmd.Parameters.AddWithValue("@NgayTao", DateTime.Now);

                return cmd.ExecuteNonQuery();
            }
        }
        public int EditThongTin(NguoiMua nm)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();
                string sql = "UPDATE NguoiMua SET SoDienThoai = @SoDienThoai, HoTen = @HoTen, Email = @Email, DiaChi = @DiaChi,QuanHuyen = @QuanHuyen,TinhThanh = @TinhThanh  WHERE MaNguoiMua = @MaNguoiMua";
                cmd.CommandText = sql;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@MaNguoiMua", nm.MaNguoiMua);
                cmd.Parameters.AddWithValue("@SoDienThoai", nm.SoDienThoai);
                cmd.Parameters.AddWithValue("@HoTen", nm.HoTen);
                cmd.Parameters.AddWithValue("@Email", nm.Email);
                cmd.Parameters.AddWithValue("@DiaChi", nm.DiaChi);
                cmd.Parameters.AddWithValue("@QuanHuyen", nm.QuanHuyen);
                cmd.Parameters.AddWithValue("@TinhThanh", nm.TinhThanh);
                return cmd.ExecuteNonQuery();
            }
        }

        public bool IsNguoiMua(string email, string password)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();
                string sql = "SELECT MatKhau FROM NguoiMua WHERE Email = @email";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Connection = connection;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return password == reader["MatKhau"].ToString();
                    }
                    return false;
                }
            }
        }


        public List<NguoiMua> GetNguoiMuas()
        {
            List<NguoiMua> l = new List<NguoiMua>();
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();
                string sql = "SELECT * FROM NguoiMua";
                cmd.CommandText = sql;
                cmd.Connection = connection;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NguoiMua nm = new NguoiMua();
                        nm.MaNguoiMua = reader.GetInt32(0);
                        nm.SoDienThoai = reader.GetString(1);
                        nm.MatKhau = reader.GetString(2);
                        nm.HoTen = reader.GetString(3);
                        nm.Email = reader.GetString(4);
                        nm.DiaChi = reader.GetString(5);
                        nm.TinhThanh = reader.GetString(6);
                        nm.QuanHuyen = reader.GetString(7);
                        nm.NgayTao = reader.GetDateTime(8);
                        l.Add(nm);
                    }
                }
            }
            return l;
        }
        public int LayIdNguoiMua(string username)
        {
            int maNguoiMua = -1;
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();
                string sql = "SELECT MaNguoiMua FROM NguoiMua WHERE Email = @username";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Connection = connection;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        maNguoiMua = reader.GetInt32(0);
                    }
                }
            }
            return maNguoiMua;
        }

        public NguoiMua GetNguoiMua(int maNguoiMua)
        {
            NguoiMua nm = new NguoiMua();
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();
                string sql = "SELECT * FROM NguoiMua WHERE MaNguoiMua = @maNguoiMua";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@maNguoiMua", maNguoiMua);
                cmd.Connection = connection;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        nm.MaNguoiMua = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                        nm.SoDienThoai = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        nm.MatKhau = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        nm.HoTen = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        nm.Email = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        nm.DiaChi = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        nm.TinhThanh = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        nm.QuanHuyen = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        nm.NgayTao = reader.IsDBNull(8) ? DateTime.MinValue : reader.GetDateTime(8);
                        return nm;
                    }
                }
            }
            return null;
        }

        public List<GioHang> GetGioHangs(int id)
        {
            List<GioHang> lstgh = new List<GioHang>();
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    conn.Open();
                    string sql = "SELECT sp.MaSanPham, sp.HinhAnhTruoc, sp.TenSanPham,dm.MaDanhMuc, dm.TenDanhMuc, sp.Gia, gh.SoLuong, gh.SoLuong * sp.Gia AS TongTien " +
                                 "FROM SanPham sp " +
                                 "INNER JOIN DanhMuc dm ON sp.MaDanhMuc = dm.MaDanhMuc " +
                                 "INNER JOIN GioHang gh ON gh.MaSanPham = sp.MaSanPham " +
                                 "INNER JOIN NguoiMua nm ON nm.MaNguoiMua = gh.MaNguoiMua " +
                                 "WHERE nm.MaNguoiMua = @id";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = conn;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GioHang gh = new GioHang();
                            gh.MaSP = int.Parse(reader["MaSanPham"].ToString());
                            gh.HinhAnhTruoc = reader["HinhAnhTruoc"].ToString();
                            gh.MaDM = int.Parse(reader["MaDanhMuc"].ToString());
                            gh.TenSanPham = reader["TenSanPham"].ToString();
                            gh.TenDanhMuc = reader["TenDanhMuc"].ToString();
                            gh.Gia = int.Parse(reader["Gia"].ToString());
                            gh.SL = int.Parse(reader["SoLuong"].ToString());
                            gh.TT = int.Parse(reader["TongTien"].ToString());
                            lstgh.Add(gh);
                        }
                    }
                }
            }
            return lstgh;
        }

        public void AddOrUpdateProductToCart(int nguoiMuaId, int sanPhamId)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();
                string checkIfExistsQuery = "SELECT SoLuong FROM GioHang WHERE MaNguoiMua = @NguoiMuaID AND MaSanPham = @SanPhamID";
                cmd.CommandText = checkIfExistsQuery;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@NguoiMuaID", nguoiMuaId);
                cmd.Parameters.AddWithValue("@SanPhamID", sanPhamId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int currentQuantity = reader.GetInt32(0);
                        reader.Close();
                        string updateQuery = "UPDATE GioHang SET SoLuong = @SoLuong WHERE MaNguoiMua = @NguoiMuaID AND MaSanPham = @SanPhamID";
                        cmd.CommandText = updateQuery;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@SoLuong", currentQuantity + 1);
                        cmd.Parameters.AddWithValue("@NguoiMuaID", nguoiMuaId);
                        cmd.Parameters.AddWithValue("@SanPhamID", sanPhamId);
                    }
                    else
                    {
                        reader.Close();
                        string insertQuery = "INSERT INTO GioHang (MaNguoiMua, MaSanPham, SoLuong) VALUES (@NguoiMuaID, @SanPhamID, 1)";
                        cmd.CommandText = insertQuery;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@NguoiMuaID", nguoiMuaId);
                        cmd.Parameters.AddWithValue("@SanPhamID", sanPhamId);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UpdateQuantity(int MaNguoiMua, int maSanPham, int soLuongMoi)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string updateQuery = "UPDATE GioHang SET SoLuong = @SoLuongMoi WHERE MaNguoiMua = @MaNguoiMua AND MaSanPham = @MaSanPham";
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.Add(new SqlParameter("@SoLuongMoi", soLuongMoi));
                    command.Parameters.Add(new SqlParameter("@MaNguoiMua", MaNguoiMua));
                    command.Parameters.Add(new SqlParameter("@MaSanPham", maSanPham));
                    command.ExecuteNonQuery();
                }
            }
        }
        public void AddOrUpdateProductToCartShop(int nguoiMuaId, int sanPhamId, int soLuong)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();
                string checkIfExistsQuery = "SELECT SoLuong FROM GioHang WHERE MaNguoiMua = @NguoiMuaID AND MaSanPham = @SanPhamID";
                cmd.CommandText = checkIfExistsQuery;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@NguoiMuaID", nguoiMuaId);
                cmd.Parameters.AddWithValue("@SanPhamID", sanPhamId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int currentQuantity = reader.GetInt32(0);
                        reader.Close();
                        int newQuantity = currentQuantity + soLuong;

                        string updateQuery = "UPDATE GioHang SET SoLuong = @SoLuongMoi WHERE MaNguoiMua = @NguoiMuaID AND MaSanPham = @SanPhamID";
                        cmd.CommandText = updateQuery;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@SoLuongMoi", newQuantity);
                        cmd.Parameters.AddWithValue("@NguoiMuaID", nguoiMuaId);
                        cmd.Parameters.AddWithValue("@SanPhamID", sanPhamId);
                    }
                    else
                    {
                        reader.Close();
                        string insertQuery = "INSERT INTO GioHang (MaNguoiMua, MaSanPham, SoLuong) VALUES (@NguoiMuaID, @SanPhamID, @SoLuong)";
                        cmd.CommandText = insertQuery;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@NguoiMuaID", nguoiMuaId);
                        cmd.Parameters.AddWithValue("@SanPhamID", sanPhamId);
                        cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(int MaNguoiMua, int maSP)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string deleteQuery = "DELETE FROM GioHang WHERE MaNguoiMua = @MaNguoiMua AND MaSanPham = @MaSanPham";
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.Add(new SqlParameter("@MaNguoiMua", MaNguoiMua));
                    command.Parameters.Add(new SqlParameter("@MaSanPham", maSP));
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteAllProduct(int MaNguoiMua)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string deleteQuery = "DELETE FROM GioHang WHERE MaNguoiMua = @MaNguoiMua";
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.Add(new SqlParameter("@MaNguoiMua", MaNguoiMua));
                    command.ExecuteNonQuery();
                }
            }
        }
        public SanPham GetSanPhamByID(int id)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT TenSanPham, HinhAnhTruoc FROM SanPham WHERE MaSanPham = @maSanPham";
                cmd.Parameters.AddWithValue("@maSanPham", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SanPham sanPham = new SanPham
                        {
                            TenSanPham = reader["TenSanPham"].ToString(),
                            HinhAnhTruoc = reader["HinhAnhTruoc"].ToString(),
                        };

                        return sanPham;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public List<ChiTietDonHang> GetChiTietDonHangByMaDonHang(int maDonHang)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();

                string sql = "SELECT * FROM ChiTietDonHang WHERE MaDonHang = @maDonHang";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@maDonHang", maDonHang);
                cmd.Connection = connection;

                List<ChiTietDonHang> chiTietDonHangs = new List<ChiTietDonHang>();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ChiTietDonHang chiTietDonHang = new ChiTietDonHang
                        {
                            MaChiTietDH = Convert.ToInt32(reader["MaChiTietDH"]),
                            MaDonHang = Convert.ToInt32(reader["MaDonHang"]),
                            MaSanPham = Convert.ToInt32(reader["MaSanPham"]),
                            SoLuong = Convert.ToInt32(reader["SoLuong"]),
                            DonGia = Convert.ToInt32(reader["DonGia"])
                        };
                        chiTietDonHangs.Add(chiTietDonHang);
                    }
                }

                return chiTietDonHangs;
            }
        }
        public void InsertDanhGia(int maNguoiMua, int maSanPham, string noiDung, int sao)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO DanhGia (MaNguoiMua, MaSanPham, NoiDung, Sao) VALUES (@MaNguoiMua, @MaSanPham, @NoiDung, @Sao)";
                    cmd.Parameters.AddWithValue("@MaNguoiMua", maNguoiMua);
                    cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                    cmd.Parameters.AddWithValue("@NoiDung", noiDung);
                    cmd.Parameters.AddWithValue("@Sao", sao);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertDonHangAndChiTietDonHang(NguoiMua nguoiMua, List<GioHang> gioHangs)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand donHangCommand = new SqlCommand("INSERT INTO DonHang (MaNguoiMua, SoDienThoai, DiaChi, TinhThanh, QuanHuyen, ThoiGianDatHang) " +
                                                                   "VALUES (@MaNguoiMua, @SoDienThoai, @DiaChi, @TinhThanh, @QuanHuyen, @ThoiGianDatHang); " +
                                                                   "SELECT SCOPE_IDENTITY();", connection))
                {
                    donHangCommand.Parameters.AddWithValue("@MaNguoiMua", nguoiMua.MaNguoiMua);
                    donHangCommand.Parameters.AddWithValue("@SoDienThoai", nguoiMua.SoDienThoai);
                    donHangCommand.Parameters.AddWithValue("@DiaChi", nguoiMua.DiaChi);
                    donHangCommand.Parameters.AddWithValue("@TinhThanh", nguoiMua.TinhThanh);
                    donHangCommand.Parameters.AddWithValue("@QuanHuyen", nguoiMua.QuanHuyen);
                    donHangCommand.Parameters.AddWithValue("@ThoiGianDatHang", DateTime.Now);

                    int maDonHang = Convert.ToInt32(donHangCommand.ExecuteScalar());
                    foreach (GioHang gioHang in gioHangs)
                    {
                        using (SqlCommand chiTietDonHangCommand = new SqlCommand("InsertChiTietDonHang", connection))
                        {
                            chiTietDonHangCommand.CommandType = CommandType.StoredProcedure;
                            chiTietDonHangCommand.Parameters.AddWithValue("@MaDonHang", maDonHang);
                            chiTietDonHangCommand.Parameters.AddWithValue("@MaSanPham", gioHang.MaSP);
                            chiTietDonHangCommand.Parameters.AddWithValue("@SoLuong", gioHang.SL);
                            chiTietDonHangCommand.Parameters.AddWithValue("@DonGia", gioHang.Gia);

                            chiTietDonHangCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }


    }

}
