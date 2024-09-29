using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DemoApp.Entities;

namespace DemoApp.DataLayers.SqlServer
{
    public class LoaiCaLamDAL : _BaseDAL, ILoaiCaLamDAL
    {
        public LoaiCaLamDAL(string connectionString)
            : base(connectionString)
        {
        }

        public IList<LoaiCaLam> List()
        {
            List<LoaiCaLam> data = new List<LoaiCaLam>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.LoaiCaLam_Select, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new LoaiCaLam()
                            {
                                MaLoai = Convert.ToInt32(dbReader["MaLoai"]),
                                TenCa = dbReader["TenCa"].ToString(),
                                ThoiGianBatDau = TimeSpan.Parse(dbReader["ThoiGianBatDau"].ToString()),
                                ThoiGianKetThuc = TimeSpan.Parse(dbReader["ThoiGianKetThuc"].ToString())
                            });
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
