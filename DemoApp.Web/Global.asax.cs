using DemoApp.BussinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace DemoApp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string connectionString = "server=Admin-PC;user id=sa;password=123456;database=qlns";

            Type[] serviceTypes = {  typeof(NhanVienService), typeof(TaiKhoanService), typeof(PhongBanService), typeof(QuaTrinhService), typeof(ChucVuService), typeof(HopDongLaoDongService), typeof(ViTriService) ,typeof(LoaiHopDongService), typeof(FileHopDongService), typeof(CaLamService), typeof(LoaiCaLamService), typeof(ChamCongService), typeof(NghiPhepService), typeof(LoaiNghiPhepService) };

            foreach (Type serviceType in serviceTypes)
            {
                Type dalType = Type.GetType($"DemoApp.DataLayers.SqlServer.{serviceType.Name}DAL");
                var initializeMethod = serviceType.GetMethod("Initialize");
                initializeMethod.Invoke(null, new object[] { TypeOfDatabase.SqlServer, connectionString });
            }

        }
    }
}
