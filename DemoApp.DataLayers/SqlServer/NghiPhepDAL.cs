using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DemoApp.Entities;

namespace DemoApp.DataLayers.SqlServer
{
    public class NghiPhepDAL : _BaseDAL, INghiPhepDAL
    {
        public NghiPhepDAL(string connectionString)
            : base(connectionString)
        {
        }

        public IList<NghiPhep> List()
        {
            List<NghiPhep> data = new List<NghiPhep>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.NghiPhep_List, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            NghiPhep nghiPhep = new NghiPhep();

                            nghiPhep.ID = Convert.ToInt32(dbReader["ID"]);
                            nghiPhep.MaNV = Convert.ToInt32(dbReader["MaNV"]);
                            nghiPhep.NgayBatDau = Convert.ToDateTime(dbReader["NgayBatDau"]);
                            nghiPhep.NgayKetThuc = Convert.ToDateTime(dbReader["NgayKetThuc"]);
                            nghiPhep.MaLoaiNghiPhep = Convert.ToInt32(dbReader["MaLoaiNghiPhep"]);
                            nghiPhep.TrangThai = dbReader["TrangThai"].ToString();
                            nghiPhep.LyDoBoSung = dbReader["LyDoBoSung"].ToString();
                            data.Add(nghiPhep);
                        }
                        dbReader.Close();
                    }
                }
                cn.Close();
            }
            return data;
        }

        public int Insert(NghiPhep nghiPhep)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.NghiPhep_Insert, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaNV", nghiPhep.MaNV));
                    cmd.Parameters.Add(CreateParameter("@NgayBatDau", nghiPhep.NgayBatDau));
                    cmd.Parameters.Add(CreateParameter("@NgayKetThuc", nghiPhep.NgayKetThuc));
                    cmd.Parameters.Add(CreateParameter("@MaLoaiNghiPhep", nghiPhep.MaLoaiNghiPhep));                 
                    cmd.Parameters.Add(CreateParameter("@TrangThai", nghiPhep.TrangThai));
                    cmd.Parameters.Add(CreateParameter("@LyDoBoSung", nghiPhep.LyDoBoSung));

                    result = cmd.ExecuteNonQuery();

                    cn.Close();
                }
            }
            return result;
        }

        public int Update(NghiPhep nghiPhep)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.NghiPhep_Update, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@ID", nghiPhep.ID));
                    cmd.Parameters.Add(CreateParameter("@MaNV", nghiPhep.MaNV));
                    cmd.Parameters.Add(CreateParameter("@NgayBatDau", nghiPhep.NgayBatDau));
                    cmd.Parameters.Add(CreateParameter("@NgayKetThuc", nghiPhep.NgayKetThuc));
                    cmd.Parameters.Add(CreateParameter("@MaLoaiNghiPhep", nghiPhep.MaLoaiNghiPhep));
                    cmd.Parameters.Add(CreateParameter("@TrangThai", nghiPhep.TrangThai));
                    cmd.Parameters.Add(CreateParameter("@LyDoBoSung", nghiPhep.LyDoBoSung));
                    result = cmd.ExecuteNonQuery();

                    cn.Close();
                }
            }
            return result;
        }

        public int Delete(int id)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.NghiPhep_Delete, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@ID", id));

                    result = cmd.ExecuteNonQuery();

                    cn.Close();
                }
            }
            return result;
        }
        public int UpdateTrangThai(int id, string trangThai)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.NghiPhep_UpdateTrangThai, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@ID", id));
                    cmd.Parameters.Add(CreateParameter("@TrangThai", trangThai));

                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }
        public IList<NghiPhep> ListByMaNV(int maNV)
        {
            List<NghiPhep> data = new List<NghiPhep>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.NghiPhep_ListByMaNV, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaNV", maNV));

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            NghiPhep nghiPhep = new NghiPhep();

                            nghiPhep.ID = Convert.ToInt32(dbReader["ID"]);
                            nghiPhep.MaNV = Convert.ToInt32(dbReader["MaNV"]);
                            nghiPhep.NgayBatDau = Convert.ToDateTime(dbReader["NgayBatDau"]);
                            nghiPhep.NgayKetThuc = Convert.ToDateTime(dbReader["NgayKetThuc"]);
                            nghiPhep.MaLoaiNghiPhep = Convert.ToInt32(dbReader["MaLoaiNghiPhep"]);
                            nghiPhep.TrangThai = dbReader["TrangThai"].ToString();
                            nghiPhep.LyDoBoSung = dbReader["LyDoBoSung"].ToString();
                            data.Add(nghiPhep);
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
