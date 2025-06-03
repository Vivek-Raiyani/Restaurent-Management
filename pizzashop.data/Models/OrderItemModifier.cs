using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class OrderItemModifier
{
    public int Id { get; set; }

    public int ModifierId { get; set; }

    public int OrderDetailsId { get; set; }

    public virtual Modifier Modifier { get; set; } = null!;

    public virtual OrderDetail OrderDetails { get; set; } = null!;
}
