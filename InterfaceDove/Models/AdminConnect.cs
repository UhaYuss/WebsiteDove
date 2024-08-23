using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InterfaceDove.Models
{
    public class AdminConnect : DbConnectionBase
    {
		public bool IsAdmin(string username, string password)
		{
			using (SqlConnection connection = GetConnection())
			using (SqlCommand cmd = new SqlCommand())
			{
				connection.Open();
				string sql = "SELECT MatKhau FROM Admin WHERE TenDangNhap = @username";
				cmd.CommandText = sql;
				cmd.Parameters.AddWithValue("@username", username);
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

		public int GetSoLuongDonHang()
		{
			using (SqlConnection connection = GetConnection())
			using (SqlCommand cmd = new SqlCommand())
			{
				connection.Open();
				string sql = "SELECT COUNT(*) FROM DonHang";
				cmd.CommandText = sql;
				cmd.Connection = connection;
				int soLuongDon = (int)cmd.ExecuteScalar();
				return soLuongDon;
			}
		}

		public int GetTongGiaTriDaBan()
		{
			using (SqlConnection connection = GetConnection())
			using (SqlCommand cmd = new SqlCommand())
			{
				connection.Open();
				string sql = "SELECT SUM(ThanhTien) FROM DonHang";
				cmd.CommandText = sql;
				cmd.Connection = connection;
				object result = cmd.ExecuteScalar();

				if (result != DBNull.Value)
				{
					return (int)result;
				}
				return 0;
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

		public NguoiMua GetNguoiMua(string maNguoiMua)
		{
			NguoiMua nm = new NguoiMua();
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
						nm.MaNguoiMua = reader.GetInt32(0);
						nm.SoDienThoai = reader.GetString(1);
						nm.MatKhau = reader.GetString(2);
						nm.HoTen = reader.GetString(3);
						nm.Email = reader.GetString(4);
						nm.DiaChi = reader.GetString(5);
						nm.TinhThanh = reader.GetString(6);
						nm.QuanHuyen = reader.GetString(7);
						nm.NgayTao = reader.GetDateTime(8);
						return nm;
					}
				}
			}
			return null;
		}

		public List<DonHang> GetDonHangs()
		{
			List<DonHang> danhSachDonHang = new List<DonHang>();

			using (SqlConnection connection = GetConnection())
			{
				connection.Open();

				string sql = "SELECT * FROM DonHang";

				using (SqlCommand cmd = new SqlCommand(sql, connection))
				{
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							DonHang donHang = new DonHang
							{
								MaDonHang = reader.GetInt32(0),
								MaNguoiMua = reader.GetInt32(1),
								SoDienThoai = reader.GetString(2),
								DiaChiNhanHang = reader.GetString(3),
								TinhThanh = reader.GetString(4),
								QuanHuyen = reader.GetString(5),
								GhiChu = reader.GetString(6),
								ThanhTien = reader.GetInt32(7),
								ThoiGianDatHang = reader.GetDateTime(8)
							};

							danhSachDonHang.Add(donHang);
						}
					}
				}
			}

			return danhSachDonHang;
		}


		public List<DonHang> GetDonHangs(string maNguoiMua)
		{
			List<DonHang> l = new List<DonHang>();
			using (SqlConnection connection = GetConnection())
			using (SqlCommand cmd = new SqlCommand())
			{
				connection.Open();
				string sql = "SELECT * FROM DonHang WHERE MaNguoiMua = @maNguoiMua";
				cmd.Parameters.AddWithValue("@maNguoiMua", maNguoiMua);
				cmd.CommandText = sql;
				cmd.Connection = connection;
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						DonHang dh = new DonHang
						{
							MaDonHang = reader.GetInt32(0),
							MaNguoiMua = reader.GetInt32(1),
							SoDienThoai = reader.GetString(2),
							DiaChiNhanHang = reader.GetString(3),
							TinhThanh = reader.GetString(4),
							QuanHuyen = reader.GetString(5),
							GhiChu = reader.GetString(6),
							ThanhTien = reader.GetInt32(7),
							ThoiGianDatHang = reader.GetDateTime(8)
						};
						l.Add(dh);
					}
				}
			}
			return l;
		}

		public List<DonHang> GetDanhSachDonDaBanCuaSanPham(int maSanPham)
		{
			List<DonHang> danhSachDonHang = new List<DonHang>();

			using (SqlConnection connection = GetConnection())
			using (SqlCommand cmd = new SqlCommand())
			{
				connection.Open();
				string sql = "SELECT * FROM DonHang WHERE MaDonHang IN (SELECT DISTINCT MaDonHang FROM ChiTietDonHang WHERE MaSanPham = @maSanPham)";
				cmd.CommandText = sql;
				cmd.Parameters.AddWithValue("@maSanPham", maSanPham);
				cmd.Connection = connection;
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						DonHang donHang = new DonHang
						{
							MaDonHang = reader.GetInt32(0),
							MaNguoiMua = reader.GetInt32(1),
							SoDienThoai = reader.GetString(2),
							DiaChiNhanHang = reader.GetString(3),
							TinhThanh = reader.GetString(4),
							QuanHuyen = reader.GetString(5),
							GhiChu = reader.GetString(6),
							ThanhTien = reader.GetInt32(7),
							ThoiGianDatHang = reader.GetDateTime(8)
						};
						danhSachDonHang.Add(donHang);
					}
				}
			}
			return danhSachDonHang;
		}
	}
}
