using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.repository.Interfaces;

namespace pizzashop.repository.Implementations;

public class EventRepository : IEventRepository
{

    private readonly PizzashopContext _db;

    public EventRepository(PizzashopContext db)
    {
        _db = db;
    }

    public IEnumerable<Event> ReadAll()
    {
        return _db.Events.Include(e=> e.Customer).ToList();
    }

    public Event Read(int eventId)
    {
        try{
            return _db.Events.Include(e=> e.Customer).Include(e=> e.Order).ThenInclude(o=> o.OrderDetails).SingleOrDefault(e=> e.EventId == eventId) ?? new Event();
        }   
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new Event();
        }
    }


    public bool AddEvent(Event newEvent)
    {
        try{
            _db.Events.Add(newEvent);
            _db.SaveChanges();
            return true;
        }   
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public bool UpdateEvent(Event newEvent)
    {
        try{
            _db.Events.Update(newEvent);
            _db.SaveChanges();
            return true;
        }   
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

}
