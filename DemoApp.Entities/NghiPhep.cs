using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Entities
{
    public class NghiPhep
    {
        public int ID { get; set; }
        public int MaNV { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int MaLoaiNghiPhep { get; set; }
        public string LyDoBoSung { get; set; }
        public string TrangThai { get; set; }
    }
}
