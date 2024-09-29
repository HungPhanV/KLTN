using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Entities;
using DemoApp.DataLayers;
namespace DemoApp.BussinessLayers
{
    public static class ViTriService
    {
        private static IViTriDAL _ViTriDAL;

        public static void Initialize(TypeOfDatabase dbType, string connectionString)
        {
            switch (dbType)
            {
                case TypeOfDatabase.SqlServer:
                    _ViTriDAL = new DataLayers.SqlServer.ViTriDAL(connectionString);
                    break;
                    // Thêm các trường hợp cho các loại cơ sở dữ liệu khác nếu cần
            }
        }

        public static IList<ViTri> ListViTri()
        {
            return _ViTriDAL.List();
        }

    }
}
