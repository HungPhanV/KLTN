using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DemoApp.Entities;

namespace DemoApp.DataLayers.SqlServer
{
    public class ChamCongDAL : _BaseDAL, IChamCongDAL
    {
        public ChamCongDAL(string connectionString)
            : base(connectionString)
        {
        }
        public bool KiemTraDaChamCong(int maCaLam)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.ChamCong_KiemTraDaChamCong, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaCaLam", maCaLam));

                    object result = cmd.ExecuteScalar();
                    cn.Close();

                    return result != null && result != DBNull.Value && Convert.ToInt32(result) > 0;
                }
            }
        }
        public int UpdateThoiGianKetThucChamCong(int maCaLam, TimeSpan thoiGianKetThuc)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.ChamCong_UpdateThoiGianKetThucChamCong, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaCaLam", maCaLam));
                    cmd.Parameters.Add(CreateParameter("@ThoiGianKetThuc", thoiGianKetThuc));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();

                    return rowsAffected;
                }
            }
        }

        public IList<Entities.ChamCong> List()
        {
            List<ChamCong> data = new List<ChamCong>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.ChamCong_Select, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            ChamCong chamCong = new ChamCong();

                            chamCong.MaChamCong = dbReader["MaChamCong"] != DBNull.Value ? Convert.ToInt32(dbReader["MaChamCong"]) : 0;
                            chamCong.MaCaLam = dbReader["MaCaLam"] != DBNull.Value ? Convert.ToInt32(dbReader["MaCaLam"]) : 0;
                            chamCong.ThoiGianBatDau = dbReader["ThoiGianBatDau"] != DBNull.Value ? (TimeSpan)dbReader["ThoiGianBatDau"] : TimeSpan.MinValue;
                            chamCong.ThoiGianKetThuc = dbReader["ThoiGianKetThuc"] != DBNull.Value ? (TimeSpan)dbReader["ThoiGianKetThuc"] : TimeSpan.MinValue;
                            chamCong.HoTen = dbReader["HoTen"] != DBNull.Value ? dbReader["HoTen"].ToString() : string.Empty; // Thêm thông tin HoTen
                            chamCong.NgayBatDau = dbReader["NgayBatDau"] != DBNull.Value ? Convert.ToDateTime(dbReader["NgayBatDau"]) : DateTime.MinValue;
                            chamCong.NgayKetThuc = dbReader["NgayKetThuc"] != DBNull.Value ? Convert.ToDateTime(dbReader["NgayKetThuc"]) : DateTime.MinValue;
                            data.Add(chamCong);
                        }
                        dbReader.Close();
                    }
                }
                cn.Close();
            }
            return data;
        }

        public IList<Entities.ChamCong> ListByMaNV(int maNV)
        {
            List<ChamCong> data = new List<ChamCong>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.ChamCong_SelectByMaNV, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaNV", maNV));

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            ChamCong chamCong = new ChamCong();

                            chamCong.MaChamCong = dbReader["MaChamCong"] != DBNull.Value ? Convert.ToInt32(dbReader["MaChamCong"]) : 0;
                            chamCong.MaCaLam = dbReader["MaCaLam"] != DBNull.Value ? Convert.ToInt32(dbReader["MaCaLam"]) : 0;
                            chamCong.ThoiGianBatDau = dbReader["ThoiGianBatDau"] != DBNull.Value ? (TimeSpan)dbReader["ThoiGianBatDau"] : TimeSpan.MinValue;
                            chamCong.ThoiGianKetThuc = dbReader["ThoiGianKetThuc"] != DBNull.Value ? (TimeSpan)dbReader["ThoiGianKetThuc"] : TimeSpan.MinValue;
                            chamCong.HoTen = dbReader["HoTen"] != DBNull.Value ? dbReader["HoTen"].ToString() : string.Empty; // Thêm thông tin HoTen
                            chamCong.NgayBatDau = dbReader["NgayBatDau"] != DBNull.Value ? Convert.ToDateTime(dbReader["NgayBatDau"]) : DateTime.MinValue;
                            chamCong.NgayKetThuc = dbReader["NgayKetThuc"] != DBNull.Value ? Convert.ToDateTime(dbReader["NgayKetThuc"]) : DateTime.MinValue;
                            data.Add(chamCong);

                        }
                        dbReader.Close();
                    }
                }
                cn.Close();
            }
            return data;
        }
        public int Insert(ChamCong chamCong)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.ChamCong_Insert, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaCaLam", chamCong.MaCaLam));
                    cmd.Parameters.Add(CreateParameter("@ThoiGianBatDau", chamCong.ThoiGianBatDau));
                    cmd.Parameters.Add(CreateParameter("@ThoiGianKetThuc", chamCong.ThoiGianKetThuc));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();

                    return rowsAffected;
                }
            }
            return result;
        }
        public int Update(ChamCong chamCong)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.ChamCong_Update, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaChamCong", chamCong.MaChamCong));
                    cmd.Parameters.Add(CreateParameter("@MaCaLam", chamCong.MaCaLam));
                    cmd.Parameters.Add(CreateParameter("@ThoiGianBatDau", chamCong.ThoiGianBatDau));
                    cmd.Parameters.Add(CreateParameter("@ThoiGianKetThuc", chamCong.ThoiGianKetThuc));



                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }

        public int Delete(int maChamCong)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.ChamCong_Delete, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaChamCong", maChamCong));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }
        public IList<Entities.ChamCong> TongHopChamCong(int maNV)
        {
            List<ChamCong> data = new List<ChamCong>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.ChamCong_TongHopChamCong, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số MaNV vào stored procedure
                    cmd.Parameters.AddWithValue("@MaNV", maNV);

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            ChamCong chamCong = new ChamCong();

                          
                            chamCong.TenCa = dbReader["TenCa"] != DBNull.Value ? dbReader["TenCa"].ToString() : string.Empty;
                            chamCong.NgayBatDau = dbReader["Ngày làm"] != DBNull.Value ? Convert.ToDateTime(dbReader["Ngày làm"]) : DateTime.MinValue;
                            chamCong.SoGioLam = dbReader["Số giờ làm"] != DBNull.Value ? Convert.ToInt32(dbReader["Số giờ làm"]) : 0;
                            chamCong.ThoiGianBatDau = TimeSpan.Parse(dbReader["Giờ vào"].ToString());
                            chamCong.ThoiGianKetThuc = TimeSpan.Parse(dbReader["Giờ ra"].ToString());
                            data.Add(chamCong);
                        }
                        dbReader.Close();
                    }
                }
                cn.Close();
            }
            return data;
        }

        public IList<Entities.ChamCong> TongHopChamCongTheoThang(int thang, int nam)
        {
            List<ChamCong> data = new List<ChamCong>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.ChamCong_TongHopChamCongTheoThang, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số tháng và năm vào stored procedure
                    cmd.Parameters.AddWithValue("@Thang", thang);
                    cmd.Parameters.AddWithValue("@Nam", nam);

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            ChamCong chamCong = new ChamCong();

                            chamCong.MaCaLam = dbReader["Mã Ca làm"] != DBNull.Value ? Convert.ToInt32(dbReader["Mã Ca làm"]) : 0;
                            chamCong.HoTen = dbReader["HoTen"] != DBNull.Value ? dbReader["HoTen"].ToString() : string.Empty;
                            chamCong.TenCa = dbReader["TenCa"] != DBNull.Value ? dbReader["TenCa"].ToString() : string.Empty;
                            chamCong.NgayBatDau = dbReader["Ngày làm"] != DBNull.Value ? Convert.ToDateTime(dbReader["Ngày làm"]) : DateTime.MinValue;
                            chamCong.SoGioLam = dbReader["Số giờ làm"] != DBNull.Value ? Convert.ToInt32(dbReader["Số giờ làm"]) : 0;
                            data.Add(chamCong);
                        }
                        dbReader.Close();
                    }
                }
                cn.Close();
            }
            return data;
        }

    }
}
