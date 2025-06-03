using pizzashop.data.Models;
using pizzashop.repository.Interfaces;

namespace pizzashop.repository.Implementations;

public class RolesRepository : IRolesRepository
{
    private readonly PizzashopContext _context;
    public RolesRepository(PizzashopContext context)
    {
        _context = context;
    }

    #region read  role name
    public string? GetRole(int id)
    {
        Role role = _context.Roles.Find(id);
        if (role == null)
        {
            return null;
        }
        return role.RoleName;
    }
    #endregion

    #region read role id
    public int GetRoleId(string name)
    {
        Role role = _context.Roles.FirstOrDefault(r => r.RoleName == name);
        if (role == null)
        {
            return 0;
        }
        return role.RoleId;
    }
    #endregion
 
    #region read all roles
    public List<Role> GetAllRoles()
    {
        return _context.Roles.ToList();
    }

    #endregion

}
