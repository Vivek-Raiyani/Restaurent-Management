using pizzashop.data.ViewModels;

namespace pizzashop.services.Interfaces;

public interface IpermissionServices
{

    public PermissionListVM GetAllPermissions(int roleid);

    public bool UpdatePermission(PermissionListVM permission_list);


}
