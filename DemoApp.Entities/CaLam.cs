using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Entities
{
    public class CaLam
    {
        public int? MaCaLam { get; set; }
        public int MaNV { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int? MaLoai { get; set; }
        public string HoTen { get; set; }
        public string TenCa { get; set; }
        public TimeSpan ThoiGianBatDau { get; set; }
        public TimeSpan ThoiGianKetThuc { get; set; }
    }
}
