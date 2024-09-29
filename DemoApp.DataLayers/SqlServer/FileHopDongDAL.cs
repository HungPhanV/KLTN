using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DemoApp.Entities;

namespace DemoApp.DataLayers.SqlServer
{
    public class FileHopDongDAL : _BaseDAL, IFileHopDongDAL
    {
        public FileHopDongDAL(string connectionString)
            : base(connectionString)
        {
        }


        public int Delete(FileHopDong fileHopDong)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.FileHopDong_Delete, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaFile", fileHopDong.MaFile));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }
        public IList<FileHopDong> GetByMaHD(int maHD)
        {
            List<FileHopDong> data = new List<FileHopDong>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.FileHopDong_GetByMaHD, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaHD", maHD));

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            FileHopDong fileHopDong = new FileHopDong();

                            fileHopDong.MaFile = dbReader["MaFile"] != DBNull.Value ? Convert.ToInt32(dbReader["MaFile"]) : 0;
                            fileHopDong.TenFile = dbReader["TenFile"].ToString();
                            fileHopDong.LuuTru = dbReader["LuuTru"].ToString();
                            fileHopDong.GhiChu = dbReader["GhiChu"].ToString();
                            fileHopDong.MaHD = dbReader["MaHD"] != DBNull.Value ? Convert.ToInt32(dbReader["MaHD"]) : 0;

                            data.Add(fileHopDong);
                        }
                    }
                }
            }

            return data;
        }

        public int Insert(FileHopDong fileHopDong)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.FileHopDong_Insert, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@TenFile", fileHopDong.TenFile));
                    cmd.Parameters.Add(CreateParameter("@LuuTru", fileHopDong.LuuTru));
                    cmd.Parameters.Add(CreateParameter("@GhiChu", fileHopDong.GhiChu));
                    cmd.Parameters.Add(CreateParameter("@MaHD", fileHopDong.MaHD));

                 
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected; // Trả về số hàng bị ảnh hưởng
                }
            }
        }
        public FileHopDong GetByMaFile(int maFile)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.FileHopDong_GetFileById, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaFile", maFile));

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            FileHopDong fileHopDong = new FileHopDong();

                            fileHopDong.MaFile = dbReader["MaFile"] != DBNull.Value ? Convert.ToInt32(dbReader["MaFile"]) : 0;
                            fileHopDong.TenFile = dbReader["TenFile"].ToString();
                            fileHopDong.LuuTru = dbReader["LuuTru"].ToString();
                            fileHopDong.GhiChu = dbReader["GhiChu"].ToString();
                            fileHopDong.MaHD = dbReader["MaHD"] != DBNull.Value ? Convert.ToInt32(dbReader["MaHD"]) : 0;

                            return fileHopDong;
                        }
                    }
                }
            }

            return null; // Trả về null nếu không tìm thấy file hợp đồng với MaFile tương ứng
        }

    }
}
