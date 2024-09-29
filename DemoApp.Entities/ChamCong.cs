using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DemoApp.Entities
{
    public class ChamCong
    {
        public int MaChamCong {  get; set; }
        public int MaCaLam { get; set; }
        public TimeSpan ThoiGianBatDau { get; set; }
        public TimeSpan ThoiGianKetThuc { get; set; }
        public string HoTen { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string TenCa { get; set; }
        public int SoGioLam { get; set; }
    }
}

