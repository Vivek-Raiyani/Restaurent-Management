using pizzashop.repository.Interfaces;
using pizzashop.services.Interfaces;

namespace pizzashop.services.Implementations;

public class EventServices : IEventServices
{

    private readonly IEventRepository _event;

    public EventServices(IEventRepository eventrepo)
    {
        _event = eventrepo;
    }

}
