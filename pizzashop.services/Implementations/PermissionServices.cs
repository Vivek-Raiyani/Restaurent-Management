using pizzashop.data.Models;
using pizzashop.data.ViewModels;
using pizzashop.repository.Interfaces;
using pizzashop.services.Interfaces;

namespace pizzashop.services.Implementations;

public class PermissionServices : IpermissionServices
{

    private readonly IPermissionRepository _permissionRepo;
    private readonly IRolesRepository _rolesRepository;
    private readonly IPermissionTypeRepository _permissiontype;
    public PermissionServices(IPermissionRepository permissionRepo, IPermissionTypeRepository permissiontype, IRolesRepository rolesRepository)
    {
        _permissionRepo = permissionRepo;
        _permissiontype = permissiontype;
        _rolesRepository = rolesRepository;
    }

    public PermissionListVM GetAllPermissions(int roleid)
    {
        var permission_iteams = _permissionRepo.GetRolePermissionAll(roleid);// type is Model.permission
        List<PermissionVM> permission_list = new List<PermissionVM>();
        foreach (var permission in permission_iteams)
        {
            var iteam = new PermissionVM();
            iteam.CanDelete = permission.CanDelete;
            iteam.CanEdit = (bool)permission.CanEdit;
            iteam.CanView = (bool)permission.CanView;
            iteam.PermissionTypeId = permission.PermissionTypeId;
            iteam.Name = _permissiontype.GetPermissionType(permission.PermissionTypeId);

            permission_list.Add(iteam);
        }
        PermissionListVM list_permissions = new PermissionListVM();
        list_permissions.RoleName = _rolesRepository.GetRole(roleid);
        list_permissions.RoleId = roleid;
        list_permissions.Permissions = permission_list;

        return list_permissions;
    }

    public bool UpdatePermission(PermissionListVM permission_list)
    {
        // var roleName = permission_list.roleName;
        // var roleId = _rolesRepository.GetRoleId(roleName);
        try{
            var roleId = permission_list.RoleId;
            var new_permission = permission_list.Permissions;
            //var old_permisssion = _permissionRepo.get_role_permission(_rolesRepository.GetRoleId(roleName));
            foreach (var permission in new_permission)
            {
                var iteam = _permissionRepo.GetRolePermission(roleId, permission.PermissionTypeId);
                    iteam.CanView = permission.CanView;
                    iteam.CanDelete = permission.CanDelete;
                    iteam.CanEdit = permission.CanEdit;
                // updated by user id 
                _permissionRepo.UpdatePermission(iteam);
            }
            return true;
        }
        catch (Exception ex){
                return false;
        }
    }
}
