namespace pizzashop.data.ViewModels.OrderApp.Tables;

public class OrderSectionVM
{
     public int Id { get; set;}

    public string Name { get; set;}=null!;
    

    public IEnumerable<OrderTableVM> Tables { get; set; } = new List<OrderTableVM>();
}
