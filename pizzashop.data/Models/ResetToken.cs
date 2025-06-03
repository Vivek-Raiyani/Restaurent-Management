using System;
using System.Collections.Generic;

namespace pizzashop.data.Models;

public partial class ResetToken
{
    public int Id { get; set; }

    public DateOnly CreatedOn { get; set; }

    public DateOnly ValidUpto { get; set; }

    public string Email { get; set; } = null!;

    public int Token { get; set; }

    public bool Used { get; set; }
}
