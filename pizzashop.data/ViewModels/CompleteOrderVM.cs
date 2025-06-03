namespace pizzashop.data.ViewModels;

public class CompleteOrderVM
{
    public int OrderId { get; set; }

    public string PaymentMode { get; set; } = null!;

    public float Total { get; set; }
    public string TaxString { get; set; } = null!;
    public List<OrderTaxSave> Tax = new List<OrderTaxSave>();
}

public class OrderTaxSave{

    public int TaxId { get; set; }

    public float Amount { get; set; }
}