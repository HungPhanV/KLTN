using System;
using System.Collections.Generic;
using DemoApp.Entities;
using DemoApp.DataLayers;

namespace DemoApp.BussinessLayers
{
    public static class LoaiNghiPhepService
    {
        private static ILoaiNghiPhepDAL _LoaiNghiPhepDAL;

        public static void Initialize(TypeOfDatabase dbType, string connectionString)
        {
            switch (dbType)
            {
                case TypeOfDatabase.SqlServer:
                    _LoaiNghiPhepDAL = new DataLayers.SqlServer.LoaiNghiPhepDAL(connectionString);
                    break;
                    // Thêm các trường hợp cho các loại cơ sở dữ liệu khác nếu cần
            }
        }

        public static IList<Entities.LoaiNghiPhep> ListLoaiNghiPhep()
        {
            return _LoaiNghiPhepDAL.List();
        }

        
    }
}
