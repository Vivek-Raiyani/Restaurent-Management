using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.repository.Interfaces;

namespace pizzashop.repository.Implementations;

public class PermissionRepository : IPermissionRepository
{

    private readonly PizzashopContext _db;

    public PermissionRepository(PizzashopContext db)
    {
        _db = db;
    }

    #region  read permission

    // gives list of permisssion for role
    public List<Permission> GetRolePermissionAll(int roleId)
    {
        var temp = _db.Permissions.Where(r => r.RoleId == roleId).OrderBy(p=> p.PermissionId).ToList();
        return temp;
    }

    // gives a single permission for role
    public Permission GetRolePermission(int roleId, int permissionTypeId)
    {
        Permission temp = _db.Permissions.Where(p => p.RoleId == roleId && p.PermissionTypeId == permissionTypeId).First();
        return temp;
    }

    #endregion

    #region  edit permission
    public void UpdatePermission(Permission permission)
    {
        _db.Update(permission);
        _db.SaveChanges();
        return;
    }

    #endregion


    #region  new permission repo
    // public bool Update(Permission permission)
    // {
    //     try
    //     {
    //         _db.Permissions.Update(permission);
    //         _db.SaveChanges();
    //         return true;
    //     }
    //     catch (DbUpdateException ex)
    //     {
    //         Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"General Exception: {ex.Message}\n{ex.StackTrace}");
    //     }
    //     return false;
    // }
    
    // public Permission Read (int roleId, int permissionTypeId){
    //     try{
    //         var permission = _db.Permissions.SingleOrDefault(x => x.RoleId == roleId && x.PermissionTypeId == permissionTypeId);
    //         return permission;
    //     }
    //     catch(Exception ex){
    //          Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
    //          Console.WriteLine($"Exception: {ex.Message}\n{ex.StackTrace}");
    //     }
    //     return null;
    // }

    // public IEnumerable<Permission> ReadAll(int roleid){
    //     return _db.Permissions.Where(x=> x.RoleId == roleid);
    // }
    
    
    #endregion

}
