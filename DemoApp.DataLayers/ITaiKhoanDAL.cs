using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Entities;
namespace DemoApp.DataLayers.SqlServer
{
    public interface ITaiKhoanDAL
    {     /// <summary>
          /// Kiểm tra tên đăng nhập và mật khẩu có hợp lệ hay không?
          /// Nếu hợp lệ thì trả về thông tin của tài khoản, ngược lại thì trả về null
          /// </summary>
          /// <param name="userName"></param>
          /// <param name="password"></param>
          /// <returns></returns>
        TaiKhoan Authorize(string userName, string password);
        IList<Entities.TaiKhoan> List();
        int Update(TaiKhoan tk);
        int DeleteByMaNV(int maNV);
        int UpdateRoleByMaNV(int maNV, string newRole);
    }
}
