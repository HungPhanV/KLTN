using DemoApp.Entities;
using System.Collections.Generic;

namespace DemoApp.DataLayers
{
    public interface IPhongBanDAL
    {
        IList<PhongBan> List();
        IList<PhongBan> GetPhongBansByMaPBParent(int maPBParent);
        PhongBan Get(int MaPB);
        int Insert(PhongBan nv);
        int Update(PhongBan nv);
        int Delete(PhongBan nv);
        IList<PhongBan> SearchPhongBanByName(string tenPB);
        //IList<Entities.PhongBan> ListPagedPhongBan(int pageNumber, int pageSize);
        int GetTotalNumberOfPhongBans();
        //int GetTotalNumberOfFilteredPhongBans(string departmentName);
        //IList<PhongBan> SearchAndFilterPagedPhongBans(string searchTerm, int pageNumber, int pageSize);

    }
}
