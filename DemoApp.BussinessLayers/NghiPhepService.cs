using System;
using System.Collections.Generic;
using DemoApp.Entities;
using DemoApp.DataLayers;

namespace DemoApp.BussinessLayers
{
    public static class NghiPhepService
    {
        private static INghiPhepDAL _NghiPhepDAL;

        public static void Initialize(TypeOfDatabase dbType, string connectionString)
        {
            switch (dbType)
            {
                case TypeOfDatabase.SqlServer:
                    _NghiPhepDAL = new DataLayers.SqlServer.NghiPhepDAL(connectionString);
                    break;
                    // Thêm các trường hợp cho các loại cơ sở dữ liệu khác nếu cần
            }
        }
     
        public static IList<NghiPhep> ListNghiPhep()
        {
            return _NghiPhepDAL.List();
        }
      
        public static int InsertNghiPhep(NghiPhep nghiPhep)
        {
            return _NghiPhepDAL.Insert(nghiPhep);
        }
        public static IList<NghiPhep> ListByMaNV(int maNV)
        {
            return _NghiPhepDAL.ListByMaNV(maNV);
        }
        public static int UpdateNghiPhep(NghiPhep nghiPhep)
        {
            return _NghiPhepDAL.Update(nghiPhep);
        }

        public static int DeleteNghiPhep(int nghiPhep)
        {
            return _NghiPhepDAL.Delete(nghiPhep);
        }
        public static int UpdateTrangThai(int id, string trangThai)
        {
            return _NghiPhepDAL.UpdateTrangThai(id, trangThai);
        }
    }
}
