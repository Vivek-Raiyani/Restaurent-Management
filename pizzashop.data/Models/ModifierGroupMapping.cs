using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class ModifierGroupMapping
{
    public int MappingId { get; set; }

    public int ModifierGroupId { get; set; }

    public int ModifierId { get; set; }

    public bool Isdeleted { get; set; }

    public virtual Modifier Modifier { get; set; } = null!;

    public virtual ModifierGroup ModifierGroup { get; set; } = null!;
}
