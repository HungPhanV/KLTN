using System;
using System.Collections.Generic;
using DemoApp.Entities;
using DemoApp.DataLayers;

namespace DemoApp.BussinessLayers
{
    public static class CaLamService
    {
        private static ICaLamDAL _CaLamDAL;

        public static void Initialize(TypeOfDatabase dbType, string connectionString)
        {
            switch (dbType)
            {
                case TypeOfDatabase.SqlServer:
                    _CaLamDAL = new DataLayers.SqlServer.CaLamDAL(connectionString);
                    break;
                    // Thêm các trường hợp cho các loại cơ sở dữ liệu khác nếu cần
            }
        }
        public static bool KiemTraCaLam(int maNV, DateTime ngay)
        {
            return _CaLamDAL.KiemTraCaLam(maNV, ngay);
        }
        public static int LayMaCaLam(int maNV, DateTime ngay)
        {
            return _CaLamDAL.LayMaCaLam(maNV, ngay);
        }
        public static IList<CaLam> ListCaLam()
        {
            return _CaLamDAL.List();
        }
        public static IList<Entities.CaLam> ListByMaNV(int maNV)
        {
            return _CaLamDAL.ListByMaNV(maNV);
        }
        public static int InsertCaLam(CaLam caLam)
        {
            return _CaLamDAL.Insert(caLam);
        }

        public static int UpdateCaLam(CaLam caLam)
        {
            return _CaLamDAL.Update(caLam);
        }

        public static int DeleteCaLam(int caLam)
        {
            return _CaLamDAL.Delete(caLam);
        }
    }
}
