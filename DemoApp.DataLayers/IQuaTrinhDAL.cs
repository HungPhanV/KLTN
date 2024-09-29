using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Entities;
namespace DemoApp.DataLayers
{
    public interface IQuaTrinhDAL
    {
        int Insert(QuaTrinh id);
        int Update(QuaTrinh id);
        int Delete(QuaTrinh id);
        IList<QuaTrinh> GetByMaNV(int maNV);
    }
}
