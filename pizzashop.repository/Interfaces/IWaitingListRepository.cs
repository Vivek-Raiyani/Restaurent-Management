using pizzashop.data.Models;

namespace pizzashop.repository.Interfaces.OrderApp;

public interface IWaitingListRepository
{
    public bool UpdateWaitingList(Waitlist waitingToken);

    public bool DeleteWaitingList(Waitlist waitingToken);

    public IEnumerable<Waitlist> GetAllWaitingList( int floorid = 0);

    public Waitlist WaitlistToken(int tokenid);
    public Waitlist WaitlistToken(string email);

}
