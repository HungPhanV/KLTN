using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.DataLayers
{
  
    public interface ILoaiNghiPhepDAL
    {
        IList<Entities.LoaiNghiPhep> List();

    }
}
