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
    public class HopDongController : BaseController
    {
        

        public ActionResult DetailHopDong(int id)
        {
            // Lấy chi tiết hợp đồng từ cơ sở dữ liệu bằng mã hợp đồng
            var hopDong = HopDongLaoDongService.GetHopDongByMaHD(id);
            ViewBag.DanhSachLoaiHD = LoaiHopDongService.ListLoaiHD();
            ViewBag.DanhSachNhanVien = NhanVienService.NhanVien_List();
            if (hopDong == null)
            {
                // Xử lý trường hợp không tìm thấy hợp đồng
                return HttpNotFound();
            }

            return View(hopDong);
        }
        public PartialViewResult FileHopDongList(int maHopDong)
        {
            var fileHopDongList = FileHopDongService.GetFileHopDongByMaHD(maHopDong);
                return PartialView("_FileHopDongList", fileHopDongList);
      
        }

        public ActionResult GetTableData(int maNhanVien)
        {
            var hopDongs = HopDongLaoDongService.GetByMaNV(maNhanVien); // Lấy dữ liệu từ CSDL dựa trên mã nhân viên
            ViewBag.DanhSachNhanVien = NhanVienService.NhanVien_List();
            ViewBag.DanhSachLoaiHD = LoaiHopDongService.ListLoaiHD();
            ViewBag.HopDongData = hopDongs; // hopDongs là danh sách các hợp đồng

            if (hopDongs != null && hopDongs.Any())
            {
                return PartialView("_HopDongTable", hopDongs);
            }
            else
            {
            
                return PartialView("_HopDongTable");
            }

        }


        // GET: HopDong
        public ActionResult Index()
        {
            // Retrieve a list of HopDong records from the database
            IList<Entities.HopDongLaoDong> hopDongList = HopDongLaoDongService.GetAllHopDongLaoDong();
            ViewBag.DanhSachNhanVien = NhanVienService.NhanVien_List();
            return View(hopDongList);
        }


        [HttpPost]
        public ActionResult Create(HopDongLaoDong model)
        {

            int id = HopDongLaoDongService.InsertHopDongLaoDong(model);
            // Sau khi thêm thành công, lấy danh sách phòng ban và trả về dưới dạng JSON
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult CreateFileHopDong(HttpPostedFileBase LuuTru, FileHopDong model)
        {

            if (LuuTru != null && LuuTru.ContentLength > 0)
            {
                try
                {
                    string fileName = Path.GetFileName(LuuTru.FileName);
                    string path = Path.Combine(Server.MapPath("~/FileHopDong"), fileName);

                    // Lưu file ảnh vào thư mục "img"
                    LuuTru.SaveAs(path);

                    // Lưu tên file ảnh vào model
                    model.LuuTru = fileName;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi tải ảnh lên: " + ex.Message);
                    return Json(new { success = false });
                }
            }

            int id = FileHopDongService.FileHopDong_Insert(model);
            return Json(new { success = true });
        }

        public FileResult DownloadFile(int maFile)
        {
            // Gọi hàm trong DAL để lấy thông tin về file hợp đồng dựa trên MaFile
            FileHopDong fileHopDong = FileHopDongService.GetByMaFile(maFile);

            if (fileHopDong != null)
            {
                // Đường dẫn đầy đủ đến tệp trong thư mục 'FileHopDong'
                string filePath = Path.Combine(Server.MapPath("~/FileHopDong"), fileHopDong.LuuTru);

                // Tạo một FileStreamResult để trả về tệp
                return File(filePath, "application/octet-stream", fileHopDong.LuuTru);
            }
            else
            {
                // Trả về một lỗi hoặc trang 404 nếu không tìm thấy file
                // Tạo một FileStreamResult để trả về tệp
                return File("application/octet-stream", fileHopDong.TenFile);
            }
        }


        [HttpPost]
        public ActionResult Update(int maHD, int maNV, int maLoaiHD, DateTime tuNgay, DateTime? denNgay)
        {
            try
            {
                // Tạo một đối tượng HopDongLaoDong mới để lưu thông tin cập nhật
                HopDongLaoDong model = new HopDongLaoDong
                {
                    MaHD = maHD, // Gán MaHD từ tham số truyền vào
                    MaNV = maNV,
                    MaLoaiHD = maLoaiHD,
                    TuNgay = tuNgay,
                    DenNgay = denNgay
                };

                // Gọi hàm cập nhật từ HopDongLaoDongService và lưu kết quả vào biến rowsAffected
                int rowsAffected = HopDongLaoDongService.UpdateHopDongLaoDong(model);

                if (rowsAffected > 0)
                {
                    // Trả về kết quả thành công dưới dạng JSON
                    return Json(new { success = true });
                }
                else
                {
                    // Trả về kết quả thất bại dưới dạng JSON nếu không thể cập nhật hợp đồng
                    return Json(new { success = false, message = "Không thể cập nhật hợp đồng." });
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có và trả về thông báo lỗi dưới dạng JSON
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //xác thực token ngăn chặn tấn công CSRF (Cross-Site Request Forgery).
        public JsonResult DeleteConfirmed(int id)
        {
            HopDongLaoDong hopDongToDelete = new HopDongLaoDong { MaHD = id };
            int rowsAffected = HopDongLaoDongService.DeleteHopDongLaoDong(hopDongToDelete);

            if (rowsAffected > 0)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]  //xác thực token ngăn chặn tấn công CSRF (Cross-Site Request Forgery).
        public JsonResult DeleteConfirmedFileHopDong(int id)
        {
            FileHopDong fileHopDongToDelete = new FileHopDong { MaFile = id };
            int rowsAffected = FileHopDongService.DeleteFileHopDong(fileHopDongToDelete);

            if (rowsAffected > 0)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }
}
