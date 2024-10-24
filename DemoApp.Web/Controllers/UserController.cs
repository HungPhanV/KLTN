using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoApp.BussinessLayers;
using DemoApp.Entities;
using DemoApp.Web;
using Newtonsoft.Json;
using System.Web.Security;


namespace DemoApp.Web.Controllers
{
    public class UserController : Controller
    {
        private const string ORDER_SEARCH = "SearchOrderCondition";
        private const string MESSAGE = "Message";
        private const int PAGE_SIZE = 10;
       
        // GET: User
        public ActionResult Index()
        {
            var taiKhoans = TaiKhoanService.List();
            ViewBag.DanhSachNhanVien = NhanVienService.NhanVien_List();
            return View(taiKhoans);
        }

        public ActionResult RegisterAI()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetCurrentUserInfo()
        {
            // Lấy thông tin người dùng từ session
            var loggedInUserJson = Session["LoggedInUserJson"];
            var userId = Session["UserID"];
            if (loggedInUserJson != null)
            {
                var loggedInUser = JsonConvert.DeserializeObject<TaiKhoan>(loggedInUserJson.ToString());
                return Json(new
                {
                    status = true,
                    userName = loggedInUser.Username,
                    userId = userId,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = false, message = "Người dùng chưa đăng nhập!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCurrentUser()
        {
            // Lấy thông tin người dùng từ session
            var loggedInUserJson = Session["LoggedInUserJson"];

            if (loggedInUserJson != null)
            {
                var loggedInUser = JsonConvert.DeserializeObject<TaiKhoan>(loggedInUserJson.ToString());
                return Json(new { userName = loggedInUser.Username, status = "success" }); // Thêm status
            }
            else
            {
                return Json(new { status = "error", message = "Không tìm thấy thông tin người dùng." });
            }
        }

        [HttpGet]
        [AllowAnonymous] //Có thể truy cập mà không bị yêu cầu xác thực trước,
        public ActionResult Login()
        {
            var cookie = Converter.CookieToUserAccount(User.Identity.Name);
            if (cookie != null)
                return RedirectToAction("Index", "NhanVien");

            ViewBag.Message = TempData[MESSAGE] ?? "";
          

            // Kiểm tra Session để xem người dùng đã đăng nhập hay chưa
            string loggedInUserJson = Session["LoggedInUserJson"] as string;
            if (!string.IsNullOrEmpty(loggedInUserJson))
            {
                // Đã đăng nhập, điều hướng đến trang chính
                return RedirectToAction("Index", "NhanVien");
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userName, string password)
        {
            TaiKhoan loggedInUser = TaiKhoanService.Authorize(userName, password);

            if (loggedInUser != null)
            {
                string loggedInUserJson = JsonConvert.SerializeObject(loggedInUser);
                Session["LoggedInUserJson"] = loggedInUserJson;

                // Lưu thêm ID của người dùng vào session
                Session["UserID"] = loggedInUser.id;
                Session["MaNV"] = loggedInUser.MaNV;
                // Lưu vai trò vào Session
                Session["UserRole"] = loggedInUser.Role;

                ViewBag.SuccessMessage = "Đăng nhập thành công!";

                return RedirectToAction("ThongKe", "Home"); // Điều hướng đến trang chính
            }
            else
            {
                ViewBag.FailMessage = "Đăng nhập thất bại!";
                ViewBag.ErrorMessage = "Invalid credentials. Please try again.";
                return View();
            }
        }



        public ActionResult Logout()
        {
            var userAccount = Converter.CookieToUserAccount(User.Identity.Name);
            Session.Clear();
            
            if (userAccount == null)
            {
                return RedirectToAction("Login");
            }
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult FaceLogin(int id)
        {
            // Giả sử bạn có một phương thức để lấy tài khoản dựa trên username
            TaiKhoan user = TaiKhoanService.GetTaiKhoanByID(id);
            var userName = user.Username;
            var passWord = user.Password;
            var userLogin = TaiKhoanService.Authorize(userName, passWord);  // Chúng ta không cần mật khẩu cho đăng nhập bằng khuôn mặt

            if (user != null)
            {
                // Lưu thông tin đăng nhập vào Session
                string loggedInUserJson = JsonConvert.SerializeObject(user);
                Session["LoggedInUserJson"] = loggedInUserJson;

                // Lưu ID và vai trò của người dùng vào Session
                Session["UserID"] = user.id;
                Session["UserRole"] = user.Role;

                // Trả về phản hồi đăng nhập thành công
                return Json(new { status = true, message = "Đăng nhập bằng khuôn mặt thành công!" });
            }
            else
            {
                // Trả về phản hồi khi không tìm thấy tài khoản
                return Json(new { status = false, message = "Không tìm thấy tài khoản!" });
            }
        }

    }
}
