using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Entities;
namespace DemoApp.DataLayers
{
    public interface IViTriDAL
    {
        IList<Entities.ViTri> List();
     
    }
}
