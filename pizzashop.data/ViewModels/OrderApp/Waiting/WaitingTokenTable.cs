namespace pizzashop.data.ViewModels.OrderApp.Waiting;

public class WaitingTokenTable
{

    public int Id { get; set;}
    public string Name { get; set;} = null!;

    public string Email { get; set;} = null!;

    public string Phone { get; set;} = null!;

    public int Persons { get; set;}

    public int Sectionid { get; set;}

    public DateTime CreatedAt { get; set; }
}


public class EmailCustomer
{
    public string Email { get; set;} = null!;

    public string Phone { get; set;} = null!;

    public string Name { get; set;} = null!;
}