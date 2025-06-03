using pizzashop.data.ViewModels.OrderApp.Tables;
using pizzashop.data.ViewModels.OrderApp.Waiting;

namespace pizzashop.services.Interfaces.OrderApp;

public interface IWaitingListServices
{

    public IEnumerable<OrderSectionVM> GetSections(int floorid);

    public IEnumerable<TableVM> GetAvailableTables(int floorid);

    public bool UpdateWaitingList(WaitingTokenVM waitingToken);

    public IEnumerable<WaitingTokenVM> GetWaitingList(int floorid);

    public IEnumerable<WaitingTokenTable> GetWaitingTable(int floorid);

    public WaitingTokenVM GetWaitingToken(int tokenid);

    public bool Delete(int tokenid);

    public EmailCustomer GetEmailCustomer(string email);

    public int WaitinfTokenCount();

}
