using System;
using System.Collections.Generic;
using DemoApp.DataLayers;
using DemoApp.Entities;

namespace DemoApp.BussinessLayers
{
    public static class HopDongLaoDongService
    {
        private static IHopDongLaoDongDAL _hopDongLaoDongDAL;

        public static void Initialize(TypeOfDatabase dbType, string connectionString)
        {
            switch (dbType)
            {
                case TypeOfDatabase.SqlServer:
                    _hopDongLaoDongDAL = new DataLayers.SqlServer.HopDongLaoDongDAL(connectionString);
                    break;
                    // Add cases for other database types if needed.
            }
        }

        public static int InsertHopDongLaoDong(HopDongLaoDong hopDongLaoDong)
        {
            return _hopDongLaoDongDAL.Insert(hopDongLaoDong);
        }

        public static int UpdateHopDongLaoDong(HopDongLaoDong hopDongLaoDong)
        {
            return _hopDongLaoDongDAL.Update(hopDongLaoDong);
        }

        public static int DeleteHopDongLaoDong(HopDongLaoDong hopDongLaoDong)
        {
            return _hopDongLaoDongDAL.Delete(hopDongLaoDong);
        }
        public static int GetTotalHopDongs()
        {
            return _hopDongLaoDongDAL.GetTotalNumberOfHopDongs();
        }

        public static IList<HopDongLaoDong> GetAllHopDongLaoDong()
        {
            return _hopDongLaoDongDAL.Select();
        }
        public static IList<HopDongLaoDong> GetByMaNV(int maNV)
        {
            return _hopDongLaoDongDAL.GetByMaNV(maNV);
        }
        public static HopDongLaoDong GetHopDongByMaHD(int maHD)
        {
            return _hopDongLaoDongDAL.GetHopDongByMaHD(maHD);
        }
    }
}
