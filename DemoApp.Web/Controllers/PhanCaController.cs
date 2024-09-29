using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoApp.BussinessLayers;
using DemoApp.Entities;
namespace DemoApp.Web.Controllers
{
    public class PhanCaController : BaseController
    {

        public ActionResult List()
        {
            var caLams = CaLamService.ListCaLam();
            ViewBag.DanhSachNhanVien = NhanVienService.NhanVien_List();
            return View(caLams);
        }

        public ActionResult Index()
        {
            string userRole = Session["UserRole"]?.ToString();
            if(userRole == "Admin")
            {
                var caLams = CaLamService.ListCaLam();
                return View(caLams);
            }
            else if (userRole == "User")
            {
                int maNV = Convert.ToInt32(Session["MaNV"]);
                var caLamNhanVien = CaLamService.ListByMaNV(maNV);
                return View(caLamNhanVien);
            }
            return View();

        }

        [HttpPost]
        public ActionResult InsertCaLam(List<Entities.CaLam> chamCongData)
        {
            foreach (var item in chamCongData)
            {
                var maNV = item.MaNV;
                var ngayBatDau = item.NgayBatDau;
                var ngayKetThuc = item.NgayKetThuc;
                var maLoai = item.MaLoai; // Lấy mã loại từ dữ liệu gửi từ view

                // Kiểm tra nếu MaLoai không phải là null hoặc giá trị mặc định
                if (maLoai != null && maLoai != default(int))
                {
                    var dateRange = Enumerable.Range(0, 1 + ngayKetThuc.Subtract(ngayBatDau).Days)
                                .Select(offset => ngayBatDau.AddDays(offset));

                    foreach (var date in dateRange)
                    {
                        var caLam = new CaLam
                        {
                            MaNV = maNV,
                            NgayBatDau = date,
                            NgayKetThuc = date,
                            MaCaLam = maLoai, // Chỉ đặt mã loại một lần, giả sử cùng một loại trong khoảng thời gian
                            MaLoai = maLoai // Gán mã loại cho đối tượng caLam
                        };

                        CaLamService.InsertCaLam(caLam); // Thêm vào cơ sở dữ liệu
                    }
                }
                else
                {
                    // Xử lý khi MaLoai là null hoặc giá trị mặc định
                    // Có thể thông báo lỗi hoặc thực hiện các hành động phù hợp
                    // Ví dụ: return Json(new { success = false, message = "MaLoai không được để trống." });
                }
            }

            return Json(new { success = true, message = "Chấm công đã được lưu." });
        }


        public ActionResult Create(int page = 1)
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
            ViewBag.DachSachCaLam = LoaiCaLamService.ListCaLam();
            ViewBag.DachSachNhanVien = NhanVienService.NhanVien_List();
            // Truyền thông tin phân trang vào ViewBag
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.ThisPage = page; // Gán giá trị trang hiện tại vào ViewBag
            return View(nhanViens);
        }



        public ActionResult SearchAndFilterPaged(string searchTerm, string gender, int maPB, int page, int pageSize)
        {
            var totalRecords = NhanVienService.GetTotalNumberOfFilteredNhanViens(searchTerm, gender, maPB);

            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (page < 1)
            {
                page = 1;
            }
            else if (page > totalPages)
            {
                page = totalPages;
            }
            int rowCount = 0;
            var nhanViens = NhanVienService.SearchAndFilterPaged(searchTerm, gender, maPB, page, pageSize, out rowCount);

            ViewBag.DanhSachPhongBan = PhongBanService.PhongBan_List();
            ViewBag.DanhSachChucVu = ChucVuService.ListChucVu();
            ViewBag.DachSachCaLam = LoaiCaLamService.ListCaLam();
            ViewBag.DachSachNhanVien = NhanVienService.NhanVien_List();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.ThisPage = page;

            return PartialView("_PhanCaNVTable", nhanViens);
        }
     
    }
}