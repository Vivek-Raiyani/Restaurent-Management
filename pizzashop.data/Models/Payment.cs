using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class Payment
{
    public int PayId { get; set; }

    public int CustomerId { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public float OrderTotal { get; set; }

    public DateTime PaymentDate { get; set; }

    public int OrderId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
