using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Entities;
namespace DemoApp.DataLayers
{
    public interface INghiPhepDAL
    {
        int Insert(NghiPhep nghiPhep);
        int Update(NghiPhep nghiPhep);
        int Delete(int nghiPhep);
        IList<Entities.NghiPhep> List();
        int UpdateTrangThai(int id, string trangThai);
        IList<NghiPhep> ListByMaNV(int maNV);
    }
}
