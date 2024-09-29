using DemoApp.DataLayers.SqlServer;
using DemoApp.Entities;
using System;
using System.Collections.Generic;

namespace DemoApp.BussinessLayers
{
    public class TaiKhoanService
    {
        private static ITaiKhoanDAL _TaiKhoanDB;

        public static void Initialize(TypeOfDatabase dbType, string connectionString)
        {
            switch (dbType)
            {
                case TypeOfDatabase.SqlServer:
                    _TaiKhoanDB = new DataLayers.SqlServer.TaiKhoanDAL(connectionString);
                    break;

            }
        }

      
        public static TaiKhoan Authorize(string userName, string password)
        {
            return _TaiKhoanDB.Authorize(userName, password);
        }
        public static IList<Entities.TaiKhoan> List()
        {
            return _TaiKhoanDB.List();
        }
        public static int Update(TaiKhoan tk)
        {
            return _TaiKhoanDB.Update(tk);
        }
        public static int Delete(int maNV)
        {
            return _TaiKhoanDB.DeleteByMaNV(maNV);
        }
    }
}
