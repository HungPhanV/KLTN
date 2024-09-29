using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DemoApp.Entities;
namespace DemoApp.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // Kiểm tra nếu session không tồn tại (người dùng chưa đăng nhập) thì chuyển hướng về trang đăng nhập
            if (Session["LoggedInUserJson"] == null)
            {
                filterContext.Result = RedirectToAction("Login", "User");
            }
            else
            {
                // Lấy thông tin MaNV từ LoggedInUserJson hoặc thông tin session tương ứng khác để lưu vào session MaNV
                var loggedInUserJson = Session["LoggedInUserJson"] as string;
                if (!string.IsNullOrEmpty(loggedInUserJson))
                {
                    var loggedInUser = Newtonsoft.Json.JsonConvert.DeserializeObject<TaiKhoan>(loggedInUserJson);
                    int maNV = loggedInUser.MaNV; // Giả sử thông tin MaNV được lưu trong đối tượng User

                    Session["MaNV"] = maNV; // Lưu MaNV vào Session
                }
            }
        }
    }
}
