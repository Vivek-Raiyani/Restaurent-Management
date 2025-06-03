namespace pizzashop.data.ViewModels.Orders;

public class OrderListVM
{

    public int OrderID { get; set;}

    public DateTime OrderDate { get; set;}

    public string Customer { get; set;} = null!;

    public string Status { get; set;} = null!;

    public string PaymentMod  { get; set;} =null!;

    public int Rating { get; set;}

    public float Total { get; set;}

}
