using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class OrderDetail
{
    public int OrderDetailsId { get; set; }

    public int OrderId { get; set; }

    public int ItemId { get; set; }

    public short Quantity { get; set; }

    public string IteamStatus { get; set; } = null!;

    public string? Instructions { get; set; }

    public short Prepared { get; set; }

    public virtual MenuItem Item { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<OrderItemModifier> OrderItemModifiers { get; set; } = new List<OrderItemModifier>();
}
