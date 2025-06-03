using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class MenuCategory
{
    public int CategoryId { get; set; }

    public string MenuName { get; set; } = null!;

    public string MenuDescription { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    public virtual User? UpdatedByNavigation { get; set; }
}
