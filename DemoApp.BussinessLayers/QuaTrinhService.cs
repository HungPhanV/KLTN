    using System;
    using System.Collections.Generic;
    using DemoApp.DataLayers;
    using DemoApp.Entities;

    namespace DemoApp.BussinessLayers
    {
        public static class QuaTrinhService
        {
            private static IQuaTrinhDAL _QuaTrinhDAL;

            public static void Initialize(TypeOfDatabase dbType, string connectionString)
            {
                switch (dbType)
                {
                    case TypeOfDatabase.SqlServer:
                        _QuaTrinhDAL = new DataLayers.SqlServer.QuaTrinhDAL(connectionString);
                        break;
                        // Thêm các trường hợp cho các loại cơ sở dữ liệu khác nếu cần
                }
            }

            public static int InsertQuaTrinh(QuaTrinh quaTrinh)
            {
                return _QuaTrinhDAL.Insert(quaTrinh);
            }

            public static int UpdateQuaTrinh(QuaTrinh quaTrinh)
            {
                return _QuaTrinhDAL.Update(quaTrinh);
            }

            public static int DeleteQuaTrinh(QuaTrinh quaTrinh)
            {
                return _QuaTrinhDAL.Delete(quaTrinh);
            }

            public static IList<QuaTrinh> GetQuaTrinhByMaNV(int maNV)
            {
                return _QuaTrinhDAL.GetByMaNV(maNV);
            }
        }
    }
