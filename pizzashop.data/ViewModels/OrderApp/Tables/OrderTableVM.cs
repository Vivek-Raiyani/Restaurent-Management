using System.Security.Cryptography.X509Certificates;

namespace pizzashop.data.ViewModels.OrderApp.Tables;

public class OrderTableVM
{
    public int Id { get; set;}

    public int OrderId { get; set;}

    public string Name { get; set;}=null!;

    public int Capacity { get; set; } 

    public string Status { get; set; } = null!;

    public float? SubTotal { get; set; }

    public DateTime OrderDuration {get; set; }

}
