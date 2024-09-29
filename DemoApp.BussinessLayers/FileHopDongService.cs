using System;
using System.Collections.Generic;
using DemoApp.DataLayers;
using DemoApp.Entities;

namespace DemoApp.BussinessLayers
{
    public static class FileHopDongService
    {
        private static IFileHopDongDAL _FileHopDongDAL;

        public static void Initialize(TypeOfDatabase dbType, string connectionString)
        {
            switch (dbType)
            {
                case TypeOfDatabase.SqlServer:
                    _FileHopDongDAL = new DataLayers.SqlServer.FileHopDongDAL(connectionString);
                    break;
                    // Thêm các trường hợp cho các loại cơ sở dữ liệu khác nếu cần
            }
        }

        public static IList<FileHopDong> GetFileHopDongByMaHD(int maHD)
        {
            return _FileHopDongDAL.GetByMaHD(maHD);
        }
        public static int FileHopDong_Insert(FileHopDong obj)
        {
            return _FileHopDongDAL.Insert(obj);
        }
        public static FileHopDong GetByMaFile(int maFile)
        {
            return _FileHopDongDAL.GetByMaFile(maFile);
        }
        public static int DeleteFileHopDong(FileHopDong fileHopDong)
        {
            return _FileHopDongDAL.Delete(fileHopDong);
        }
    }
}
