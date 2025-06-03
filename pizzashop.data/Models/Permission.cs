using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class Permission
{
    public int PermissionId { get; set; }

    public int RoleId { get; set; }

    public int PermissionTypeId { get; set; }

    public bool? CanView { get; set; }

    public bool? CanEdit { get; set; }

    public bool CanDelete { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual PermissionType PermissionType { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}
