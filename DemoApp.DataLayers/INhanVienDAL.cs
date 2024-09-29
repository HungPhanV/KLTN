using DemoApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.DataLayers
{
    public interface INhanVienDAL
    {
        string GetTenNhanVien(int maNV);
        int GetTotalNumberOfNhanViens();
        int GetTotalNumberOfFilteredNhanViens(string searchTerm, string gender, int maPB);
        /// <summary>
        /// Lấy danh sách nhân viên
        /// </summary>
        /// <returns></returns>
        IList<NhanVien> List();
        IList<NhanVien> ListPaged(int pageNumber, int pageSize);
        IList<NhanVien> SearchAndFilterPaged(string searchTerm, string gender, int maPB, int pageNumber, int pageSize, out int rowCount);
        /// <summary>
        /// Lấy thông tin 1 nhân viên
        /// </summary>
        /// <param name="maNhanVien">Mã của nhân viên</param>
        /// <returns>đối tượng chứa thông tin nhân viên, null nếu không tồn tại</returns>
        NhanVien Get(int maNhanVien);
        string GetHinhAnhById(int id);
        int Insert(NhanVien nv);
        int Update(NhanVien nv);
        int Delete(NhanVien nv);
        int CountNhanVienByGender(string gender);
    }
}
