using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.repository.Interfaces;

namespace pizzashop.repository.Implementations.Orders;

public class OrderDetailsRepository : IOrderDetailsRepository
{
    private readonly PizzashopContext _db;

    public OrderDetailsRepository(PizzashopContext db)
    {
        _db = db;
    }
    public bool AddUpdateMany(List<OrderDetail> details, int status)
    {
        try
        {
            if (status == 0)
            {
                _db.OrderDetails.AddRange(details);
            }
            else
            {
                _db.OrderDetails.UpdateRange(details);
            }
            _db.SaveChanges();
            return true;
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return false;
    }


    public bool AddUpdate(OrderDetail details)
    {
        try
        {
            if (details.OrderDetailsId == 0)
            {
                _db.OrderDetails.Add(details);
            }
            else
            {
                _db.OrderDetails.Update(details);
            }
            _db.SaveChanges();
            return true;
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return false;
    }

    public OrderDetail Read(int detailsid)
    {
        try
        {
            var details = _db.OrderDetails.SingleOrDefault(d=> d.OrderDetailsId == detailsid && d.IteamStatus != "cancelled") ?? new OrderDetail();
            return details;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return new OrderDetail();
    }

    public IEnumerable<OrderDetail> ReadAll(int orderid)
    {
        try
        {
            var details = _db.OrderDetails.Where(x => x.OrderId == orderid && x.IteamStatus != "cancelled").OrderBy(o=>o.OrderId).ToList();
            return details;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return new List<OrderDetail>();
    }

}
