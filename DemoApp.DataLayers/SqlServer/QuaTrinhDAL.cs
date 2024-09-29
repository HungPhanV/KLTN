using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DemoApp.Entities;

namespace DemoApp.DataLayers.SqlServer
{
    public class QuaTrinhDAL : _BaseDAL, IQuaTrinhDAL
    {
        public QuaTrinhDAL(string connectionString)
            : base(connectionString)
        {
        }

        public int Insert(QuaTrinh quaTrinh)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.QuaTrinh_Create, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@ThoiGianBatDau", quaTrinh.ThoiGianBatDau));
                    cmd.Parameters.Add(CreateParameter("@ThoiGianKetThuc", quaTrinh.ThoiGianKetThuc));
                    cmd.Parameters.Add(CreateParameter("@TrangThai", quaTrinh.TrangThai));
                    cmd.Parameters.Add(CreateParameter("@MaPB", quaTrinh.MaPB));
                    cmd.Parameters.Add(CreateParameter("@MaNV", quaTrinh.MaNV));
                    cmd.Parameters.Add(CreateParameter("@MAVT", quaTrinh.MaVT));
                    cmd.Parameters.Add(CreateParameter("@MACV", quaTrinh.MaCV));
                    int rowsAffected = cmd.ExecuteNonQuery();

                    cn.Close();

                    return rowsAffected; // Trả về số hàng bị ảnh hưởng
                }
            }
            return result;
        }

        public int Update(QuaTrinh quaTrinh)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.QuaTrinh_Update, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@ID", quaTrinh.ID));
                    cmd.Parameters.Add(CreateParameter("@ThoiGianBatDau", quaTrinh.ThoiGianBatDau));
                    cmd.Parameters.Add(CreateParameter("@ThoiGianKetThuc", quaTrinh.ThoiGianKetThuc));
                    cmd.Parameters.Add(CreateParameter("@TrangThai", quaTrinh.TrangThai));
                    cmd.Parameters.Add(CreateParameter("@MaPB", quaTrinh.MaPB));
                    cmd.Parameters.Add(CreateParameter("@MaNV", quaTrinh.MaNV));
                    cmd.Parameters.Add(CreateParameter("@MaVT", quaTrinh.MaVT));
                    cmd.Parameters.Add(CreateParameter("@MaCV", quaTrinh.MaCV));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }

        public int Delete(QuaTrinh quaTrinh)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.QuaTrinh_Delete, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@ID", quaTrinh.ID));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }

        public IList<QuaTrinh> GetByMaNV(int maNV)
        {
            List<QuaTrinh> data = new List<QuaTrinh>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.QuaTrinh_GetByMaNV, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaNV", maNV));
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            QuaTrinh quaTrinh = new QuaTrinh();

                            quaTrinh.ID = dbReader["ID"] != DBNull.Value ? Convert.ToInt32(dbReader["ID"]) : 0;

                            if (dbReader["ThoiGianBatDau"] != DBNull.Value)
                            {
                                quaTrinh.ThoiGianBatDau = Convert.ToDateTime(dbReader["ThoiGianBatDau"]);
                            }

                            if (dbReader["ThoiGianKetThuc"] != DBNull.Value)
                            {
                                quaTrinh.ThoiGianKetThuc = Convert.ToDateTime(dbReader["ThoiGianKetThuc"]);
                            }

                            quaTrinh.TrangThai = dbReader["TrangThai"].ToString();
                            quaTrinh.MaPB = dbReader["MaPB"] != DBNull.Value ? Convert.ToInt32(dbReader["MaPB"]) : 0;
                            quaTrinh.MaNV = dbReader["MaNV"] != DBNull.Value ? Convert.ToInt32(dbReader["MaNV"]) : 0;
                            quaTrinh.MaVT = dbReader["MaVT"] != DBNull.Value ? Convert.ToInt32(dbReader["MaVT"]) : 0;
                            quaTrinh.MaCV = dbReader["MaCV"] != DBNull.Value ? Convert.ToInt32(dbReader["MaCV"]) : 0;

                            data.Add(quaTrinh);
                        }
                    }
                }
            }

            return data;
        }

    }
}
