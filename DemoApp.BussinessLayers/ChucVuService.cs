using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Entities;
using DemoApp.DataLayers;
namespace DemoApp.BussinessLayers
{
    public static class ChucVuService
    {
        private static IChucVuDAL _ChucVuDAL;

        public static void Initialize(TypeOfDatabase dbType, string connectionString)
        {
            switch (dbType)
            {
                case TypeOfDatabase.SqlServer:
                    _ChucVuDAL = new DataLayers.SqlServer.ChucVuDAL(connectionString);
                    break;
                    // Thêm các trường hợp cho các loại cơ sở dữ liệu khác nếu cần
            }
        }

        public static IList<ChucVu> ListChucVu()
        {
            return _ChucVuDAL.List();
        }

        public static int InsertQuaTrinh(ChucVu chucVu)
        {
            return _ChucVuDAL.Insert(chucVu);
        }

        public static int UpdateQuaTrinh(ChucVu chucVu)
        {
            return _ChucVuDAL.Update(chucVu);
        }

        public static int DeleteQuaTrinh(ChucVu chucVu)
        {
            return _ChucVuDAL.Delete(chucVu);
        }

    }
}
