using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InterfaceDove.Models
{
    public class SanPhamConnect : DbConnectionBase
    {
        public List<SanPham> GetSanPhams()
        {
            List<SanPham> list = new List<SanPham>();

            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();
                string sql = "SELECT * FROM SanPham";
                cmd.CommandText = sql;
                cmd.Connection = connection;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SanPham sp = new SanPham();
                        sp.MaSanPham = Convert.ToInt32(reader["MaSanPham"]);
                        sp.TenSanPham = reader["TenSanPham"].ToString();
                        sp.Gia = Convert.ToInt32(reader["Gia"]);
                        sp.MaDanhMuc = Convert.ToInt32(reader["MaDanhMuc"]);
                        sp.MoTa = reader["MoTa"].ToString();
                        sp.SoLuong = Convert.ToInt32(reader["SoLuong"]);
                        sp.HinhAnhTruoc = reader["HinhAnhTruoc"].ToString();
                        sp.HinhAnhSau = reader["HinhAnhSau"].ToString();
                        sp.Sao = float.Parse(reader["Sao"].ToString());
                        sp.LanCuoiCapNhat = DateTime.Parse(reader["LanCuoiCapNhat"].ToString());

                        list.Add(sp);
                    }
                }
            }
            return list;
        }

        public int AddSanPham(SanPham a)
        {
            int rowEffected = 0;
            try
            {
                using (SqlConnection connection = GetConnection())
                using (SqlCommand cmd = new SqlCommand())
                {
                    connection.Open();
                    cmd.Connection = connection;

                    cmd.CommandText = "INSERT INTO SANPHAM (TenSanPham, Gia, MaDanhMuc, MoTa, SoLuong, HinhAnhTruoc, HinhAnhSau, Sao, LanCuoiCapNhat) VALUES (@TenSanPham, @Gia, @MaDanhMuc, @MoTa, @SoLuong, @HinhAnhTruoc, @HinhAnhSau, @Sao, @LanCuoiCapNhat)";

                    cmd.Parameters.AddWithValue("@TenSanPham", a.TenSanPham);
                    cmd.Parameters.AddWithValue("@Gia", a.Gia);
                    cmd.Parameters.AddWithValue("@MaDanhMuc", a.MaDanhMuc);
                    cmd.Parameters.AddWithValue("@MoTa", a.MoTa);
                    cmd.Parameters.AddWithValue("@SoLuong", a.SoLuong);
                    cmd.Parameters.AddWithValue("@HinhAnhTruoc", a.HinhAnhTruoc);
                    cmd.Parameters.AddWithValue("@HinhAnhSau", a.HinhAnhSau);
                    cmd.Parameters.AddWithValue("@Sao", a.Sao);
                    cmd.Parameters.AddWithValue("@LanCuoiCapNhat", a.LanCuoiCapNhat);

                    rowEffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return rowEffected;
        }

        public SanPham GetSanPhamById(int id)
        {
            SanPham sp = null;
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();
                cmd.Connection = connection;

                cmd.CommandText = "SELECT * FROM SANPHAM WHERE MaSanPham = @MaSanPham";
                cmd.Parameters.AddWithValue("@MaSanPham", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sp = new SanPham
                        {
                            MaSanPham = Convert.ToInt32(reader["MaSanPham"]),
                            TenSanPham = reader["TenSanPham"].ToString(),
                            Gia = Convert.ToInt32(reader["Gia"]),
                            MaDanhMuc = Convert.ToInt32(reader["MaDanhMuc"]),
                            MoTa = reader["MoTa"].ToString(),
                            SoLuong = Convert.ToInt32(reader["SoLuong"]),
                            HinhAnhTruoc = reader["HinhAnhTruoc"].ToString(),
                            HinhAnhSau = reader["HinhAnhSau"].ToString(),
                            Sao = float.Parse(reader["Sao"].ToString()),
                            LanCuoiCapNhat = Convert.ToDateTime(reader["LanCuoiCapNhat"])
                        };
                    }
                }
            }
            return sp;
        }

        public int UpdateSanPham(SanPham sp)
        {
            int rowEffected = 0;
            try
            {
                using (SqlConnection connection = GetConnection())
                using (SqlCommand cmd = new SqlCommand())
                {
                    connection.Open();
                    string sql = "UPDATE SanPham SET TenSanPham = @TenSanPham, Gia = @Gia, MaDanhMuc = @MaDanhMuc, " +
                        "MoTa = @MoTa, SoLuong = @SoLuong, HinhAnhTruoc = @HinhAnhTruoc, HinhAnhSau = @HinhAnhSau " +
                        "WHERE MaSanPham = @MaSanPham";

                    cmd.CommandText = sql;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@TenSanPham", sp.TenSanPham);
                    cmd.Parameters.AddWithValue("@Gia", sp.Gia);
                    cmd.Parameters.AddWithValue("@MaDanhMuc", sp.MaDanhMuc);
                    cmd.Parameters.AddWithValue("@MoTa", sp.MoTa);
                    cmd.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                    cmd.Parameters.AddWithValue("@HinhAnhTruoc", sp.HinhAnhTruoc);
                    cmd.Parameters.AddWithValue("@HinhAnhSau", sp.HinhAnhSau);
                    cmd.Parameters.AddWithValue("@MaSanPham", sp.MaSanPham);


                    rowEffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            return rowEffected;
        }

        public int DeleteSanPhamById(string id)
        {
            int rowEffected = 0;
            try
            {
                using (SqlConnection connection = GetConnection())
                using (SqlCommand cmd = new SqlCommand())
                {
                    connection.Open();
                    string sql = "DELETE FROM SanPham WHERE MaSanPham = @MaSanPham";
                    cmd.CommandText = sql;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@MaSanPham", id);
                    rowEffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            return rowEffected;
        }

        public List<SanPham> GetSanPhamByName(string name)
        {
            List<SanPham> sanPhams = new List<SanPham>();
            try
            {
                using (SqlConnection connection = GetConnection())
                using (SqlCommand cmd = new SqlCommand())
                {
                    connection.Open();
                    string sql = "SELECT * FROM SanPham WHERE TenSanPham LIKE @TenSanPham";
                    cmd.CommandText = sql;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@TenSanPham", "%" + name + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SanPham sp = new SanPham();
                            sp.MaSanPham = Convert.ToInt32(reader["MaSanPham"]);
                            sp.TenSanPham = reader["TenSanPham"].ToString();
                            sp.Gia = Convert.ToInt32(reader["Gia"]);
                            sp.MaDanhMuc = Convert.ToInt32(reader["MaDanhMuc"]);
                            sp.MoTa = reader["MoTa"].ToString();
                            sp.SoLuong = Convert.ToInt32(reader["SoLuong"]);
                            sp.HinhAnhTruoc = reader["HinhAnhTruoc"].ToString();
                            sp.HinhAnhSau = reader["HinhAnhSau"].ToString();
                            sp.Sao = float.Parse(reader["Sao"].ToString());
                            sp.LanCuoiCapNhat = DateTime.Parse(reader["LanCuoiCapNhat"].ToString());
                            sanPhams.Add(sp);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return sanPhams;
        }

        public int AddChiTietDonHang(ChiTietDonHang dh)
        {
            int rowEffected = 0;
            try
            {
                using (SqlConnection connection = GetConnection())
                using (SqlCommand cmd = new SqlCommand())
                {
                    connection.Open();
                    cmd.CommandText = "InsertChiTietDonHang";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@MaDonHang", dh.MaDonHang);
                    cmd.Parameters.AddWithValue("@MaSanPham", dh.MaSanPham);
                    cmd.Parameters.AddWithValue("@SoLuong", dh.SoLuong);
                    cmd.Parameters.AddWithValue("@DonGia", dh.DonGia);
                    rowEffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            return 0;
        }
        public int GetSoLuongDonDaMua(string maNguoiMua)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM DonHang WHERE MaNguoiMua = @maNguoiMua";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@maNguoiMua", maNguoiMua);
                cmd.Connection = connection;
                int soLuongDonHang = (int)cmd.ExecuteScalar();
                return soLuongDonHang;
            }
        }

        public int GetTongGiaTriDaMua(string maNguoiMua)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();
                string sql = "SELECT SUM(ThanhTien) FROM DonHang WHERE MaNguoiMua = @maNguoiMua";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@maNguoiMua", maNguoiMua);
                cmd.Connection = connection;
                var result = cmd.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                return 0;
            }
        }

        

        public int GetTongGiaTriDaBanCuaSanPham(int maSanPham)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();

                string sqlTongGiaTri = "SELECT SUM(SoLuong * DonGia) FROM ChiTietDonHang WHERE MaSanPham = @maSanPham";
                cmd.CommandText = sqlTongGiaTri;
                cmd.Parameters.AddWithValue("@maSanPham", maSanPham);
                cmd.Connection = connection;

                var tongGiaTriResult = cmd.ExecuteScalar();

                return (tongGiaTriResult != DBNull.Value && tongGiaTriResult != null) ? Convert.ToInt32(tongGiaTriResult) : 0;
            }
        }

        public int GetSoLuongSanPhamDaBan(int maSanPham)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();

                string sqlSoLuong = "SELECT SUM(SoLuong) FROM ChiTietDonHang WHERE MaSanPham = @maSanPham";
                cmd.CommandText = sqlSoLuong;
                cmd.Parameters.AddWithValue("@maSanPham", maSanPham);
                cmd.Connection = connection;

                var soLuongResult = cmd.ExecuteScalar();

                return (soLuongResult != DBNull.Value && soLuongResult != null) ? Convert.ToInt32(soLuongResult) : 0;
            }
        }
        public string GetTenSanPhamById(int id)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "SELECT TenSanPham FROM SanPham WHERE MaSanPham = @maSanPham";
                cmd.Parameters.AddWithValue("@maSanPham", id);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["TenSanPham"].ToString();
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

        public DonHang GetDonHangById(int id)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM DonHang WHERE MaNguoiMua = @MaNguoiMua ";
                cmd.Parameters.AddWithValue("@MaNguoiMua", id);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DonHang donHang = new DonHang
                        {
                            MaDonHang = (int)reader["MaDonHang"],
                            MaNguoiMua = (int)reader["MaNguoiMua"],
                            SoDienThoai = reader["SoDienThoai"].ToString(),
                            DiaChiNhanHang = reader["DiaChi"].ToString(),
                            QuanHuyen = reader["QuanHuyen"].ToString(),
                            TinhThanh = reader["TinhThanh"].ToString(),
                            GhiChu = reader["GhiChu"].ToString(),
                            ThanhTien = (int)reader["ThanhTien"],
                            ThoiGianDatHang = (DateTime)reader["ThoiGianDatHang"]
                        };

                        return donHang;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public List<DonHang> GetDonHangsByUserId(int userId)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM DonHang WHERE MaNguoiMua = @MaNguoiMua ";
                cmd.Parameters.AddWithValue("@MaNguoiMua", userId);


                List<DonHang> danhSachDonHang = new List<DonHang>();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DonHang donHang = new DonHang
                        {
                            MaDonHang = (int)reader["MaDonHang"],
                            MaNguoiMua = (int)reader["MaNguoiMua"],
                            SoDienThoai = reader["SoDienThoai"].ToString(),
                            DiaChiNhanHang = reader["DiaChi"].ToString(),
                            QuanHuyen = reader["QuanHuyen"].ToString(),
                            TinhThanh = reader["TinhThanh"].ToString(),
                            GhiChu = reader["GhiChu"].ToString(),
                            ThanhTien = (int)reader["ThanhTien"],
                            ThoiGianDatHang = (DateTime)reader["ThoiGianDatHang"]
                        };

                        danhSachDonHang.Add(donHang);
                    }
                }

                return danhSachDonHang;
            }
        }

        public List<SanPham> getType(int id)
        {
            List<SanPham> list = new List<SanPham>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM SanPham WHERE MaDanhMuc = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SanPham pd = new SanPham();
                            pd.MaSanPham = int.Parse(reader["MaSanPham"].ToString());
                            pd.TenSanPham = reader["TenSanPham"].ToString();
                            pd.Gia = int.Parse(reader["Gia"].ToString());
                            pd.MaDanhMuc = int.Parse(reader["MaDanhMuc"].ToString());
                            pd.MoTa = reader["MoTa"].ToString();
                            pd.Sao = float.Parse(reader["Sao"].ToString());
                            pd.SoLuong = int.Parse(reader["SoLuong"].ToString());
                            pd.HinhAnhTruoc = reader["HinhAnhTruoc"].ToString();
                            pd.HinhAnhSau = reader["HinhAnhSau"].ToString();
                            pd.LanCuoiCapNhat = DateTime.Parse(reader["LanCuoiCapNhat"].ToString());
                            list.Add(pd);
                        }
                    }
                }
            }

            return list;
        }
        public DanhMuc getDanhMucById(int id)
        {
            DanhMuc dm = new DanhMuc();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                using (SqlCommand categoryCmd = new SqlCommand("SELECT TenDanhMuc, MoTa FROM DanhMuc WHERE MaDanhMuc = @id", connection))
                {
                    categoryCmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader categoryReader = categoryCmd.ExecuteReader())
                    {
                        if (categoryReader.Read())
                        {
                            dm.MaDanhMuc = id;
                            dm.TenDanhMuc = categoryReader["TenDanhMuc"].ToString();
                            dm.MoTa = categoryReader["MoTa"].ToString();
                        }
                    }
                }
            }

            return dm;
        }
        public SanPhamWithDanhGia GetSanPhamWithRateById(int id)
        {
            SanPhamWithDanhGia sp = null;

            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = @"
            SELECT SANPHAM.*, DANHMUC.TenDanhMuc
            FROM SANPHAM
            INNER JOIN DANHMUC ON SANPHAM.MaDanhMuc = DANHMUC.MaDanhMuc
            WHERE SANPHAM.MaSanPham = @MaSanPham";
                cmd.Parameters.AddWithValue("@MaSanPham", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sp = new SanPhamWithDanhGia
                        {
                            MaSanPham = Convert.ToInt32(reader["MaSanPham"]),
                            TenSanPham = reader["TenSanPham"].ToString(),
                            Gia = Convert.ToInt32(reader["Gia"]),
                            MaDanhMuc = Convert.ToInt32(reader["MaDanhMuc"]),
                            TenDanhMuc = reader["TenDanhMuc"].ToString(),
                            MoTa = reader["MoTa"].ToString(),
                            SoLuong = Convert.ToInt32(reader["SoLuong"]),
                            HinhAnhTruoc = reader["HinhAnhTruoc"].ToString(),
                            HinhAnhSau = reader["HinhAnhSau"].ToString(),
                            Sao = float.Parse(reader["Sao"].ToString()),
                            LanCuoiCapNhat = Convert.ToDateTime(reader["LanCuoiCapNhat"]),
                            DanhGiaList = new List<DanhGia>()
                        };
                    }
                }
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT * FROM DanhGia WHERE MaSanPham = @MaSanPham";
                cmd.Parameters.AddWithValue("@MaSanPham", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DanhGia danhGia = new DanhGia
                        {
                            MaPhanHoi = Convert.ToInt32(reader["MaPhanHoi"]),
                            MaNguoiMua = Convert.ToInt32(reader["MaNguoiMua"]),
                            MaSanPham = Convert.ToInt32(reader["MaSanPham"]),
                            NoiDung = reader["NoiDung"].ToString(),
                            Sao = Convert.ToInt32(reader["Sao"])
                        };

                        sp.DanhGiaList.Add(danhGia);
                    }
                }
            }

            return sp;
        }
        public List<DanhGia> GetDanhGiaBySanPhamId(int sanPhamId)
        {
            List<DanhGia> danhGiaList = new List<DanhGia>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT nm.HoTen,dg.* FROM DanhGia dg,NguoiMua nm WHERE MaSanPham = @SanPhamId AND dg.MaNguoiMua = nm.MaNguoiMua";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SanPhamId", sanPhamId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhGiaList.Add(new DanhGia
                            {
                                HoTen = reader.GetString(0),
                                MaPhanHoi = reader.GetInt32(1),
                                MaNguoiMua = reader.GetInt32(2),
                                MaSanPham = reader.GetInt32(3),
                                NoiDung = reader.GetString(4),
                                Sao = reader.GetInt32(5)
                            });
                        }
                    }
                }
            }

            return danhGiaList;
        }
        
        public List<DanhGia> GetDanhGias()
        {
            List<DanhGia> danhGias = new List<DanhGia>();

            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM DanhGia";

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DanhGia danhGia = new DanhGia
                        {
                            MaPhanHoi = (int)reader["MaPhanHoi"],
                            MaNguoiMua = (int)reader["MaNguoiMua"],
                            MaSanPham = (int)reader["MaSanPham"],
                            NoiDung = reader["NoiDung"].ToString(),
                            Sao = (int)reader["Sao"]
                        };
                        danhGias.Add(danhGia);
                    }
                }
            }
            return danhGias;
        }
        public int AddDanhGia(int maNM, int maSP, string ND, int sao)
        {
            int result = 0;
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "INSERT INTO DanhGia (MaNguoiMua, MaSanPham, NoiDung, Sao) VALUES (@maNM, @maSP, @ND, @sao)";
                cmd.Parameters.AddWithValue("@maNM", maNM);
                cmd.Parameters.AddWithValue("@maSP", maSP);
                cmd.Parameters.AddWithValue("@ND", ND);
                cmd.Parameters.AddWithValue("@sao", sao);
                connection.Open();
                result = cmd.ExecuteNonQuery();
            }
            return result;
        }

        public void DeleteDanhGia(int id)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "DELETE FROM DanhGia WHERE MaPhanHoi = @ma";
                cmd.Parameters.AddWithValue("@ma", id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public bool HasUserReviewedProduct(int maNguoiMua, int maSanPham)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM DanhGia WHERE MaNguoiMua = @MaNguoiMua AND MaSanPham = @MaSanPham";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNguoiMua", maNguoiMua);
                    command.Parameters.AddWithValue("@MaSanPham", maSanPham);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }


    }
}