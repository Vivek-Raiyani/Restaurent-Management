using pizzashop.data.Models;

namespace pizzashop.data.ViewModels;

public class PermissionListVM
{

    public string RoleName { get; set; } = null!;

    public int RoleId { get; set; }

    public List<PermissionVM> Permissions{ get; set; } = new List<PermissionVM>();
}
