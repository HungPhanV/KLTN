using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DemoApp.Entities;

namespace DemoApp.DataLayers.SqlServer
{
    public class LoaiNghiPhepDAL : _BaseDAL, ILoaiNghiPhepDAL
    {
        public LoaiNghiPhepDAL(string connectionString)
            : base(connectionString)
        {
        }

        public IList<Entities.LoaiNghiPhep> List()
        {
            List<LoaiNghiPhep> data = new List<LoaiNghiPhep>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.LoaiNghiPhep_List, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            LoaiNghiPhep loaiNghiPhep = new LoaiNghiPhep();
                            loaiNghiPhep.MaLoaiNghiPhep = Convert.ToInt32(dbReader["MaLoaiNghiPhep"]);
                            loaiNghiPhep.TenLoai = dbReader["TenLoai"].ToString();
                            data.Add(loaiNghiPhep);
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
