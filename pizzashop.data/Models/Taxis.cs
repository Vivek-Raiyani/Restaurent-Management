using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class Taxis
{
    public int TaxId { get; set; }

    public string TaxName { get; set; } = null!;

    public string TaxType { get; set; } = null!;

    public float TaxAmount { get; set; }

    public bool IsEnabled { get; set; }

    public bool IsDefault { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual ICollection<OrderTax> OrderTaxes { get; set; } = new List<OrderTax>();
}
