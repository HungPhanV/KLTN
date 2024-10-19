using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DemoApp.Entities;

namespace DemoApp.DataLayers.SqlServer
{
    public class TaiKhoanDAL : _BaseDAL, ITaiKhoanDAL
    {
        public TaiKhoanDAL(string connectionString)
            : base(connectionString)
        {
        }

        public TaiKhoan Authorize(string userName, string password)
        {
            TaiKhoan data = null;

            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, Username, MaNV, Role
                                FROM TaiKhoan
                                WHERE Username = @Username AND Password = @Password";

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@Username", userName);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            data = new TaiKhoan()
                            {
                                // Gán thêm giá trị ID
                                id = Convert.ToInt32(dbReader["id"]),
                                Username = Convert.ToString(dbReader["Username"]),
                                MaNV = Convert.ToInt32(dbReader["MaNV"]),
                                Role = Convert.ToString(dbReader["Role"])
                            };
                        }
                        dbReader.Close();
                    }
                }
            }

            return data;
        }

        public IList<Entities.TaiKhoan> List()
        {
            List<TaiKhoan> data = new List<TaiKhoan>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.TaiKhoan_SelectAll, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new TaiKhoan()
                            {
                                Username = dbReader["Username"].ToString(),
                                Password = dbReader["Password"].ToString(),
                                MaNV = Convert.ToInt32(dbReader["MaNV"]),
                                Role = dbReader["Role"].ToString()
                            });
                        }
                        dbReader.Close();
                    }
                }
                cn.Close();
            }
            return data;
        }

        public int Update(TaiKhoan tk)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.TaiKhoan_Update, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@Username", tk.Username));
                    cmd.Parameters.Add(CreateParameter("@Password", tk.Password));
                    cmd.Parameters.Add(CreateParameter("@MaNV", tk.MaNV));
                    cmd.Parameters.Add(CreateParameter("@Role", tk.Role));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }

        public int DeleteByMaNV(int maNV)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.TaiKhoan_Delete, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaNV", maNV));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }


        public int UpdateRoleByMaNV(int maNV, string newRole)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.TaiKhoan_UpdateRoleByMaNV, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaNV", maNV));
                    cmd.Parameters.Add(CreateParameter("@NewRole", newRole));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }
        public TaiKhoan GetByID(int id)
        {
            TaiKhoan data = null;

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.TaiKhoan_SelectByID, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            data = new TaiKhoan()
                            {
                                id = Convert.ToInt32(dbReader["id"]),
                                Username = dbReader["Username"].ToString(),
                                Password = dbReader["Password"].ToString(), // Nếu cần lấy cả mật khẩu
                                MaNV = Convert.ToInt32(dbReader["MaNV"]),
                                Role = dbReader["Role"].ToString()
                            };
                        }
                        dbReader.Close();
                    }
                }
            }

            return data;
        }

    }
}
