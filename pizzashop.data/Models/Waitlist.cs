using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class Waitlist
{
    public int TokenId { get; set; }

    public short NoPeople { get; set; }

    public string CustName { get; set; } = null!;

    public string CustEmail { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int SectionsId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Section Sections { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}
