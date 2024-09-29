using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoApp.BussinessLayers;
using DemoApp.Entities;
using DemoApp.Web.Models;

namespace DemoApp.Web.Controllers
{
    public class ChamCongController : BaseController
    {
        public ActionResult List()
        {
            var chamCongs = ChamCongService.ListChamCong();
            return View(chamCongs);
        }
        public ActionResult Index()
        {
            string userRole = Session["UserRole"]?.ToString();
            if (userRole == "Admin")
            {
                var chamCongs = ChamCongService.ListChamCong();
                return View(chamCongs);
            }
            else if (userRole == "User")
            {
                int maNV = Convert.ToInt32(Session["MaNV"]);
                var chamCongNhanVien = ChamCongService.ListByMaNV(maNV);
                return View(chamCongNhanVien);
            }
            return View();

        }
        // GET: ChamCong
        public ActionResult TotalHoursWorkViewUser()
        {
            int maNV = Convert.ToInt32(Session["MaNV"]);
            var totalHoursWorkViewUser = ChamCongService.TongHopChamCong(maNV);
            return View(totalHoursWorkViewUser);
        }
        // GET: ChamCong/TotalHoursWorkViewAdmin
        public ActionResult TotalHoursWorkViewAdmin()
        {

            // Lấy danh sách nghỉ phép từ Service
            var totalHoursWorkViewAdmin = ChamCongService.TongHopChamCongTheoThang(12, 2023);
            var totalNghiPhep = NghiPhepService.ListNghiPhep();
            var listCaLam = CaLamService.ListCaLam();
            var model = new TongHopChamCongModel
            {
                TongHopChamCongTheoThang = totalHoursWorkViewAdmin,
                ListNghiPhep = totalNghiPhep,
                ListCaLam = listCaLam
                //
            };
            //ViewBag.DanhSachNghiPhep = NghiPhepService.ListNghiPhep();
            return View(model);
        }

        public ActionResult TotalHoursByMonth(string month)
        {
            int maNV = Convert.ToInt32(Session["MaNV"]);
            // Xử lý chuỗi 'month' để lấy giá trị tháng và năm
            int selectedMonth = int.Parse(month.Split('-')[1]); // Lấy giá trị tháng từ chuỗi 'month'
            int selectedYear = int.Parse(month.Split('-')[0]); // Lấy giá trị năm từ chuỗi 'month'

            // Gọi hàm TongHopChamCongTheoThang với tham số tháng và năm
            var totalHoursWorkView = ChamCongService.TongHopChamCongTheoThang(selectedMonth, selectedYear);
            var totalNghiPhep = NghiPhepService.ListNghiPhep();
            var listCaLam = CaLamService.ListCaLam();
            var model = new TongHopChamCongModel
            {
                TongHopChamCongTheoThang = totalHoursWorkView,
                ListNghiPhep = totalNghiPhep,
                ListCaLam = listCaLam
                //
            };
            return View("TotalHoursWorkViewAdmin", model);
        }

        public ActionResult Checkin()
        {
            int maNV = (int)Session["MaNV"];
            ViewBag.MaNV = maNV;
            return View();
        }

        [HttpPost]
        public ActionResult KiemTraCaLam(int maNV, DateTime? ngay)
        {
            if (ngay.HasValue)
            {
                int maCaLam = CaLamService.LayMaCaLam(maNV, ngay.Value);
                if (maCaLam > 0)
                {
                    TimeSpan thoiGianBatDau = DateTime.Now.TimeOfDay;
                    TimeSpan thoiGianKetThuc = DateTime.Now.TimeOfDay;

                    // Kiểm tra nếu đã chấm công vào ca thì cập nhật thời gian kết thúc
                    bool daChamCong = ChamCongService.KiemTraDaChamCong(maCaLam);
                    if (daChamCong)
                    {
                        thoiGianKetThuc = DateTime.Now.TimeOfDay;
                        int result = ChamCongService.UpdateThoiGianKetThucChamCong(maCaLam, thoiGianKetThuc);

                        return Json(new { success = "Bạn đã Checkout thành công!!!" });
                    }
                    else
                    {
                        ChamCong chamCong = new ChamCong
                        {
                            MaCaLam = maCaLam,
                            ThoiGianBatDau = thoiGianBatDau,
                            ThoiGianKetThuc = thoiGianKetThuc
                        };

                        int result = ChamCongService.InsertChamCong(chamCong);
                        if (result > 0)
                        {
                            return Json(new { success = "Bạn đã Checkin thành công!!!" });
                        }
                        else
                        {
                            return Json(new { error = "Lỗi khi chấm công!" });
                        }
                    }
                }
                else
                {
                    return Json(new { error = "Không có ca làm vào ngày này!" });
                }
            }
            else
            {
                return Json(new { error = "Tham số ngày không hợp lệ" });
            }
        }



    }
}