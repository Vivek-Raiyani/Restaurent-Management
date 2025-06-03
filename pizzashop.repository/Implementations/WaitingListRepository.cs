using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.data.ViewModels.OrderApp.Waiting;
using pizzashop.repository.Interfaces.OrderApp;

namespace pizzashop.repository.Implementations.OrderApp;

public class WaitingListRepository : IWaitingListRepository
{

    private readonly PizzashopContext _db;

    public WaitingListRepository(PizzashopContext db)
    {
        _db = db;
    }

    // add and update tokne method
    public bool UpdateWaitingList(Waitlist waitingToken)
    {
        waitingToken.CreatedOn = DateTime.Now;
        // tokenid 0 add new token 
        // tokenid > 0 update token
        try
        {
            if (waitingToken.TokenId == 0)
            {
                _db.Waitlists.Add(waitingToken);
                _db.SaveChanges();
            }
            else
            {
                _db.Waitlists.Update(waitingToken);
                _db.SaveChanges();
            }
            return true;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Exception: {ex.Message}\n{ex.StackTrace}");
        }
        return false;
    }

    // hard delete
    public bool DeleteWaitingList(Waitlist waitingToken)
    {
        try
        {
            _db.Waitlists.Remove(waitingToken);
            _db.SaveChanges();
            return true;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Exception: {ex.Message}\n{ex.StackTrace}");
        }
        return false;
    }

    // read 
    public IEnumerable<Waitlist> GetAllWaitingList(int floorId=0)
    {
        if (floorId > 0)
        {
            return _db.Waitlists.Where(t => t.SectionsId == floorId).ToList();

        }
        return _db.Waitlists.ToList();
    }

    public Waitlist WaitlistToken(int tokenId)
    {
        return _db.Waitlists.Find(tokenId) ;
    }

    public Waitlist WaitlistToken(string email)
    {
        return _db.Waitlists.Where(w => w.CustEmail == email).First();
    }


}
