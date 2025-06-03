using pizzashop.data.Models;
using pizzashop.data.ViewModels;
using pizzashop.data.ViewModels.Orders;

namespace pizzashop.services.Interfaces.Orders;

public interface IOrderService
{


     // public OrderExportVM GetExportOrders(string search, string status = "", int time = 0);
     public OrderExportVM ExportOrders(string search, string status = "", int time = 1);
     public OderDetailsVM GetOrderDetail(int orderID);

     public PaginatedListVM<OrderListVM> Pagination(int page, int pageSize, string search, string status, int time, DateTime startdate,
                        DateTime enddate, string sortname, string sorttype, int sortbit);
}
