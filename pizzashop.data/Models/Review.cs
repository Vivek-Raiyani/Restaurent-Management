using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class Review
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public short Food { get; set; }

    public short ServiceRatings { get; set; }

    public short? Ambience { get; set; }

    public string? Descript { get; set; }

    public int OrderId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
