using pizzashop.data.Models;
using pizzashop.data.ViewModels.Orders;

namespace pizzashop.repository.Interfaces.Orders;

public interface IOrderRepository
{

  public IEnumerable<Order> ExportOrderData(string status);

  // public Order GetOrderDetails (int orderid);
  public Order GetOrderDetails(int orderid);

  public Order AddNewOrder(Order order);


  public Order KotOrder(int orderid);
  public List<Order> KotOrderList();

  public Order OrderAppDetails(int orderid);
  public Order ReadOrderOnly(int orderid);
  public bool UpdateOrder(Order order);

  public Order ReadOrderCustomer (int orderId, int customerId);

   public IEnumerable<Order> DashboardOrderdata();

   public List<Order> Pagination(string search, string status);

}
