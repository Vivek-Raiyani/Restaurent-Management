using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class OrderTax
{
    public int OrderTaxId { get; set; }

    public int OrderId { get; set; }

    public int TaxId { get; set; }

    public float Amount { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Taxis Tax { get; set; } = null!;
}
