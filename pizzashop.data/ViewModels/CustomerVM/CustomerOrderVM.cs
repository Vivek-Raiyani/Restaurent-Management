namespace pizzashop.data.ViewModels.CustomerVM;

public class CustomerOrderVM
{
     public DateTime OrderDate { get; set;}

    public string OrderType { get; set; } = null!;

    public float Total { get; set; }

    public int Items { get; set; } // from mapping

    public string PaymentMethod { get; set; } = null!; // from payment

}
