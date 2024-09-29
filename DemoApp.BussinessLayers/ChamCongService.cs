using System;
using System.Collections.Generic;
using DemoApp.Entities;
using DemoApp.DataLayers;

namespace DemoApp.BussinessLayers
{
    public static class ChamCongService
    {
        private static IChamCongDAL _ChamCongDAL;

        public static void Initialize(TypeOfDatabase dbType, string connectionString)
        {
            switch (dbType)
            {
                case TypeOfDatabase.SqlServer:
                    _ChamCongDAL = new DataLayers.SqlServer.ChamCongDAL(connectionString);
                    break;
                    // Thêm các trường hợp cho các loại cơ sở dữ liệu khác nếu cần
            }
        }
        public static IList<Entities.ChamCong> TongHopChamCong(int maNV)
        {
            return _ChamCongDAL.TongHopChamCong(maNV);
        }
        public static IList<Entities.ChamCong> TongHopChamCongTheoThang(int thang, int nam)
        {
            return _ChamCongDAL.TongHopChamCongTheoThang(thang,nam);
        }
        public static bool KiemTraDaChamCong(int maCaLam)
        {
            return _ChamCongDAL.KiemTraDaChamCong(maCaLam);
        }
        public static int UpdateThoiGianKetThucChamCong(int maCaLam, TimeSpan thoiGianKetThuc)
        {
            return _ChamCongDAL.UpdateThoiGianKetThucChamCong(maCaLam, thoiGianKetThuc);
        }
        public static IList<ChamCong> ListChamCong()
        {
            return _ChamCongDAL.List();
        }
        public static IList<Entities.ChamCong> ListByMaNV(int maNV)
        {
            return _ChamCongDAL.ListByMaNV(maNV);
        }
        public static int InsertChamCong(ChamCong chamCong)
        {
            return _ChamCongDAL.Insert(chamCong);
        }

        public static int UpdateChamCong(ChamCong chamCong)
        {
            return _ChamCongDAL.Update(chamCong);
        }

        public static int DeleteChamCong(int chamCong)
        {
            return _ChamCongDAL.Delete(chamCong);
        }
    }
}
