using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApp.Web.Models
{
    public class TongHopChamCongModel
    {
        public IList<Entities.ChamCong> TongHopChamCongTheoThang { get; set; }
        public IList<Entities.NghiPhep> ListNghiPhep { get; set; }
        public IList<Entities.CaLam> ListCaLam { get; set; }
        ///
    }
}