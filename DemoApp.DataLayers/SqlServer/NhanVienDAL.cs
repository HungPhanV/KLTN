using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DemoApp.Entities;

namespace DemoApp.DataLayers.SqlServer
{
    public class NhanVienDAL : _BaseDAL, INhanVienDAL
    {
        public NhanVienDAL(string connectionString)
            : base(connectionString)
        {
        }
        public string GetHinhAnhById(int id)
        {
            string hinhAnh = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT HinhAnh FROM NhanVien WHERE MaNV = @MaNV";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaNV", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hinhAnh = reader["HinhAnh"].ToString();
                        }
                    }
                }
            }

            return hinhAnh;
        }
        public IList<Entities.NhanVien> ListPaged(int pageNumber, int pageSize)
        {
            List<NhanVien> data = new List<NhanVien>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.NhanVien_ListPaged, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new NhanVien()
                            {
                                MaNV = Convert.ToInt32(dbReader["MaNV"]),
                                HoTen = dbReader["HoTen"].ToString(),
                                NgaySinh = Convert.ToDateTime(dbReader["NgaySinh"]),
                                QueQuan = dbReader["QueQuan"].ToString(),
                                GioiTinh = dbReader["GioiTinh"].ToString(),
                                SDT = dbReader["SDT"].ToString(),
                                HinhAnh = dbReader["HinhAnh"].ToString()
                            });
                        }
                        dbReader.Close();
                    }
                }
                cn.Close();
            }
            return data;
        }

        // Phương thức để lấy tổng số lượng nhân viên
        public int GetTotalNumberOfNhanViens()
        {
            int totalRecords = 0;

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM nhanvien", cn))
                {
                    totalRecords = (int)cmd.ExecuteScalar();
                }

                cn.Close();
            }

            return totalRecords;
        }
    // Phương thức để lấy tổng số lượng nhân viên sau khi áp dụng điều kiện lọc
        public int GetTotalNumberOfFilteredNhanViens(string searchTerm, string gender, int maPB)
        {
            int totalRecords = 0;

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();

                // Tạo một câu truy vấn SQL dựa trên các điều kiện lọc được truyền vào
                string query = "SELECT COUNT(*) FROM NhanVien WHERE 1 = 1"; // 1=1 để bắt đầu câu truy vấn

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query += " AND HoTen LIKE @SearchTerm";
                }

                if (!string.IsNullOrEmpty(gender) && gender != "all")
                {
                    query += " AND GioiTinh = @Gender";
                }

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                    }

                    if (!string.IsNullOrEmpty(gender) && gender != "all")
                    {
                        cmd.Parameters.AddWithValue("@Gender", gender);
                    }

                    totalRecords = (int)cmd.ExecuteScalar();
                }

                cn.Close();
            }

            return totalRecords;
        }

        public IList<Entities.NhanVien> List()
        {
            List<NhanVien> data = new List<NhanVien>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.NhanVien_Select, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new NhanVien()
                            {
                                MaNV = Convert.ToInt32(dbReader["MaNV"]),
                                HoTen = dbReader["HoTen"].ToString(),
                                NgaySinh = Convert.ToDateTime(dbReader["NgaySinh"]),
                                QueQuan = dbReader["QueQuan"].ToString(),
                                GioiTinh = dbReader["GioiTinh"].ToString(),
                                SDT = dbReader["SDT"].ToString(),
                                HinhAnh = dbReader["HinhAnh"].ToString()
                            });
                        }
                        dbReader.Close();
                    }
                }
                cn.Close();
            }
            return data;
        }

        public NhanVien Get(int maNhanVien)
        {
            NhanVien data = new NhanVien();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.NhanVien_Detail, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@Manv", maNhanVien));
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            data = new NhanVien()
                            {
                                MaNV = Convert.ToInt32(dbReader["MaNV"]),
                                HoTen = dbReader["HoTen"].ToString(),
                                NgaySinh = Convert.ToDateTime(dbReader["NgaySinh"]),
                                QueQuan = dbReader["QueQuan"].ToString(),
                                GioiTinh = dbReader["GioiTinh"].ToString(),
                                SDT = dbReader["SDT"].ToString(),
                                HinhAnh = dbReader["HinhAnh"].ToString()


                            };
                        }
                        dbReader.Close();
                    }
                }
                cn.Close();
            }

            return data;
        }

        public int Insert(NhanVien nv)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.NhanVien_Insert, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@HoTen", nv.HoTen));
                    cmd.Parameters.Add(CreateParameter("@QueQuan", nv.QueQuan));
                    cmd.Parameters.Add(CreateParameter("@NgaySinh", nv.NgaySinh));
                    cmd.Parameters.Add(CreateParameter("@GioiTinh", nv.GioiTinh));
                    cmd.Parameters.Add(CreateParameter("@SDT", nv.SDT));
                    cmd.Parameters.Add(CreateParameter("@HinhAnh", nv.HinhAnh)); // Thêm HinhAnh vào đây

                    int rowsAffected = cmd.ExecuteNonQuery();

                    cn.Close();

                    return rowsAffected; // Trả về số hàng bị ảnh hưởng
                }
            }
            return result;
        }

        public int Update(NhanVien nv)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.NhanVien_Update, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@MaNV", nv.MaNV));
                    cmd.Parameters.Add(CreateParameter("@HoTen", nv.HoTen));
                    cmd.Parameters.Add(CreateParameter("@NgaySinh", nv.NgaySinh));
                    cmd.Parameters.Add(CreateParameter("@QueQuan", nv.QueQuan));
                    cmd.Parameters.Add(CreateParameter("@GioiTinh", nv.GioiTinh));
                    cmd.Parameters.Add(CreateParameter("@SDT", nv.SDT));
                    cmd.Parameters.Add(CreateParameter("@HinhAnh", nv.HinhAnh)); // Thêm HinhAnh vào đây

                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }

        public int Delete(NhanVien nv)
        {
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.NhanVien_Delete, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@Manv", nv.MaNV));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                    return rowsAffected;
                }
            }
        }


        public IList<NhanVien> SearchAndFilterPaged(string searchTerm, string gender, int maPB, int pageNumber, int pageSize, out int rowCount)
        {
            List<NhanVien> data = new List<NhanVien>();

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.NhanVien_SearchAndFilterPaged, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@SearchTerm", searchTerm));
                    cmd.Parameters.Add(CreateParameter("@Gender", gender));
                    cmd.Parameters.Add(CreateParameter("@MaPB", maPB));
                    cmd.Parameters.Add(CreateParameter("@PageNumber", pageNumber));
                    cmd.Parameters.Add(CreateParameter("@PageSize", pageSize));
                    cmd.Parameters.Add("@rowCount", SqlDbType.Int).Direction = ParameterDirection.Output;


                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            NhanVien nhanVien = new NhanVien()
                            {
                                MaNV = Convert.ToInt32(dbReader["MaNV"]),
                                HoTen = dbReader["HoTen"].ToString(),
                                NgaySinh = dbReader["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(dbReader["NgaySinh"]) : DateTime.MinValue,
                                QueQuan = dbReader["QueQuan"].ToString(),
                                GioiTinh = dbReader["GioiTinh"].ToString(),
                                SDT = dbReader["SDT"].ToString(),
                                HinhAnh = dbReader["HinhAnh"].ToString()
                            };

                            // Kiểm tra giá trị DBNull trước khi chuyển đổi và gán giá trị cho MaPB, MaVT, MaCV
                            if (dbReader["MaPB"] != DBNull.Value)
                            {
                                nhanVien.MaPB = Convert.ToInt32(dbReader["MaPB"]);
                            }
                            else
                            {
                                nhanVien.MaPB = 0; // Gán giá trị mặc định (hoặc giá trị phù hợp) khi là DBNull
                            }

                            if (dbReader["MaVT"] != DBNull.Value)
                            {
                                nhanVien.MaVT = Convert.ToInt32(dbReader["MaVT"]);
                            }
                            else
                            {
                                nhanVien.MaVT = 0; // Gán giá trị mặc định (hoặc giá trị phù hợp) khi là DBNull
                            }

                            if (dbReader["MaCV"] != DBNull.Value)
                            {
                                nhanVien.MaCV = Convert.ToInt32(dbReader["MaCV"]);
                            }
                            else
                            {
                                nhanVien.MaCV = 0; // Gán giá trị mặc định (hoặc giá trị phù hợp) khi là DBNull
                            }

                            data.Add(nhanVien);
                        }

                        dbReader.Close();
                        rowCount = Convert.ToInt32(cmd.Parameters["@rowCount"].Value);
                        
                    }
                }
                cn.Close();
            }
            return data;
        }

        public int CountNhanVienByGender(string gender)
        {
            int count = 0;

            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CommandList.NhanVien_CountByGender, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(CreateParameter("@Gender", gender));

             

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        count = Convert.ToInt32(result);
                    }
                }
            }

            return count;
        }

        public string GetTenNhanVien(int maNV)
        {
            string tenNhanVien = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT HoTen FROM NhanVien WHERE MaNV = @MaNV";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaNV", maNV);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        tenNhanVien = result.ToString();
                    }
                }
            }

            return tenNhanVien;
        }

    }
}
