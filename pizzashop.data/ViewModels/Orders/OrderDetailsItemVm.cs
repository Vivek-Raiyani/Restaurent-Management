namespace pizzashop.data.ViewModels.Orders;

public class OrderDetailsItemVm
{

    public string Name { get; set; } =null!;

    public int Quantity { get; set; }

    public float Price { get; set; }

    public float Total { get; set; }

    public List<OrderDetailsModifierVM> Modifiers { get; set; } = new List<OrderDetailsModifierVM>();
}
