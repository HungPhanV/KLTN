using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Entities;
namespace DemoApp.DataLayers
{
    public interface IChucVuDAL
    {
        IList<Entities.ChucVu> List();
        int Insert(ChucVu cv);
        int Update(ChucVu cv);
        int Delete(ChucVu cv);
    }
}
