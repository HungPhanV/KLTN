using DemoApp.DataLayers;
using DemoApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.BussinessLayers
{
    public static class NhanVienService
    {
        private static INhanVienDAL _NhanVienDB;
        //cấu hình các đối tượng dữ liệu dựa trên loại cơ sở dữ liệu và chuỗi kết nối được cung cấp
        public static void Initialize(TypeOfDatabase dbType, string connectionString)
        {
            switch (dbType)
            {
                case TypeOfDatabase.SqlServer:
                    _NhanVienDB = new DataLayers.SqlServer.NhanVienDAL(connectionString);
                    break;
               
            }
        }
        public static int GetTotalNumberOfNhanViens()
        {
            return _NhanVienDB.GetTotalNumberOfNhanViens();
        }
        public static IList<Entities.NhanVien> NhanVien_ListPaged(int pageNumber, int pageSize)
        {
            return _NhanVienDB.ListPaged(pageNumber, pageSize);
        }
        public static int GetTotalNumberOfFilteredNhanViens(string searchTerm, string gender, int maPB)
        {
            return _NhanVienDB.GetTotalNumberOfFilteredNhanViens(searchTerm, gender, maPB);
        }
        public static IList<NhanVien> NhanVien_List()
        {
            return _NhanVienDB.List();
        }

        public static int NhanVien_Insert(NhanVien obj)
        {
            return _NhanVienDB.Insert(obj);
        }

        public static NhanVien NhanVien_Get(int Id)
        {
            return _NhanVienDB.Get(Id);
        }

        public static int NhanVien_Update(NhanVien obj)
        {
            return _NhanVienDB.Update(obj);
        }

        public static int NhanVien_Delete(NhanVien nhanVienToDelete)
        {
            return _NhanVienDB.Delete(nhanVienToDelete);
        }

        public static IList<NhanVien> SearchAndFilterPaged(string searchTerm, string gender, int maPB, int pageNumber, int pageSize, out int rowCount)
        {
            return _NhanVienDB.SearchAndFilterPaged(searchTerm, gender, maPB, pageNumber, pageSize, out rowCount);
        }
        public static string GetHinhAnhById(int id)
        {
            return _NhanVienDB.GetHinhAnhById(id);
        }
        public static int CountNhanVienByGender(string gender)
        {
            return _NhanVienDB.CountNhanVienByGender(gender);
        }
        public static string GetTenNhanVien(int maNV)
        {
            return _NhanVienDB.GetTenNhanVien(maNV);
        }
    }
}
