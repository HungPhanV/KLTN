using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoApp.BussinessLayers;
using DemoApp.Entities;

namespace DemoApp.Web.Controllers
{
    public class QuaTrinhController : BaseController
    {
        // GET: QuaTrinh
        public ActionResult Index()
        {
            // Gọi phương thức để lấy danh sách Quá Trình từ service
            ViewBag.DanhSachPhongBan = PhongBanService.PhongBan_List();
            ViewBag.DanhSachNhanVien = NhanVienService.NhanVien_List();
            // Truyền danh sách này đến view
            return View();
        }
        public ActionResult GetTableData(int maNhanVien)
        {
            var quaTrinhs = QuaTrinhService.GetQuaTrinhByMaNV(maNhanVien); // Lấy dữ liệu từ CSDL dựa trên mã nhân viên
            ViewBag.DanhSachPhongBan = PhongBanService.PhongBan_List();
            ViewBag.DanhSachChucVu = ChucVuService.ListChucVu();
            ViewBag.DanhSachViTri = ViTriService.ListViTri();

            if (quaTrinhs != null && quaTrinhs.Any())
            {
                return PartialView("_QuaTrinhTable", quaTrinhs);
            }
            else
            {
                ViewBag.QuaTrinhMessage = "Không có dữ liệu.";
                return PartialView("_QuaTrinhTable");
            }
        }




        [HttpPost]
        public ActionResult Create(QuaTrinh model)
        {

            int id = QuaTrinhService.InsertQuaTrinh(model);
            // Sau khi thêm thành công, lấy danh sách phòng ban và trả về dưới dạng JSON
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult Update(int quaTrinhID, int maNV, int maPBQuaTrinh,int maCVQuaTrinh, int maVTQuaTrinh,  DateTime thoiGianBatDau, DateTime? thoiGianKetThuc, string trangThai)
        {
            try
            {
                // Tạo một đối tượng QuaTrinh mới để lưu thông tin cập nhật
                QuaTrinh model = new QuaTrinh
                {
                    ID = quaTrinhID, // Gán ID từ tham số truyền vào
                    MaNV = maNV,
                    MaPB = maPBQuaTrinh,
                    MaCV = maCVQuaTrinh,
                    MaVT = maVTQuaTrinh,
                    ThoiGianBatDau = thoiGianBatDau,
                    ThoiGianKetThuc = thoiGianKetThuc,
                    TrangThai = trangThai
                };
                //model.ThoiGianKetThuc = thoiGianKetThuc != null ? thoiGianKetThuc : null;

                // Gọi hàm cập nhật từ QuaTrinhService và lưu kết quả vào biến rowsAffected
                int rowsAffected = QuaTrinhService.UpdateQuaTrinh(model);

                if (rowsAffected > 0)
                {
                    // Trả về kết quả thành công dưới dạng JSON
                    return Json(new { success = true });
                }
                else
                {
                    // Trả về kết quả thất bại dưới dạng JSON
                    return Json(new { success = false, message = "Không thể cập nhật thông tin Quá Trình này." });
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có và trả về thông báo lỗi dưới dạng JSON
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //xác thực token ngăn chặn tấn công CSRF (Cross-Site Request Forgery).
        public JsonResult DeleteConfirmed(int id)
        {
            QuaTrinh quaTrinhToDelete = new QuaTrinh { ID = id };
            int rowsAffected = QuaTrinhService.DeleteQuaTrinh(quaTrinhToDelete);

            if (rowsAffected > 0)
            {
                return Json(new { success = true, message = "Xoá phòng ban và các phòng ban con thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Không thể xoá phòng ban và các phòng ban con." });
            }
        }
    }


}

