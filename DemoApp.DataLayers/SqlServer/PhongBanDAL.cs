using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DemoApp.Entities;

namespace DemoApp.DataLayers.SqlServer
{
    public class PhongBanDAL : _BaseDAL, IPhongBanDAL
    {
        public PhongBanDAL(string connectionString)
            : base(connectionString)
        {
        }
        //public IList<Entities.PhongBan> ListPagedPhongBan(int pageNumber, int pageSize)
        //{
        //    List<PhongBan> data = new List<PhongBan>();

        //    using (SqlConnection cn = GetConnection())
        //    {
        //        using (SqlCommand cmd = new SqlCommand(CommandList.PhongBan_SelectedPaged, cn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
        //            cmd.Parameters.AddWithValue("@PageSize", pageSize);

        //            using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
        //            {
        //                while (dbReader.Read())
        //                {
        //                    data.Add(new PhongBan()
        //                    {
        //                        MaPB = Convert.ToInt32(dbReader["MaPB"]),
        //                        TenPB = dbReader["TenPB"].ToString(),
        //                        DiaChi = dbReader["DiaChi"].ToString(),
        //                        SDTPB = dbReader["SDTPB"].ToString(),
        //                        MaPBParent = dbReader["MaPBParent"] != DBNull.Value ? (int?)Convert.ToInt32(dbReader["MaPBParent"]) : null
        //                    });
        //                }
        //                dbReader.Close();
        //            }
        //        }
        //        cn.Close();
        //    }
        //    return data;
        //}
        // Phương thức để lấy tổng số lượng nhân viên
        public int GetTotalNumberOfPhongBans()
        {
            int totalRecords = 0;

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM phongban", cn))
                {
                    totalRecords = (int)cmd.ExecuteScalar();
                }

                cn.Close();
            }

            return totalRecords;
        }

        //public int GetTotalNumberOfFilteredPhongBans(string departmentName)
        //{
        //    int totalRecords = 0;

        //    using (SqlConnection cn = new SqlConnection(connectionString))
        //    {
        //        cn.Open();

        //        // Tạo câu truy vấn SQL dựa trên điều kiện tìm kiếm theo tên phòng ban
        //        string query = "SELECT COUNT(*) FROM PhongBan WHERE MaPBParent = 0"; // 1=1 để bắt đầu câu truy vấn

        //        if (!string.IsNullOrEmpty(departmentName) && departmentName != "all")
        //        {
        //            query += " AND TenPB LIKE @DepartmentName";
        //        }

        //        using (SqlCommand cmd = new SqlCommand(query, cn))
        //        {
        //            if (!string.IsNullOrEmpty(departmentName) && departmentName != "all")
        //            {
        //                cmd.Parameters.AddWithValue("@DepartmentName", "%" + departmentName + "%");
        //            }

        //            totalRecords = (int)cmd.ExecuteScalar();
        //        }

        //        cn.Close();
        //    }

        //    return totalRecords;
        //}

        //public IList<PhongBan> SearchAndFilterPagedPhongBans(string searchTerm, int pageNumber, int pageSize)
        //{
        //    IList<PhongBan> phongBans = new List<PhongBan>();

        //    using (SqlConnection cn = GetConnection())
        //    {
        //        using (SqlCommand cmd = new SqlCommand(CommandList.PhongBan_SearchAndFilterPaged, cn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
        //            cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
        //            cmd.Parameters.AddWithValue("@PageSize", pageSize);



        //            using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
        //            {
        //                while (dbReader.Read())
        //                {
        //                    phongBans.Add(new PhongBan()
        //                    {
        //                        MaPB = Convert.ToInt32(dbReader["MaPB"]),
        //                        TenPB = dbReader["TenPB"].ToString(),
        //                        DiaChi = dbReader["DiaChi"].ToString(),
        //                        SDTPB = dbReader["SDTPB"].ToString(),
        //                        MaPBParent = dbReader["MaPBParent"] != DBNull.Value ? Convert.ToInt32(dbReader["MaPBParent"]) : (int?)null
        //                    });
        //                }
        //                dbReader.Close();
        //            }
        //        }
        //        cn.Open();
        //    }

        //    return phongBans;
        //}

