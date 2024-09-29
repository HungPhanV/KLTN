using DemoApp.DataLayers;
using DemoApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.BussinessLayers
{
    public static class PhongBanService
    {
        private static IPhongBanDAL _PhongBanDB;

        public static void Initialize(TypeOfDatabase dbType, string connectionString)
        {
            switch (dbType)
            {
                case TypeOfDatabase.SqlServer:
                    _PhongBanDB = new DataLayers.SqlServer.PhongBanDAL(connectionString);
                    break;
                    // Add more cases for other database types if needed
            }
        }

        public static IList<PhongBan> PhongBan_List()
        {
            return _PhongBanDB.List();
        }

        public static int PhongBan_Insert(PhongBan obj)
        {
            return _PhongBanDB.Insert(obj);
        }

        public static PhongBan PhongBan_Get(int Id)
        {
            return _PhongBanDB.Get(Id);
        }

        public static int PhongBan_Update(PhongBan obj)
        {
            return _PhongBanDB.Update(obj);
        }

        public static int PhongBan_Delete(PhongBan phongBanToDelete)
        {
            return _PhongBanDB.Delete(phongBanToDelete);
        }
        public static int PhongBan_DeleteWithChildren(int maPB)
        {
            // Trước tiên, lấy danh sách các phòng ban con dựa trên maPB
            var childPhongBans = _PhongBanDB.GetPhongBansByMaPBParent(maPB);

            // Xoá từng phòng ban con
            foreach (var childPhongBan in childPhongBans)
            {
                _PhongBanDB.Delete(childPhongBan);
            }

            // Sau đó, xoá phòng ban cha
            return _PhongBanDB.Delete(new PhongBan { MaPB = maPB });
        }
        public static IList<PhongBan> SearchPhongBanByName(string tenPB)
        {
            return _PhongBanDB.SearchPhongBanByName(tenPB);
        }
        //public static IList<Entities.PhongBan> ListPagedPhongBan(int pageNumber, int pageSize)
        //{
        //    return _PhongBanDB.ListPagedPhongBan(pageNumber, pageSize);
        //}
        public static int GetTotalNumberOfPhongBans()
       {
           return _PhongBanDB.GetTotalNumberOfPhongBans();
        }
        //public static int GetTotalNumberOfFilteredPhongBans(string departmentName)
        //{
        //    return _PhongBanDB.GetTotalNumberOfFilteredPhongBans(departmentName);
        //}
        //public static IList<PhongBan> SearchAndFilterPagedPhongBans(string searchTerm, int pageNumber, int pageSize)
        //{
        //    return _PhongBanDB.SearchAndFilterPagedPhongBans(searchTerm, pageNumber, pageSize);
        //}
    }
}
