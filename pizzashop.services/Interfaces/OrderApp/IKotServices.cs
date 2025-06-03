using pizzashop.data.ViewModels.OrderApp.Kot;

namespace pizzashop.services.Interfaces.OrderApp;

public interface IKotServices
{
    public IEnumerable<OrderAppCategoryVM> GetCategories();

    public PaginatedKotVM<OrderCardVM> KotPagination(int categoryid, string status,int size = 1 , int page =1 );
    public IEnumerable<OrderCardVM> KotOrdersList(int categoryid , string status , int size = 3, int page = 1);
     public OrderModalVM KotOrders(int categoryid, string status, int orderid);

     public bool UpdateKotOrder(KotOrderUpdate order);
}
