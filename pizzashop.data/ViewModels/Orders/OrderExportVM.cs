namespace pizzashop.data.ViewModels.Orders;

public class OrderExportVM
{

    public string Search { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Date { get; set; } = null!;

    public int Record { get; set; } = 0;

    public List<OrderListVM> OrderData { get; set; } = new List<OrderListVM>();
}
