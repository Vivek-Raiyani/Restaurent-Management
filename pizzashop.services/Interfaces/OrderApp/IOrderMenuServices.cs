using pizzashop.data.ViewModels;

namespace pizzashop.services.Interfaces.OrderApp;

public interface IOrderMenuServices
{

    // public List<CategoryVM> OrderAppMenuCategories();
    public MenuCategories OrderAppMenuCategories();
    public List<CategoryItems> CategoryItems(int categoryid, string search);
    public List<ItemGroups> ItemGroups(int itemid);

    public string FavToggle(int itemid);
    public OrderDisplay OrderDisplay(int orderid);
    public bool OrderUpdate(List<UpdateOrderDetails> items, List<int> tax, int orderid );
    public bool SaveOrder(SaveOrderVM order);
    public bool OrderReview(OrderReview review);
    // public bool OrderComplete(int orderid);
    
    public MenuCustomerDetail OrderCustomer (int customerId, int orderId);

    public bool OrderComplete(CompleteOrderVM order);
    public string DeleteOrderItem(int detailsid);

    public int CheckPrepared(int detailsid);

    public bool CheckOrderComplete(int orderId);

    public bool OrderCancel(int orderid);
}
