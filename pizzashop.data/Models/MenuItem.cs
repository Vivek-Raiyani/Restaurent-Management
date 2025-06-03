using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class MenuItem
{
    public int IteamId { get; set; }

    public int CategoryId { get; set; }

    public string IteamName { get; set; } = null!;

    public string ItemType { get; set; } = null!;

    public float Rate { get; set; }

    public short Quantity { get; set; }

    public string Unit { get; set; } = null!;

    public bool? Available { get; set; }

    public bool? DefaultTax { get; set; }

    public short TaxPercentage { get; set; }

    public short ShortCode { get; set; }

    public string Description { get; set; } = null!;

    public string? IteamImg { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsFavourite { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual MenuCategory Category { get; set; } = null!;

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<ItemModifierGroup> ItemModifierGroups { get; set; } = new List<ItemModifierGroup>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? UpdatedByNavigation { get; set; }
}
