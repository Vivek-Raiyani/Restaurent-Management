using System.Security.Cryptography.X509Certificates;

namespace pizzashop.data.ViewModels;

public class CustomerReview
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public int Food { get; set; }

    public int Service {    get; set; }

    public int Ambience { get; set; }

    public string? Comments { get; set; }
}
