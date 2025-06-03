namespace pizzashop.data.ViewModels;

public class SaveOrderVM{

    public int OrderId { get; set; }

    public string OrderInstruct { get; set; } = null!;

    public string DetailsString { get; set; } = null!;
    
    public string PaymentMode { get; set; } = null!;
    public List<OrderItemVM> Details { get; set; } = new List<OrderItemVM>();

    public string TaxString { get; set; } = null!;
    public List<OrderTaxSave> Tax = new();

}

public class OrderItemVM{
    public int DetailsId { get; set; }

    public int ItemId { get; set; }

    public int Quantity { get; set; }

    public string Instruct { get; set; } = null!;

    public string ModifierStr { get; set; } = null!;
    public List<int> ModifierIds { get; set; } = new List<int>();
}