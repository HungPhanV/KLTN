using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Entities;
namespace DemoApp.DataLayers
{
    public interface IFileHopDongDAL
    {
        IList<FileHopDong> GetByMaHD(int maHD);
        int Insert(FileHopDong fileHopDong);
        FileHopDong GetByMaFile(int maFile);
        int Delete(FileHopDong fileHopDong);
    }
}
