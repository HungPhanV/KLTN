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
    }
}
