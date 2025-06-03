using pizzashop.data.Models;

namespace pizzashop.services.Interfaces;

public interface IRoleService
{
    public string GetRoleName(int roleId);

    public List<Role> GetAllRoles();

    public int GetRoleId(string rolename);
}
