

using System.Security.Cryptography.X509Certificates;
using pizzashop.data.Models;

namespace pizzashop.data.ViewModels.Orders;

public class OderDetailsVM
{
    public int OrderID { get; set;}
    
    public DateTime PaidOn { get; set;}

    public DateTime PlacedOn { get; set;}

    public DateTime ModifiedOn { get; set;}

    public TimeOnly OrderDuration { get; set;}

    public string? PaymentMethod { get; set;}

    public string Status { get; set;} =null!;
    
    public int InvoiceId { get; set;} 

    public float OrderTotal { get; set;}

    public OrderDetailsCustomerVM Customer{ get; set;} = new OrderDetailsCustomerVM();

    public List<OrderDetailsItemVm> Items {get; set;} = new List<OrderDetailsItemVm>();

    public OrderDetailsTableVM Tables {get; set;} = new OrderDetailsTableVM();

    public List<OrderTax> OrderTax { get; set;} = new List<OrderTax>();

}
