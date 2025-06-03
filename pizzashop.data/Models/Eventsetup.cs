using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class Eventsetup
{
    public int SetupId { get; set; }

    public string Setup { get; set; } = null!;

    public string? Items { get; set; }

    public bool? IsDefault { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
