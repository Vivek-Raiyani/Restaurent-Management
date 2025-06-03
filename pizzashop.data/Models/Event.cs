using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class Event
{
    public int EventId { get; set; }

    public DateTime EventDate { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string EventType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int Persons { get; set; }

    public bool? Dinein { get; set; }

    public bool Ac { get; set; }

    public int SetupId { get; set; }

    public int OrderId { get; set; }

    public string? Instructions { get; set; }

    public string? Address { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Eventsetup Setup { get; set; } = null!;
}
