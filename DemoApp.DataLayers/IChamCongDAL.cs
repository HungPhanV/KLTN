using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Entities;
namespace DemoApp.DataLayers
{
    public interface IChamCongDAL
    {
        int Insert(ChamCong chamCong);
        int Update(ChamCong chamCong);
        int Delete(int chamCong);
        IList<Entities.ChamCong> List();
        IList<Entities.ChamCong> ListByMaNV(int maNV);
        bool KiemTraDaChamCong(int maCaLam);
        int UpdateThoiGianKetThucChamCong(int maCaLam, TimeSpan thoiGianKetThuc);
        IList<Entities.ChamCong> TongHopChamCong(int maNV);
        IList<Entities.ChamCong> TongHopChamCongTheoThang(int thang, int nam);
    }
}
