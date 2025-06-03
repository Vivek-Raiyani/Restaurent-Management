using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.repository.Interfaces.Customers;

namespace pizzashop.repository.Implementations.Customers;

public class CustomerRepositry : ICustomerRepository
{
    private readonly PizzashopContext _db;
    public CustomerRepositry(PizzashopContext db)
    {
        _db = db;
    }


    public int ExportCustomerCount(string search, int status, DateTime start, DateTime end)
    {
        return status switch
        {
            2 => GetTodaysCustomer(search).Count(),
            7 => GetLastXDaysCustomer(search, status).Count(),
            30 => GetLastXDaysCustomer(search, status).Count(),
            -1 => GetCustomRangeCustomer(search, start: start, end: end).Count(),
            0 => GetCurrentMonthCustomer(search).Count(),
            _ => GetAllTimeCustomer(search).Count(),
        };
    }

    public IEnumerable<Customer> GetExportCustomers(string search, int status, DateTime start, DateTime end)
    {
        return status switch
        {
            2 => GetTodaysCustomer(search),
            7 => GetLastXDaysCustomer(search, status),
            30 => GetLastXDaysCustomer(search, status),
            -1 => GetCustomRangeCustomer(search, start: start, end: end),
            0 => GetCurrentMonthCustomer(search),
            _ => GetAllTimeCustomer(search),
        };
    }

    public IEnumerable<Customer> GetTodaysCustomer(string search)
    {
        var startdate = DateTime.Today;
        var enddate = DateTime.Today.AddDays(1);
        if (string.IsNullOrEmpty(search))
        {
            return _db.Customers.Include(o => o.Orders)
                            .Where(t => t.IsDeleted != true && t.CreatedOn >= startdate && t.CreatedOn <= enddate);
        }
        return _db.Customers.Include(o => o.Orders)
                            .Where(t => t.IsDeleted != true && t.CreatedOn >= startdate && t.CreatedOn <= enddate
                            && t.Name.ToLower().Contains(search.ToLower()));
    }

    public IEnumerable<Customer> GetCurrentMonthCustomer(string search)
    {
        var today = DateTime.Today;
        var startdate = new DateTime(today.Year, today.Month, 1, 00, 00, 01);
        var enddate = DateTime.Today.AddDays(1);
        if (string.IsNullOrEmpty(search))
        {
            return _db.Customers.Include(o => o.Orders)
                             .Where(t => t.IsDeleted != true && t.CreatedOn >= startdate && t.CreatedOn <= enddate);
        }
        return _db.Customers.Include(o => o.Orders)
                            .Where(t => t.IsDeleted != true && t.CreatedOn >= startdate && t.CreatedOn <= enddate
                            && t.Name.ToLower().Contains(search.ToLower()));
    }

    public IEnumerable<Customer> GetLastXDaysCustomer(string search, int days)
    {
        var startdate = DateTime.Today.AddDays(-days);
        var enddate = DateTime.Today;
        if (string.IsNullOrEmpty(search))
        {
            return _db.Customers.Include(o => o.Orders)
                             .Where(t => t.IsDeleted != true && t.CreatedOn >= startdate && t.CreatedOn <= enddate);
        }
        return _db.Customers.Include(o => o.Orders)
                            .Where(t => t.IsDeleted != true && t.CreatedOn >= startdate && t.CreatedOn <= enddate
                            && t.Name.ToLower().Contains(search.ToLower()));
    }

    public IEnumerable<Customer> GetAllTimeCustomer(string search)
    {
        if (!string.IsNullOrEmpty(search))
        {
            return _db.Customers.Include(o => o.Orders)
                            .Where(t => t.IsDeleted != true
                            && t.Name.ToLower().Contains(search.ToLower()));
        }
        return _db.Customers.Include(o => o.Orders)
                         .Where(t => t.IsDeleted != true);
    }

    public IEnumerable<Customer> GetCustomRangeCustomer(string search, DateTime start, DateTime end)
    {
        var defaultdatevalue = new DateTime(0001, 01, 01, 00, 00, 00);
        List<Customer> data = _db.Customers.Include(o => o.Orders)
                             .Where(t => t.IsDeleted != true).ToList();
        if (start != defaultdatevalue)
        {
            if (end == defaultdatevalue)
            {
                end = DateTime.Today;
            }
            data = data.Where(t => t.CreatedOn >= start && t.CreatedOn <= end).ToList();
        }
        if (end != defaultdatevalue)
        {
            data = data.Where(t => t.CreatedOn <= end).ToList();
        }

        if (string.IsNullOrEmpty(search))
        {
            return data;
        }
        return data.Where(t => t.Name.ToLower().Contains(search.ToLower())).ToList();

    }


    // ------------------------------------------------------------------------------------
    #region  new repo
    public Customer Read(int customerid)
    {
        try
        {
            var customer = _db.Customers.Include(c=> c.Orders).ThenInclude(c=> c.Payments).SingleOrDefault(o => o.CustomerId == customerid && o.IsDeleted != true);
            return customer ?? new Customer();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new Customer();
        }

    }

    public IEnumerable<Customer> ReadAll()
    {
        return _db.Customers.Where(o => o.IsDeleted != true).OrderBy(c => c.CustomerId);
    }

    public IEnumerable<Customer> ReadOrderProgress()
    {
        try
        {
            var customer = _db.Customers.Include(c => c.Orders.Where(o => o.OrderStatus != "completed"))
                        .Where(o=> o.IsDeleted != true).OrderBy(c => c.CustomerId).Where(o=> o.Orders.Count() >0);
            return customer;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new List<Customer>();
        }
    }

    public IEnumerable<Customer> Search(string search)
    {
        if (!string.IsNullOrEmpty(search))
        {
            return _db.Customers.Include(o => o.Orders)
                            .Where(t => t.IsDeleted != true
                            && t.Name.ToLower().Contains(search.ToLower()))
                            .OrderBy(c => c.CustomerId);
        }
        return _db.Customers.Include(o => o.Orders)
                         .Where(t => t.IsDeleted != true)
                         .OrderBy(c => c.CustomerId);
    }

    public Customer ExisitingCustomerCheck(string email)
    {
        try
        {
            return _db.Customers.SingleOrDefault(c => c.Email == email && c.IsDeleted != true) ?? new Customer();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // we need to handle the exceptions here as well if more than one with same email are there 
            return new Customer();
        }

    }

    public Customer AddUpdateCustomer(Customer customer)
    {
        try
        {
            if (customer.CustomerId == 0)
            {
                _db.Customers.Add(customer);
            }
            else
            {
                _db.Customers.Update(customer);
            }
            _db.SaveChanges();

            return _db.Customers.SingleOrDefault(c => c.Email == customer.Email) ?? new Customer();
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return new Customer();
    }


    #endregion
}
