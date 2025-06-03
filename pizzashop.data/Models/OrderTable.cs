﻿using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class OrderTable
{
    public int OrderTableId { get; set; }

    public int OrderId { get; set; }

    public int TableId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual TableDetail Table { get; set; } = null!;
}
