using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DemoApp.Entities;

namespace DemoApp.DataLayers.SqlServer
{
    public class ViTriDAL : _BaseDAL, IViTriDAL
    {
        public ViTriDAL(string connectionString)
            : base(connectionString)
        {
        }

        public IList<Entities.ViTri> List()
        {
            List<ViTri> data = new List<ViTri>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.ViTri_Select, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new ViTri()
                            {
                                MaVT = Convert.ToInt32(dbReader["MaVT"]),
                                TenVT = dbReader["TenVT"].ToString()
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

