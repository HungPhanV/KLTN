using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DemoApp.Entities
{
    public class NhanVien
    {
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; } // Thêm thuộc tính NgaySinh
        public string QueQuan { get; set; }
        public string GioiTinh { get; set; }
        public string SDT { get; set; }
        public string HinhAnh { get; set; }
        public int MaPB { get; set; }
        public int MaVT { get; set; }
        public int MaCV { get; set; }
        public List<QuaTrinh> QuaTrinhLamViec { get; set; }
    }
}
