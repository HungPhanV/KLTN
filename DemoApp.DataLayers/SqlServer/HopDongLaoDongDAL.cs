using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DemoApp.Entities;

namespace DemoApp.DataLayers.SqlServer
{
    public class HopDongLaoDongDAL : _BaseDAL, IHopDongLaoDongDAL
    {
        public HopDongLaoDongDAL(string connectionString)
            : base(connectionString)
        {
        }
        public HopDongLaoDong GetHopDongByMaHD(int maHD)
        {
            HopDongLaoDong hopDong = null;

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.HopDongLaoDong_GetByMaHD, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaHD", maHD));
                    using (SqlDataReader dbReader = cmd.ExecuteReader())
                    {
                        if (dbReader.Read())
                        {
                            hopDong = new HopDongLaoDong()
                            {
                                MaHD = Convert.ToInt32(dbReader["MaHD"]),
                                MaNV = Convert.ToInt32(dbReader["MaNV"]),
                                MaLoaiHD = Convert.ToInt32(dbReader["MaLoaiHD"]),
                                TuNgay = (dbReader["TuNgay"] != DBNull.Value) ? Convert.ToDateTime(dbReader["TuNgay"]) : DateTime.MinValue,
                                DenNgay = (dbReader["DenNgay"] != DBNull.Value) ? Convert.ToDateTime(dbReader["DenNgay"]) : DateTime.MinValue,
                            };
                        }
                    }
                }
            }

            return hopDong;
        }


        public IList<Entities.HopDongLaoDong> Select()
        {
            List<HopDongLaoDong> data = new List<HopDongLaoDong>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.HopDongLaoDong_Select, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new HopDongLaoDong()
                            {
                                MaHD = Convert.ToInt32(dbReader["MaHD"]),
                                MaNV = Convert.ToInt32(dbReader["MaNV"]),
                                MaLoaiHD = Convert.ToInt32(dbReader["MaLoaiHD"]),
                                TuNgay = Convert.ToDateTime(dbReader["TuNgay"]),
                                DenNgay = Convert.ToDateTime(dbReader["DenNgay"])
                            });
                        }

                        // Đóng SqlDataReader sau khi hoàn thành vòng lặp
                        dbReader.Close();
                    }
                }

                // Đóng SqlConnection sau khi hoàn thành
                cn.Close();
            }

            return data;
        }
        public int Insert(HopDongLaoDong hopDong)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.HopDongLaoDong_Insert, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaNV", hopDong.MaNV));
                    cmd.Parameters.Add(CreateParameter("@MaLoaiHD", hopDong.MaLoaiHD));
                    cmd.Parameters.Add(CreateParameter("@TuNgay", hopDong.TuNgay));
                    cmd.Parameters.Add(CreateParameter("@DenNgay", hopDong.DenNgay));
                    int rowsAffected = cmd.ExecuteNonQuery();

                    cn.Close();

                    return rowsAffected; // Trả về số hàng bị ảnh hưởng
                }
            }
            return result;
        }

        public int Update(HopDongLaoDong hopDong)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.HopDongLaoDong_Update, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaHD", hopDong.MaHD));
                    cmd.Parameters.Add(CreateParameter("@MaNV", hopDong.MaNV));
                    cmd.Parameters.Add(CreateParameter("@MaLoaiHD", hopDong.MaLoaiHD));
                    cmd.Parameters.Add(CreateParameter("@TuNgay", hopDong.TuNgay));
                    cmd.Parameters.Add(CreateParameter("@DenNgay", hopDong.DenNgay));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }

        public int Delete(HopDongLaoDong hopDong)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.HopDongLaoDong_Delete, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaHD", hopDong.MaHD));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }

        public IList<HopDongLaoDong> GetByMaNV(int maNV)
        {
            List<HopDongLaoDong> data = new List<HopDongLaoDong>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.HopDongLaoDong_GetByMaNV, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaNV", maNV));
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            HopDongLaoDong hopDong = new HopDongLaoDong()
                            {
                                MaHD = Convert.ToInt32(dbReader["MaHD"]),
                                MaNV = Convert.ToInt32(dbReader["MaNV"]),
                                MaLoaiHD = Convert.ToInt32(dbReader["MaLoaiHD"])
                            };

                            // Kiểm tra nếu cột 'TuNgay' không có giá trị NULL
                            if (!dbReader.IsDBNull(dbReader.GetOrdinal("TuNgay")))
                            {
                                hopDong.TuNgay = Convert.ToDateTime(dbReader["TuNgay"]);
                            }

                            // Kiểm tra nếu cột 'DenNgay' không có giá trị NULL
                            if (!dbReader.IsDBNull(dbReader.GetOrdinal("DenNgay")))
                            {
                                hopDong.DenNgay = Convert.ToDateTime(dbReader["DenNgay"]);
                            }

                            data.Add(hopDong);
                        }

                        // Đóng SqlDataReader sau khi hoàn thành vòng lặp
                        dbReader.Close();


                        // Đóng SqlDataReader sau khi hoàn thành vòng lặp
                        dbReader.Close();
                    }
                }

                // Đóng SqlConnection sau khi hoàn thành
                cn.Close();
            }

            return data;
        }
        public int GetTotalNumberOfHopDongs()
        {
            int totalRecords = 0;

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM hopdong", cn))
                {
                    totalRecords = (int)cmd.ExecuteScalar();
                }

                cn.Close();
            }

            return totalRecords;
        }
    }
}
