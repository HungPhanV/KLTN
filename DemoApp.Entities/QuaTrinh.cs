using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Entities
{
    public class QuaTrinh
    {
        public int ID { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public string TrangThai { get; set; }
        public int? MaPB { get; set; }
        public int? MaNV { get; set; }
        public int? MaVT { get; set; }
        public int? MaCV { get; set; }
    }
}
