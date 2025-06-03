using pizzashop.data.Models;

namespace pizzashop.repository.Interfaces;

public interface IEventRepository
{
    public IEnumerable<Event> ReadAll();
    public Event Read(int eventId);
    public bool AddEvent(Event newEvent);
    public bool UpdateEvent(Event newEvent);
}
