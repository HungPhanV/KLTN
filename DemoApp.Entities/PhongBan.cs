using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Entities
{
    public class PhongBan
    {
        public int MaPB { get; set; }
        public string TenPB { get; set; }
        public string DiaChi { get; set; }
        public string SDTPB { get; set; }
        public int? MaPBParent { get; set; }
    }
}
