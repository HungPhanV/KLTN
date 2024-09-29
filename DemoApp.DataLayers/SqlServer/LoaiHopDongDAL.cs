using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DemoApp.Entities;

namespace DemoApp.DataLayers.SqlServer
{
    public class LoaiHopDongDAL : _BaseDAL, ILoaiHopDongDAL
    {
        public LoaiHopDongDAL(string connectionString)
            : base(connectionString)
        {
        }

        public IList<Entities.LoaiHopDong> List()
        {
            List<LoaiHopDong> data = new List<LoaiHopDong>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.LoaiHopDong_Select, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new LoaiHopDong()
                            {
                                MaLoaiHD = Convert.ToInt32(dbReader["MaLoaiHD"]),
                                TenLoaiHD = dbReader["TenLoaiHD"].ToString()
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

