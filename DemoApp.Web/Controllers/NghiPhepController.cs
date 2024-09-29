using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoApp.BussinessLayers;
using DemoApp.Entities;
using System.IO;
namespace DemoApp.Web.Controllers
{
    public class NghiPhepController : BaseController
    {
        // GET: NghiPhep
        public ActionResult Index()
        {
            return View();
        }
        // GET: NghiPhep
        public ActionResult Create()
        {
            ViewBag.DanhSachLoaiNghiPhep = LoaiNghiPhepService.ListLoaiNghiPhep();
            return View();
        }
        [HttpPost]
        public ActionResult Create(NghiPhep model)
        {
            if (ModelState.IsValid)
            {
                // Lấy mã nhân viên từ Session
                int maNV = Convert.ToInt32(Session["MaNV"]);

                // Gán mã nhân viên và trạng thái mặc định
                model.MaNV = maNV;
                model.TrangThai = "Đang chờ";

                // Thực hiện lưu đơn nghỉ phép vào CSDL
                // Việc này tùy thuộc vào cách bạn đã thiết kế DataAccess hoặc Service layer

                NghiPhepService.InsertNghiPhep(model);

                // Redirect hoặc thực hiện các thao tác khác sau khi lưu thành công
                return RedirectToAction("Index", "Home");
            }
            
            // Nếu ModelState không hợp lệ, quay trở lại view Create để hiển thị lỗi
            return View(model);
        }
        // GET: NghiPhep
        public ActionResult List()

        {
            ViewBag.DanhSachLoaiNghiPhep = LoaiNghiPhepService.ListLoaiNghiPhep();
            ViewBag.DanhSachNhanVien = NhanVienService.NhanVien_List();
            var nghiPheps = NghiPhepService.ListNghiPhep();
            return View(nghiPheps);
        }
        [HttpPost]
        public ActionResult UpdateTrangThai(int id, string trangThai)
        {
            // Gọi hàm cập nhật trạng thái từ DAL hoặc Service của bạn
            // Ví dụ:
            NghiPhepService.UpdateTrangThai(id, trangThai);
            return Json(new { success = true });
        }

    }
}