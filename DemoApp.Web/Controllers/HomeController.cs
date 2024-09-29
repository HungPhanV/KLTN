using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoApp.BussinessLayers;
using DemoApp.Entities;


namespace DemoApp.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThongKe()
        {
            // Gọi dịch vụ hoặc logic thống kê dữ liệu ở đây
            int soLuongNhanVien = NhanVienService.GetTotalNumberOfNhanViens();
            int soLuongPhongBan = PhongBanService.GetTotalNumberOfPhongBans();
            int soLuongHopDong = HopDongLaoDongService.GetTotalHopDongs();
            int Nam = NhanVienService.CountNhanVienByGender("Nam");
            int Nu = NhanVienService.CountNhanVienByGender("Nữ");
            //long slPhongBan = PhongBanService.GetTotalNumberOfPhongBans();
            //ViewBag.SLPhongBan = slPhongBan;
            ViewBag.SLNV = soLuongNhanVien;
            ViewBag.SLPB = soLuongPhongBan;
            ViewBag.SLHD = soLuongHopDong;
            ViewBag.SLNVNam = Nam;
            ViewBag.SLNVNu = Nu;
            return View();
        }


       


    }
}