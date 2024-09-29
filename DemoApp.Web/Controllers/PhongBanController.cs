using DemoApp.BussinessLayers;
using DemoApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO; // Import namespace System.IO để sử dụng StringWriter
namespace DemoApp.Web.Controllers
{
    public class PhongBanController : BaseController
    {
      
        public ActionResult Search(string searchTerm)
        {
            ViewBag.DanhSachPhongBan = PhongBanService.PhongBan_List().Where(pb => pb.MaPBParent == 0);
            try
            {
                var phongBans = PhongBanService.SearchPhongBanByName(searchTerm);
                return PartialView("_PhongBanTable", phongBans);
            }
            catch (Exception ex)
            {
                // Handle errors here, e.g., log the exception
                return Content("Error: " + ex.Message);
            }
        }


        // GET: PhongBan
        public ActionResult Index(int page = 1)
        {
            //int pageSize = 3; // Số lượng nhân viên trên mỗi trang
            //var phongBans = PhongBanService.PhongBan_List(); // Lấy dữ liệu từ CSDL
            //ViewBag.DanhSachPhongBan = PhongBanService.PhongBan_List();
            //ViewBag.DanhSachChucVu = ChucVuService.ListChucVu();

            //// Phân trang dữ liệu
            //var pagePhongBans = phongBans.Skip((page - 1) * pageSize).Take(pageSize);

            //// Tính tổng số trang
            //int totalPhongBan = phongBans.Count();
            //int totalPages = (int)Math.Ceiling((double)totalPhongBan / pageSize);

            //ViewBag.CurrentPage = page;
            //ViewBag.TotalPages = totalPages;

            return View();
        }

        [HttpPost]
        public ActionResult Create(PhongBan model)
        {
            if (string.IsNullOrWhiteSpace(model.TenPB) || string.IsNullOrWhiteSpace(model.DiaChi))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin!");
                return Json(new { success = false, message = "Vui lòng nhập đầy đủ thông tin!" });
            }
            else
            {
                int id = PhongBanService.PhongBan_Insert(model);
                if (id > 0)
                {
                    // Sau khi thêm thành công, lấy danh sách phòng ban và trả về dưới dạng JSON
                    var phongBanList = PhongBanService.PhongBan_List(); // Điều này tùy thuộc vào dịch vụ của bạn
                    return Json(new { success = true, data = phongBanList });
                }
                else
                {
                    return Json(new { success = false, message = "Không thể tạo phòng ban mới." });
                }
            }
        }




        [HttpPost]
        public ActionResult Update(int id, PhongBan model)
        {
            if (ModelState.IsValid)
            {
                model.MaPB = id;
                int rowsAffected = PhongBanService.PhongBan_Update(model);
                if (rowsAffected > 0)
                {
                    // Trả về kết quả thành công dưới dạng JSON
                    return Json(new { success = true });
                }
                else
                {
                    // Trả về kết quả thất bại dưới dạng JSON
                    return Json(new { success = false, message = "Không thể cập nhật thông tin phòng ban này." });
                }
            }
            // Trả về kết quả thất bại nếu ModelState không hợp lệ
            return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            int rowsAffected = PhongBanService.PhongBan_DeleteWithChildren(id);

            if (rowsAffected > 0)
            {
                return Json(new { success = true, message = "Xoá phòng ban và các phòng ban con thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Không thể xoá phòng ban và các phòng ban con." });
            }
        }

        //public ActionResult GetTableData()
        //{
        //    var phongBans = PhongBanService.PhongBan_List(); // Lấy dữ liệu từ CSDL dựa trên mã nhân viên
        //    ViewBag.DanhSachPhongBan = PhongBanService.PhongBan_List();
        //    return PartialView("_PhongBanTable", phongBans);
        //}


        //public ActionResult SearchAndFilterPaged(string searchTerm, int page = 1)
        //{
        //    int pageSize =20; // Kích thước trang là 6
        //    var totalRecords = PhongBanService.GetTotalNumberOfFilteredPhongBans(searchTerm); // Số lượng tổng cộng sau khi áp dụng điều kiện lọc

