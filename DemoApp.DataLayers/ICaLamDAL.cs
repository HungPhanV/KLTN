using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Entities;
namespace DemoApp.DataLayers
{
    public interface ICaLamDAL
    {
        int Insert(CaLam caLam);
        int Update(CaLam caLam);
        int Delete(int caLam);
        IList<Entities.CaLam> List();
        IList<Entities.CaLam> ListByMaNV(int maNV);
        bool KiemTraCaLam(int maNV, DateTime ngay);
        int LayMaCaLam(int maNV, DateTime ngay);
        //IList<ChamCong> GetByMaNV(int maNV);
    }
}