        public IList<Entities.PhongBan> List()
        {
            List<PhongBan> data = new List<PhongBan>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.PhongBan_Select, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new PhongBan()
                            {
                                MaPB = dbReader["MaPB"] != DBNull.Value ? Convert.ToInt32(dbReader["MaPB"]) : 0,
                                TenPB = dbReader["TenPB"].ToString(),
                                DiaChi = dbReader["DiaChi"].ToString(),
                                SDTPB = dbReader["SDTPB"].ToString(),
                                MaPBParent = dbReader["MaPBParent"] != DBNull.Value ? Convert.ToInt32(dbReader["MaPBParent"]) : 0
                            });
                        }
                        dbReader.Close();
                    }
                }
                cn.Close();
            }
            return data;
        }


        public PhongBan Get(int MaPB)
        {
            PhongBan data = null;

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.PhongBan_Get, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaPB", MaPB));
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            data = new PhongBan()
                            {
                                MaPB = dbReader["MaPB"] != DBNull.Value ? Convert.ToInt32(dbReader["MaPB"]) : 0,
                                TenPB = dbReader["TenPB"].ToString(),
                                DiaChi = dbReader["DiaChi"].ToString(),
                                SDTPB = dbReader["SDTPB"].ToString(),
                                MaPBParent = dbReader["MaPBParent"] != DBNull.Value ? Convert.ToInt32(dbReader["MaPBParent"]) : 0
                            };
                        }
                        dbReader.Close();
                    }
                }
                cn.Close();
            }

            return data;
        }


        public int Insert(PhongBan pb)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.PhongBan_Insert, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@TenPB", pb.TenPB));
                    cmd.Parameters.Add(CreateParameter("@DiaChi", pb.DiaChi));
                    cmd.Parameters.Add(CreateParameter("@SDTPB", pb.SDTPB));
                    cmd.Parameters.Add(CreateParameter("@MaPBParent", pb.MaPBParent));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected; // Trả về số hàng bị ảnh hưởng
                }
            }
            return result;
        }

        public int Update(PhongBan pb)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.PhongBan_Update, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaPB", pb.MaPB));
                    cmd.Parameters.Add(CreateParameter("@TenPB", pb.TenPB));
                    cmd.Parameters.Add(CreateParameter("@DiaChi", pb.DiaChi));
                    cmd.Parameters.Add(CreateParameter("@SDTPB", pb.SDTPB));
                    cmd.Parameters.Add(CreateParameter("@MaPBParent", pb.MaPBParent));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }

        public int Delete(PhongBan pb)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.PhongBan_Delete, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaPB", pb.MaPB));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }
        public IList<PhongBan> GetPhongBansByMaPBParent(int maPBParent)
        {
            List<PhongBan> data = new List<PhongBan>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.PhongBan_SelectByParent, cn)) // Điều chỉnh tên stored procedure tương ứng
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaPBParent", maPBParent));

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new PhongBan()
                            {
                                MaPB = dbReader["MaPB"] != DBNull.Value ? Convert.ToInt32(dbReader["MaPB"]) : 0,
                                TenPB = dbReader["TenPB"].ToString(),
                                DiaChi = dbReader["DiaChi"].ToString(),
                                SDTPB = dbReader["SDTPB"].ToString(),
                                MaPBParent = dbReader["MaPBParent"] != DBNull.Value ? Convert.ToInt32(dbReader["MaPBParent"]) : 0
                            });
                        }
                        dbReader.Close();
                    }
                }
                cn.Close();
            }
            return data;
        }

        public IList<PhongBan> SearchPhongBanByName(string tenPB)
        {
            List<PhongBan> data = new List<PhongBan>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.PhongBan_SearchByName, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@SearchTerm", tenPB)); // Change parameter name to @SearchTerm

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new PhongBan()
                            {
                                MaPB = dbReader["MaPB"] != DBNull.Value ? Convert.ToInt32(dbReader["MaPB"]) : 0,
                                TenPB = dbReader["TenPB"].ToString(),
                                DiaChi = dbReader["DiaChi"].ToString(),
                                SDTPB = dbReader["SDTPB"].ToString(),
                                MaPBParent = dbReader["MaPBParent"] != DBNull.Value ? Convert.ToInt32(dbReader["MaPBParent"]) : 0
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
