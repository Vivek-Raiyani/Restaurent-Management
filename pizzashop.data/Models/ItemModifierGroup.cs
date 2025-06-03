using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class ItemModifierGroup
{
    public int ItemModId { get; set; }

    public int ItemId { get; set; }

    public int ModiferId { get; set; }

    public int Minimun { get; set; }

    public int Maximum { get; set; }

    public bool Isdeleted { get; set; }

    public virtual MenuItem Item { get; set; } = null!;

    public virtual ModifierGroup Modifer { get; set; } = null!;
}
