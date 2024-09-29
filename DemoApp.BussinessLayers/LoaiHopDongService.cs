using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Entities;
using DemoApp.DataLayers;
namespace DemoApp.BussinessLayers
{
    public static class LoaiHopDongService
    {
        private static ILoaiHopDongDAL _LoaiHDDAL;

        public static void Initialize(TypeOfDatabase dbType, string connectionString)
        {
            switch (dbType)
            {
                case TypeOfDatabase.SqlServer:
                    _LoaiHDDAL = new DataLayers.SqlServer.LoaiHopDongDAL(connectionString);
                    break;
                    // Thêm các trường hợp cho các loại cơ sở dữ liệu khác nếu cần
            }
        }

        public static IList<LoaiHopDong> ListLoaiHD()
        {
            return _LoaiHDDAL.List();
        }

    }
}
