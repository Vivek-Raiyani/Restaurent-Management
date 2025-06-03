using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.repository.Interfaces;

namespace pizzashop.repository.Implementations.Orders;

public class OrderTaxRepository : IOrderTaxRepository
{
    private readonly PizzashopContext _db;

    public OrderTaxRepository(PizzashopContext db)
    {
        _db = db;
    }

    public bool AddUpdateMany(List<OrderTax> Taxes, int status)
    {
        try
        {
            if (status == 0)
            {
                _db.OrderTaxes.AddRange(Taxes);
            }
            else
            {
                _db.OrderTaxes.UpdateRange(Taxes);
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


    public bool AddUpdate(OrderTax Taxes)
    {
        try
        {
            if (Taxes.TaxId == 0)
            {
                _db.OrderTaxes.Add(Taxes);
            }
            else
            {
                _db.OrderTaxes.Update(Taxes);
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

    public OrderTax Read(int Taxesid)
    {
        try
        {
            var Taxes = _db.OrderTaxes.Find(Taxesid);
            return Taxes ?? new OrderTax();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return new OrderTax();
    }

    public IEnumerable<OrderTax> ReadAll(int orderid)
    {
        try
        {
            var Taxes = _db.OrderTaxes.Where(x => x.OrderId == orderid).OrderBy(o=> o.OrderTaxId).ToList();
            return Taxes;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return new List<OrderTax>();
    }

}
