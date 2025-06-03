using pizzashop.data.Models;

namespace pizzashop.repository.Interfaces;

public interface IPermissionRepository
{

    public List<Permission> GetRolePermissionAll(int roleId);

    public Permission GetRolePermission(int roleId, int permissionTypeId);

    public void UpdatePermission(Permission permission);
}
