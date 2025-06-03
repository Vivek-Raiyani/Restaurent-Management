using pizzashop.data.Models;

namespace pizzashop.repository.Interfaces;

public interface IOrderDetailsRepository
{
    public bool AddUpdateMany(List<OrderDetail> details,int status);
    public bool AddUpdate(OrderDetail details);
    public OrderDetail Read(int detailsid);
    public IEnumerable<OrderDetail> ReadAll(int orderid);
    
}
