using pizzashop.data.ViewModels.OrderApp.Tables;

namespace pizzashop.data.ViewModels.OrderApp.Waiting;

public class AssigntableVM
{
    public IEnumerable<OrderSectionVM>? SectionVMs { get; set; }
    public IEnumerable<TableVM>? TableVMs { get; set; } 
}

public class TableVM
{

    public int Id { get; set; }
    public string Name { get; set; } = null!;

}
