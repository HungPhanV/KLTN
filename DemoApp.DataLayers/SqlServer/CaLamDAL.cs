using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DemoApp.Entities;

namespace DemoApp.DataLayers.SqlServer
{
    public class CaLamDAL : _BaseDAL, ICaLamDAL
    {
        public CaLamDAL(string connectionString)
            : base(connectionString)
        {
        }
        public int LayMaCaLam(int maNV, DateTime ngay)
        {
            int maCaLam = -1; // Giá trị mặc định nếu không tìm thấy mã ca làm

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT MaCaLam FROM CaLam WHERE MaNV = @MaNV AND @Ngay BETWEEN NgayBatDau AND NgayKetThuc";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNV", maNV);
                        command.Parameters.AddWithValue("@Ngay", ngay);
                        connection.Open();

                        object result = command.ExecuteScalar();

                        // Kiểm tra nếu result không null thì gán giá trị MaCaLam
                        if (result != DBNull.Value && result != null)
                        {
                            maCaLam = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý exception nếu có
                Console.WriteLine("Lỗi: " + ex.Message);
            }

            return maCaLam;
        }



        public bool KiemTraCaLam(int maNV, DateTime ngay)
        {
            bool coCaLam = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM CaLam WHERE MaNV = @MaNV AND @Ngay BETWEEN NgayBatDau AND NgayKetThuc";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNV", maNV);
                        command.Parameters.AddWithValue("@Ngay", ngay);
                        connection.Open();

                        int count = (int)command.ExecuteScalar();

                        coCaLam = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý exception nếu có
                Console.WriteLine("Lỗi: " + ex.Message);
            }

            return coCaLam;
        }
        public IList<Entities.CaLam> List()
        {
            List<CaLam> data = new List<CaLam>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.CaLam_Select, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            CaLam caLam = new CaLam();

                            caLam.MaCaLam = dbReader["MaCaLam"] != DBNull.Value ? Convert.ToInt32(dbReader["MaCaLam"]) : 0;
                            caLam.MaNV = dbReader["MaNV"] != DBNull.Value ? Convert.ToInt32(dbReader["MaNV"]) : 0;
                            caLam.NgayBatDau = dbReader["NgayBatDau"] != DBNull.Value ? Convert.ToDateTime(dbReader["NgayBatDau"]) : DateTime.MinValue;
                            caLam.NgayKetThuc = dbReader["NgayKetThuc"] != DBNull.Value ? Convert.ToDateTime(dbReader["NgayKetThuc"]) : DateTime.MinValue;
                            caLam.MaLoai = dbReader["MaLoai"] != DBNull.Value ? Convert.ToInt32(dbReader["MaLoai"]) : 0;
                            caLam.HoTen = dbReader["HoTen"] != DBNull.Value ? dbReader["HoTen"].ToString() : string.Empty; // Thêm thông tin HoTen
                            caLam.TenCa = dbReader["TenCa"] != DBNull.Value ? dbReader["TenCa"].ToString() : string.Empty; // Thêm thông tin TenCa
                            caLam.ThoiGianBatDau = dbReader["ThoiGianBatDau"] != DBNull.Value ? (TimeSpan)dbReader["ThoiGianBatDau"] : TimeSpan.MinValue; // Bổ sung thông tin ThoiGianBatDau
                            caLam.ThoiGianKetThuc = dbReader["ThoiGianKetThuc"] != DBNull.Value ? (TimeSpan)dbReader["ThoiGianKetThuc"] : TimeSpan.MinValue; // Bổ sung thông tin ThoiGianKetThuc
                            data.Add(caLam);
                        }
                        dbReader.Close();
                    }
                }
                cn.Close();
            }
            return data;
        }
        public IList<Entities.CaLam> ListByMaNV(int maNV)
        {
            List<CaLam> data = new List<CaLam>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.CaLam_SelectByMaNV, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaNV", maNV));

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            CaLam caLam = new CaLam();

                            caLam.MaCaLam = dbReader["MaCaLam"] != DBNull.Value ? Convert.ToInt32(dbReader["MaCaLam"]) : 0;
                            caLam.MaNV = dbReader["MaNV"] != DBNull.Value ? Convert.ToInt32(dbReader["MaNV"]) : 0;
                            caLam.NgayBatDau = dbReader["NgayBatDau"] != DBNull.Value ? Convert.ToDateTime(dbReader["NgayBatDau"]) : DateTime.MinValue;
                            caLam.NgayKetThuc = dbReader["NgayKetThuc"] != DBNull.Value ? Convert.ToDateTime(dbReader["NgayKetThuc"]) : DateTime.MinValue;
                            caLam.MaLoai = dbReader["MaLoai"] != DBNull.Value ? Convert.ToInt32(dbReader["MaLoai"]) : 0;
                            caLam.HoTen = dbReader["HoTen"] != DBNull.Value ? dbReader["HoTen"].ToString() : string.Empty; // Thêm thông tin HoTen
                            caLam.TenCa = dbReader["TenCa"] != DBNull.Value ? dbReader["TenCa"].ToString() : string.Empty; // Thêm thông tin TenCa
                            caLam.ThoiGianBatDau = dbReader["ThoiGianBatDau"] != DBNull.Value ? (TimeSpan)dbReader["ThoiGianBatDau"] : TimeSpan.MinValue; // Bổ sung thông tin ThoiGianBatDau
                            caLam.ThoiGianKetThuc = dbReader["ThoiGianKetThuc"] != DBNull.Value ? (TimeSpan)dbReader["ThoiGianKetThuc"] : TimeSpan.MinValue; // Bổ sung thông tin ThoiGianKetThuc
                            data.Add(caLam);
                        }
                        dbReader.Close();
                    }
                }
                cn.Close();
            }
            return data;
        }

        public int Insert(CaLam caLam)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.CaLam_Insert, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaNV", caLam.MaNV));
                    cmd.Parameters.Add(CreateParameter("@NgayBatDau", caLam.NgayBatDau));
                    cmd.Parameters.Add(CreateParameter("@NgayKetThuc", caLam.NgayKetThuc));
                    cmd.Parameters.Add(CreateParameter("@MaLoai", caLam.MaLoai));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();

                    return rowsAffected;
                }
            }
            return result;
        }
        public int Update(CaLam caLam)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.CaLam_Update, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaCaLam", caLam.MaCaLam));
                    cmd.Parameters.Add(CreateParameter("@MaNV", caLam.MaNV));
                    cmd.Parameters.Add(CreateParameter("@NgayBatDau", caLam.NgayBatDau));
                    cmd.Parameters.Add(CreateParameter("@NgayKetThuc", caLam.NgayKetThuc));
                    cmd.Parameters.Add(CreateParameter("@MaLoai", caLam.MaLoai));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }

        public int Delete(int maCaLam)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.CaLam_Delete, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaCaLam", maCaLam));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }
    }
}
