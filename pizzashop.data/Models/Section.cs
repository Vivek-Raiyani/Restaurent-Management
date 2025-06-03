using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class Section
{
    public int SectionId { get; set; }

    public string SecName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<TableDetail> TableDetails { get; set; } = new List<TableDetail>();

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual ICollection<Waitlist> Waitlists { get; set; } = new List<Waitlist>();
}
