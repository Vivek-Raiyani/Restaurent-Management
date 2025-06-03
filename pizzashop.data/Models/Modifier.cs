using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class Modifier
{
    public int ModifierId { get; set; }

    public string ModName { get; set; } = null!;

    public float Rate { get; set; }

    public short Quantity { get; set; }

    public string Unit { get; set; } = null!;

    public string ModifierDesc { get; set; } = null!;

    public bool? IsAvail { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<ModifierGroupMapping> ModifierGroupMappings { get; set; } = new List<ModifierGroupMapping>();

    public virtual ICollection<OrderItemModifier> OrderItemModifiers { get; set; } = new List<OrderItemModifier>();

    public virtual User? UpdatedByNavigation { get; set; }
}
