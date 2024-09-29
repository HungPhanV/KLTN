using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Entities;
namespace DemoApp.DataLayers
{
    public interface IHopDongLaoDongDAL
    {
        int Insert(HopDongLaoDong hopDong);
        int Update(HopDongLaoDong hopDong);
        int Delete(HopDongLaoDong hopDong);
        IList<HopDongLaoDong> GetByMaNV(int maNV);
        IList<Entities.HopDongLaoDong> Select();
        HopDongLaoDong GetHopDongByMaHD(int maHD);
        int GetTotalNumberOfHopDongs();
    }
}
