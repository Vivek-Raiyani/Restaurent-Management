using pizzashop.data.ViewModels.OrderApp.Tables;
using pizzashop.data.ViewModels.OrderApp.Waiting;

namespace pizzashop.services.Interfaces.OrderApp;

public interface IOrderTableServices
{

    public IEnumerable<OrderSectionVM> GetSections();

    public int AssignTables(WaitingTokenVM token, List<int> tableids);
}
