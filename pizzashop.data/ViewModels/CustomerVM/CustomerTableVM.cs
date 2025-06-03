namespace pizzashop.data.ViewModels.CustomerVM;

public class CustomerTableVM
{
    public int CustomerId { get; set; }

    public string? Name { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNo { get; set; }

    public DateTime CreatedOn { get; set; }

    public int TotalOrders { get; set; }
}

