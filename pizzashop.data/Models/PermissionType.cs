using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class PermissionType
{
    public int PermisssionTypeId { get; set; }

    public string PermissionType1 { get; set; } = null!;

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
