using System;
using System.Collections.Generic;
using DemoApp.Entities;
using DemoApp.DataLayers;

namespace DemoApp.BussinessLayers
{
    public static class LoaiCaLamService
    {
        private static ILoaiCaLamDAL _LoaiCaLamDAL;

        public static void Initialize(TypeOfDatabase dbType, string connectionString)
        {
            switch (dbType)
            {
                case TypeOfDatabase.SqlServer:
                    _LoaiCaLamDAL = new DataLayers.SqlServer.LoaiCaLamDAL(connectionString);
                    break;
                    // Thêm các trường hợp cho các loại cơ sở dữ liệu khác nếu cần
            }
        }

        public static IList<LoaiCaLam> ListCaLam()
        {
            return _LoaiCaLamDAL.List();
        }

    }
}