        //    // Tính số trang và kiểm tra nếu trang hiện tại lớn hơn số trang tối đa
        //    int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        //    if (page < 1)
        //    {
        //        page = 1;
        //    }
        //    else if (page > totalPages)
        //    {
        //        page = totalPages;
        //    }

        //    // Lấy danh sách nhân viên sau khi áp dụng điều kiện lọc và phân trang
        //    var phongBans = PhongBanService.SearchAndFilterPagedPhongBans(searchTerm, page, pageSize);

        //    // Truyền danh sách phòng ban và chức vụ vào ViewBag
        //    ViewBag.DanhSachPhongBan = PhongBanService.PhongBan_List().Where(pb => pb.MaPBParent == 0);
        //    ViewBag.DanhSachChucVu = ChucVuService.ListChucVu();

        //    // Truyền thông tin phân trang vào ViewBag
        //    ViewBag.CurrentPage = page;
        //    ViewBag.TotalPages = totalPages;
        //    ViewBag.ThisPage = page; // Gán giá trị trang hiện tại vào ViewBag
        //    return PartialView("_PhongBanTable", phongBans);
        //}

        //// Danh sách nhân viên của phòng ban
        //public ActionResult DanhSachNhanVien(int id)
        //{
        //    // Lọc danh sách nhân viên theo mã phòng ban
        //    var danhSachNhanVien = NhanVienService.NhanVien_List()
        //        .Where(nv => nv.QuaTrinhLamViec.Any(qt => qt.MaPB == id))
        //        .ToList();

        //    // Lấy danh sách phòng ban con
        //    var danhSachPhongBan = PhongBanService.PhongBan_List().Where(pb => pb.MaPBParent != 0);
        //    ViewBag.DanhSachPhongBan = danhSachPhongBan;

        //    // Lấy thông tin phòng ban
        //    var phongBan = PhongBanService.PhongBan_Get(id);

        //    if (phongBan == null)
        //    {
        //        // Xử lý nếu phòng ban không tồn tại
        //        return HttpNotFound();
        //    }
        //    ViewBag.DanhSachChucVu = ChucVuService.ListChucVu();
        //    ViewBag.PhongBan = phongBan;
        //    return View("ListNV", danhSachNhanVien);
        //}
        //[HttpPost]
        //// Chuyển nhân viên sang phòng ban khác
        //public ActionResult ChuyenNhanVien(int idNhanVien, int idPhongBanMoi)
        //{
        //    ViewBag.DanhSachPhongBan = PhongBanService.PhongBan_List();

        //    // Lấy thông tin nhân viên từ idNhanVien
        //    var nhanVien = NhanVienService.NhanVien_Get(idNhanVien);

        //    if (nhanVien == null)
        //    {
        //        // Xử lý nếu nhân viên không tồn tại
        //        return HttpNotFound();
        //    }

        //    // Lấy thông tin phòng ban mới từ idPhongBanMoi
        //    var phongBanMoi = PhongBanService.PhongBan_Get(idPhongBanMoi);

        //    if (phongBanMoi == null)
        //    {
        //        // Xử lý nếu phòng ban mới không tồn tại
        //        return HttpNotFound();
        //    }

        //    // Tạo một quá trình mới để thể hiện việc chuyển phòng ban
        //    var quaTrinhMoi = new QuaTrinh
        //    {
        //        MaNV = idNhanVien,
        //        MaPB = idPhongBanMoi,
        //        ThoiGianBatDau = DateTime.Now, // Thời gian bắt đầu mới
        //        ThoiGianKetThuc = null // Thời gian kết thúc sẽ được cập nhật sau
        //    };

        //    // Thêm quá trình mới vào cơ sở dữ liệu
        //    QuaTrinhService.InsertQuaTrinh(quaTrinhMoi);

        //    // Trả về danh sách nhân viên sau khi chuyển
        //    return RedirectToAction("DanhSachNhanVien", new { id = idPhongBanMoi });
        //}


    }
}

