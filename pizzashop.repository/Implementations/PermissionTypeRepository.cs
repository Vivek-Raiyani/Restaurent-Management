using pizzashop.data.Models;
using pizzashop.repository.Interfaces;

namespace pizzashop.repository.Implementations;

public class PermissionTypeRepository : IPermissionTypeRepository
{
    private readonly PizzashopContext _db;
    public PermissionTypeRepository(PizzashopContext db)
    {
        _db = db;
    }

    #region  read permissions type 

    // returns name of permission type based on id
    public string GetPermissionType(int permissionTypeId)
    {
        PermissionType temp = _db.PermissionTypes.Where(p => p.PermisssionTypeId == permissionTypeId).First();
        return temp.PermissionType1;
    }

    #endregion

}
