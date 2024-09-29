using DemoApp.BussinessLayers;
using DemoApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace DemoApp.Web.Controllers
{
    
    public class NhanVienController : BaseController
    {

       public ActionResult Index(int page = 1)
        {
            int pageSize = 5; // Kích thước trang là 5
            var totalRecords = NhanVienService.GetTotalNumberOfNhanViens(); // Số lượng tổng cộng của nhân viên

            // Tính số trang và kiểm tra nếu trang hiện tại lớn hơn số trang tối đa
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (page < 1)
            {
                page = 1;
            }
            else if (page > totalPages)
            {
                page = totalPages;
            }

            // Lấy danh sách nhân viên cho trang hiện tại
            var nhanViens = NhanVienService.NhanVien_ListPaged(page, pageSize);

            // Truyền danh sách phòng ban vào ViewBag
            ViewBag.DanhSachPhongBan = PhongBanService.PhongBan_List();
            ViewBag.DanhSachChucVu = ChucVuService.ListChucVu();
            ViewBag.DanhSachViTri = ViTriService.ListViTri();


            // Truyền thông tin phân trang vào ViewBag
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.ThisPage = page; // Gán giá trị trang hiện tại vào ViewBag
            return View(nhanViens);
        }


        [HttpGet]
        public ActionResult Create()
        {
            var obj = new NhanVien();
            // Lấy danh sách phòng ban và đặt nó vào ViewBag
            ViewBag.DanhSachPhongBan = PhongBanService.PhongBan_List().Where(pb => pb.MaPBParent != 0);
            ViewBag.DanhSachChucVu = ChucVuService.ListChucVu();
            return View(obj);
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, NhanVien model)
        {
            if (string.IsNullOrWhiteSpace(model.HoTen) || string.IsNullOrWhiteSpace(model.QueQuan))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin!");
                return View();
            }

            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/img"), fileName);

                    // Lưu file ảnh vào thư mục "img"
                    file.SaveAs(path);

                    // Lưu tên file ảnh vào model
                    model.HinhAnh = fileName;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi tải ảnh lên: " + ex.Message);
                    return View();
                }
            }

            int id = NhanVienService.NhanVien_Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            NhanVien nhanVienToUpdate = NhanVienService.NhanVien_Get(id);
            // Lấy danh sách phòng ban và đặt nó vào ViewBag
            ViewBag.DanhSachPhongBan = PhongBanService.PhongBan_List().Where(pb => pb.MaPBParent != 0);
            ViewBag.DanhSachChucVu = ChucVuService.ListChucVu();
            if (nhanVienToUpdate == null)
            {
                return HttpNotFound();
            }

            return View(nhanVienToUpdate);
        }

        [HttpPost]
        public ActionResult Update(int id, NhanVien model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                model.MaNV = id;

                // Xử lý tải lên hình ảnh mới
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/img"), fileName);
                    file.SaveAs(path);

                    // Cập nhật tên hình ảnh mới trong model
                    model.HinhAnh = fileName;
                }
                else
                {
                    // Nếu không có tệp ảnh mới, giữ nguyên tên ảnh cũ
                    model.HinhAnh = NhanVienService.GetHinhAnhById(id); // Thay thế bằng phương thức lấy tên ảnh cũ
                }

                int rowsAffected = NhanVienService.NhanVien_Update(model);
                if (rowsAffected > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể cập nhật thông tin nhân viên này.");
                }
            }
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]  //xác thực token ngăn chặn tấn công CSRF (Cross-Site Request Forgery).
        public JsonResult DeleteConfirmed(int id)
        {
            NhanVien nhanVienToDelete = new NhanVien { MaNV = id };
            int rowsAffected = NhanVienService.NhanVien_Delete(nhanVienToDelete);

            if (rowsAffected > 0)
            {
                return Json(new { success = true, message = "Xoá phòng ban và các phòng ban con thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Không thể xoá phòng ban và các phòng ban con." });
            }
        }
        public ActionResult Detail(int id, int? currentPage)
        {
            // Lấy thông tin chi tiết của nhân viên
            NhanVien nhanVien = NhanVienService.NhanVien_Get(id);

            // Lấy danh sách quá trình làm việc của nhân viên
            IList<QuaTrinh> quaTrinhList = QuaTrinhService.GetQuaTrinhByMaNV(id);
            nhanVien.QuaTrinhLamViec = new List<QuaTrinh>(quaTrinhList);

            // Lấy danh sách phòng ban và đặt nó vào ViewBag
            ViewBag.DanhSachPhongBan = PhongBanService.PhongBan_List().Where(pb => pb.MaPBParent != 0);
            ViewBag.DanhSachChucVu = ChucVuService.ListChucVu();
            ViewBag.DanhSachViTri= ViTriService.ListViTri();
            ViewBag.DanhSachLoaiHD = LoaiHopDongService.ListLoaiHD();
            ViewBag.ThisPage = currentPage;
            return View(nhanVien);
        }
        //public ActionResult GetTableDataQuaTrinh(int maNhanVien)
        //{
        //    var quaTrinhs = QuaTrinhService.GetQuaTrinhByMaNV(maNhanVien); // Lấy dữ liệu từ CSDL dựa trên mã nhân viên
        //    ViewBag.DanhSachPhongBan = PhongBanService.PhongBan_List();
        //    ViewBag.DanhSachChucVu = ChucVuService.ListChucVu();
        //    ViewBag.DanhSachViTri = ViTriService.ListViTri();

        //    if (quaTrinhs != null && quaTrinhs.Any())
        //    {
        //        return PartialView("_HienThiQTNVTable", quaTrinhs);
        //    }
        //    else
        //    {
        //        ViewBag.QuaTrinhMessage = "Không có dữ liệu.";
        //        return PartialView("_HienThiQTNVTable");
        //    }
        //}




        public ActionResult SearchAndFilterPaged(string searchTerm, string gender, int maPB , int page, int pageSize)
        {
            var totalRecords = NhanVienService.GetTotalNumberOfFilteredNhanViens(searchTerm, gender, maPB);


            int rowCount = 0;
            var nhanViens = NhanVienService.SearchAndFilterPaged(searchTerm, gender, maPB, page, pageSize, out rowCount);
            int totalPages = (int)Math.Ceiling((double)rowCount / pageSize);
            if (page < 1)
            {
                page = 1;
            }
            else if (page > totalPages)
            {
                page = totalPages;
            }

            ViewBag.DanhSachPhongBan = PhongBanService.PhongBan_List();
            ViewBag.DanhSachChucVu = ChucVuService.ListChucVu();
            ViewBag.DanhSachViTri = ViTriService.ListViTri();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.ThisPage = page;

            return PartialView("_NhanVienTable", nhanViens);
        }


    }
}
