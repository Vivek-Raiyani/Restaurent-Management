using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class TableDetail
{
    public int TableId { get; set; }

    public string TblName { get; set; } = null!;

    public int SectionId { get; set; }

    public short Capacity { get; set; }

    public string TableStatus { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<OrderTable> OrderTables { get; set; } = new List<OrderTable>();

    public virtual Section Section { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}
