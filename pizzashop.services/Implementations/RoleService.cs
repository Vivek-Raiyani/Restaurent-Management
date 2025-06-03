using pizzashop.data.Models;
using pizzashop.repository.Interfaces;
using pizzashop.services.Interfaces;

namespace pizzashop.services.Implementations;

public class RoleService : IRoleService
{
    private readonly IRolesRepository _roleRepo;

    public RoleService(IRolesRepository rolesRepository)
    {
        _roleRepo = rolesRepository;
    }

    public string GetRoleName(int roleId){
        return _roleRepo.GetRole(roleId) ?? "";
    }

    public int GetRoleId(string rolename){
        return _roleRepo.GetRoleId(rolename);
    }

    public List<Role> GetAllRoles(){
        return _roleRepo.GetAllRoles();
    }
}
