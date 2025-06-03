

using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.repository.Interfaces.Orders;

namespace pizzashop.repository.Implementations.Orders;

public class OrderRepository : IOrderRepository
{
    private readonly PizzashopContext _db;

    public OrderRepository(PizzashopContext db)
    {
        _db = db;
    }

    public List<Order> KotOrderList()
    {
        try
        {
            var order = _db.Orders.Where(o=> o.IsDeleted != true || o.OrderStatus != "cancelled")
                        .Include(o=> o.OrderDetails.Where(o=> o.IteamStatus != "cancelled")).ThenInclude(d=> d.Item).ThenInclude(i=> i.Category)
                        .Include(o=> o.OrderDetails).ThenInclude(d=>d.OrderItemModifiers).ThenInclude(m=> m.Modifier)
                        .Include(o=> o.OrderTables).ThenInclude(t=> t.Table).ThenInclude(s=>s.Section)
                        .OrderBy(o=>o.OrderId)
                        .ToList();
            return order;

        }
        catch(Exception e)
        {   
            Console.WriteLine(e.Message);
            return new List<Order>();
        }
    }

    public Order KotOrder(int orderid)
    {
        try
        {
            var order = _db.Orders.Where(o=> o.IsDeleted != true || o.OrderStatus != "cancelled")
                        .Include(o=> o.OrderDetails.Where(o=> o.IteamStatus != "cancelled")).ThenInclude(d=> d.Item).ThenInclude(i=> i.Category)
                        .Include(o=> o.OrderDetails).ThenInclude(d=>d.OrderItemModifiers).ThenInclude(m=> m.Modifier)
                        .Include(o=> o.OrderTables).ThenInclude(t=> t.Table).ThenInclude(s=>s.Section);

            return order.SingleOrDefault(o=> o.OrderId == orderid) ?? new Order();

        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return new Order();
        }
    }

    // for orderapp-menu
    public Order OrderAppDetails(int orderid)
    {
        try
        {
            var order = _db.Orders.Where(o=> o.IsDeleted != true || o.OrderStatus != "cancelled")
                        .Include(o=> o.OrderTables).ThenInclude(t=> t.Table).ThenInclude(s=>s.Section)
                        .Include(o=> o.OrderDetails).ThenInclude(d=> d.Item)
                        .Include(o=> o.OrderDetails.Where(o=> o.IteamStatus!= "cancelled")).ThenInclude(d=>d.OrderItemModifiers).ThenInclude(m=> m.Modifier)
                        .Include(o=> o.Customer)
                        .Include(o=> o.Payments)
                        .Include(o=> o.OrderTaxes.Where(o=> o.IsDeleted != true))
                        .SingleOrDefault(o=> o.OrderId == orderid);
            return order ?? new Order();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return new Order();
        }
    }

    public IEnumerable<Order> DashboardOrderdata()
    {
        var order = _db.Orders.Where(o=> o.IsDeleted != true || o.OrderStatus != "cancelled")
                        .Include(o=> o.OrderDetails).ThenInclude(i=> i.Item)
                        .Include(o=> o.OrderDetails).OrderBy(o=>o.OrderId);
            return order;
    }

    public Order ReadOrderOnly(int orderid)
    {
        try
        {
            var order = _db.Orders.Where(o=> o.IsDeleted != true || o.OrderStatus != "cancelled")
                        .SingleOrDefault(o=> o.OrderId == orderid);
            return order ?? new Order();
        }
        catch(Exception e)
        {
            Console.WriteLine($"{e.Message}, {e.StackTrace}");
            return new Order();
        }
    }

    public bool UpdateOrder(Order order)
    {
        try
        {
            _db.Orders.Update(order);
            _db.SaveChanges();
            return true;
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public Order ReadOrderCustomer (int orderId, int customerId)
    {
        try
        {
            
            return _db.Orders.Where(o=> o.IsDeleted != true || o.OrderStatus != "cancelled")
                    .Include(o=> o.Customer).SingleOrDefault(o=> o.CustomerId == customerId && o.OrderId == orderId) ?? new Order();

        }catch(Exception e)
        {    
            Console.WriteLine(e.Message);
            return new Order();
        }
    }

    public IEnumerable<Order> ExportOrderData(string status)
    {
        if (status != "allstatus")
        {
            return _db.Orders.Include(o => o.Customer).Include(o => o.Payments).Include(o => o.Review)
                .Where(t => t.IsDeleted != true && t.OrderStatus == status).OrderBy(o=>o.OrderId);
        }
        return _db.Orders.Include(o => o.Customer).Include(o => o.Payments).Include(o => o.Review)
                .Where(t => t.IsDeleted != true).OrderBy(o=>o.OrderId);
    }


    public Order AddNewOrder(Order order){
        try{
            _db.Orders.Add(order);
            _db.SaveChanges();
            return _db.Orders.SingleOrDefault(o=> o.CustomerId == order.CustomerId && o.CreatedOn == order.CreatedOn) ?? new Order();
        }catch(DbUpdateException e){
            Console.WriteLine(e.Message);
        }catch (Exception e){
            Console.WriteLine(e.Message);
        }
        return new Order();
    }
    
    public Order GetOrderDetails(int orderid)
    {
        return _db.Orders.Where(o=> o.IsDeleted != true || o.OrderStatus != "cancelled")
        .Include(o => o.OrderDetails.Where(o=> o.IteamStatus != "cancelled")).ThenInclude(i => i.Item)
        .Include(o => o.OrderDetails.Where(o=> o.IteamStatus != "cancelled")).ThenInclude(i => i.OrderItemModifiers).ThenInclude(m => m.Modifier)
        .Include(o => o.Payments)
        .Include(o => o.Customer)
        .Include(o => o.OrderTables).ThenInclude(t => t.Table).ThenInclude(s => s.Section)
        .Include(o => o.OrderTaxes).ThenInclude(t => t.Tax)
        .FirstOrDefault(o => o.IsDeleted != true && o.OrderId == orderid)
        ?? new Order();
    }


    public List<Order> Pagination(string search, string status)
    {
        var data = new List<Order>();
        if (status == "allstatus")
        {
            data = Search(search:search);
        }
        else
        {   
           data = Search(search:search).Where(o=> o.OrderStatus == status).ToList();
        }

        return data.OrderBy(x => x.OrderId).ToList();
    }

    public List<Order> Search(string search)
    {
         if (string.IsNullOrEmpty(search))
        {
            return _db.Orders.Where(o=> o.IsDeleted != true).Include(o => o.Customer).Include(o => o.Payments).Include(o => o.Review)
                        .Where(t => t.IsDeleted != true).OrderBy(o=>o.OrderId)
                        .ToList();
        }
        else
        {
            return _db.Orders.Where(o=> o.IsDeleted != true).Include(o => o.Customer).Include(o => o.Payments).Include(o => o.Review)
                        .Where(t => t.IsDeleted != true && (t.Customer.Name.ToLower().Contains(search.ToLower()) || t.OrderId.ToString() == search))
                        .OrderBy(o=>o.OrderId)
                        .ToList();
        }
    }

}
