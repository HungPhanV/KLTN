using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DemoApp.Entities;

namespace DemoApp.DataLayers.SqlServer
{
    public class ChucVuDAL : _BaseDAL, IChucVuDAL
    {
        public ChucVuDAL(string connectionString)
            : base(connectionString)
        {
        }

        public IList<Entities.ChucVu> List()
        {
            List<ChucVu> data = new List<ChucVu>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.ChucVu_Select, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new ChucVu()
                            {
                                MaCV = Convert.ToInt32(dbReader["MaCV"]),
                                TenCV = dbReader["TenCV"].ToString()
                            });
                        }
                        dbReader.Close();
                    }
                }
                cn.Close();
            }
            return data;
        }

        public int Insert(ChucVu cv)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.ChucVu_Create, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@TenCV", cv.TenCV));
                    int rowsAffected = cmd.ExecuteNonQuery();

                    cn.Close();

                    return rowsAffected; // Trả về số hàng bị ảnh hưởng
                }
            }
        }

        public int Update(ChucVu cv)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.ChucVu_Update, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaCV", cv.MaCV));
                    cmd.Parameters.Add(CreateParameter("@TenCV", cv.TenCV));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }

        public int Delete(ChucVu cv)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.ChucVu_Delete, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaCV", cv.MaCV));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }
    }
}
