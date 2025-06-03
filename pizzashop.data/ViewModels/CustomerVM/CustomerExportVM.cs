namespace pizzashop.data.ViewModels.CustomerVM;

public class CustomerExportVM
{
    public string Search { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Date { get; set; } = null!;

    public int Record { get; set; } = 0;

    public List<CustomerTableVM> Customer { get; set; } = new List<CustomerTableVM>();
}
