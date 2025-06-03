namespace pizzashop.data.ViewModels;

public class MenuCustomerDetail{

    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public int Persons { get; set; }
}