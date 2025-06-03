namespace pizzashop.data.ViewModels.CustomerVM;

public class CustomerDetailsVM
{

    public string? Name { get; set; }


    public string? PhoneNo { get; set; }

    public DateTime CreatedOn { get; set; }

    public float Avgbill { get; set; }

    public float Maxbill { get; set; }

    public int Visits { get; set; }

    public List<CustomerOrderVM> OrderHistory { get; set; } = new List<CustomerOrderVM>();
}
