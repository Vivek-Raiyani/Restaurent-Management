using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class ModifierGroup
{
    public int ModGroupId { get; set; }

    public string ModifierName { get; set; } = null!;

    public string GroupDescription { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<ItemModifierGroup> ItemModifierGroups { get; set; } = new List<ItemModifierGroup>();

    public virtual ICollection<ModifierGroupMapping> ModifierGroupMappings { get; set; } = new List<ModifierGroupMapping>();

    public virtual User? UpdatedByNavigation { get; set; }
}
