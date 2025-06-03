using pizzashop.data.Models;
using pizzashop.data.ViewModels;
using pizzashop.data.ViewModels.CustomerVM;
using pizzashop.repository.Interfaces.Customers;
using pizzashop.services.Interfaces.Customers;


namespace pizzashop.services.Implementations.Customers;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepo;

    public CustomerService(ICustomerRepository customerRepo)
    {
        _customerRepo = customerRepo;
    }

    public CustomerDetailsVM CustomerDetails(int CustomerId)
    {
        var customer = _customerRepo.Read(CustomerId);
        var OrderDetails = customer.Orders.Select(o => new CustomerOrderVM
        {
            OrderDate = (DateTime)o.CreatedOn,
            OrderType = o.OrderType,
            Items = o.OrderDetails.Count,
            PaymentMethod = o.Payments.Any() == true ? o.Payments.First(p => p.OrderId == o.OrderId).PaymentMethod : "pending",
            Total = (float)o.Total,
        }).ToList();



        CustomerDetailsVM CustomerDetails = new()
        {
            Name = customer.Name,
            PhoneNo = customer.PhoneNo,
            Visits = customer.Orders.Count,
            CreatedOn = (DateTime)customer.CreatedOn,
            Avgbill = GetAvgBill(customer.Orders),
            Maxbill = GetMaxBill(customer.Orders),
            OrderHistory = OrderDetails
        };

        return CustomerDetails;
    }


    public CustomerExportVM GetExportCustomer(string search, DateTime from, DateTime to, int status = 1)
    {
        // custom date other method 
        var totalcount = _customerRepo.ExportCustomerCount(search, status, from, to);
        var customers = _customerRepo.GetExportCustomers(search, status, from, to);

        CustomerExportVM customerExport = new()
        {
            Search = search,

            // OrderExport.Date = time;
            Record = totalcount
        };


        var CustomerData = customers.Select(c => new CustomerTableVM
        {
            CustomerId = c.CustomerId,
            Name = c.Name,
            PhoneNo = c.PhoneNo,
            CreatedOn = (DateTime)c.CreatedOn,
            TotalOrders = c.Orders.Count(),
            Email = c.Email,

        });

        customerExport.Date = status switch
        {
            2 => "Today",
            7 => "Last 7 Days",
            30 => "Last 30 Days",
            -1 => "CustomDate",
            0 => "Current Month",
            _ => "All time",
        };
        customerExport.Customer = CustomerData.ToList();
        return customerExport;
    }

    public static float GetAvgBill(ICollection<Order> orders)
    {
        if (orders.Count == 0)
        {
            return 0;
        }
        // var order = orders.Select(o=>o.Total);
        float avgbill = 0;
        foreach (var order in orders)
        {
            avgbill += (float)order.Total;
        }
        return avgbill / orders.Count();
    }

    public static float GetMaxBill(ICollection<Order> orders)
    {
        if (orders.Count() == 0)
        {
            return 0;
        }
        return (float)orders.OrderByDescending(o => o.Total).Select(o => o.Total).First();
    }


    public PaginatedListVM<CustomerTableVM> PaginationCustomer(int page, int pageSize, string search, int status, DateTime StartDate,
                    DateTime Enddate, string sortname = "", string sorttype = "", int sortbit = 0)
    {
        List<Customer> data = _customerRepo.Search(search).ToList();
        switch (status)
        {

            case 2:
                StartDate = DateTime.Today;
                Enddate = DateTime.Today.AddDays(1);
                data = data.Where(t => t.CreatedOn >= StartDate && t.CreatedOn <= Enddate).OrderBy(t => t.CustomerId).ToList();
                break;
            case 7:
                StartDate = DateTime.Today.AddDays(-status);
                Enddate = DateTime.Today.AddDays(1);
                data = data.Where(t => t.CreatedOn >= StartDate && t.CreatedOn <= Enddate).OrderBy(t => t.CustomerId).ToList();
                break;
            case 30:
                StartDate = DateTime.Today.AddDays(-status);
                Enddate = DateTime.Today.AddDays(1);
                data = data.Where(t => t.CreatedOn >= StartDate && t.CreatedOn <= Enddate).OrderBy(t => t.CustomerId).ToList();
                break;
            case -1:
                var defaultdatevalue = new DateTime(0001, 01, 01, 00, 00, 00);
                if (StartDate != defaultdatevalue)
                {
                    if (Enddate == defaultdatevalue)
                    {
                        Enddate = DateTime.Today;
                    }
                    data = data.Where(t => t.CreatedOn >= StartDate && t.CreatedOn <= Enddate).OrderBy(t => t.CustomerId).ToList();
                }
                if (Enddate != defaultdatevalue)
                {
                    data = data.Where(t => t.CreatedOn <= Enddate).OrderBy(t => t.CustomerId).ToList();
                }
                break;
            case 0:
                var today = DateTime.Today;
                StartDate = new DateTime(today.Year, today.Month, 1, 00, 00, 01);
                Enddate = DateTime.Today.AddDays(1);
                data = data.Where(t => t.CreatedOn >= StartDate && t.CreatedOn <= Enddate).OrderBy(t => t.CustomerId).ToList();
                break;
            default:
                break;

        }
        int count = data.Count;
        if (sortbit == 1)
        {
            if (sorttype == "desc")
            {
                switch (sortname)
                {
                    case "date":
                        data = data.OrderByDescending(x => x.CreatedOn)
                                .ToList();
                        break;
                    case "name":
                        data = data.OrderByDescending(x => x.Name)
                                    .ToList();
                        break;
                    case "orders":
                        data = data.OrderByDescending(x => x.Orders.Count)
                                    .ToList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (sortname)
                {
                    case "date":
                        data = data.OrderBy(x => x.CreatedOn).ToList();
                        break;
                    case "name":
                        data = data.OrderBy(x => x.Name)
                                    .ToList();
                        break;
                    case "orders":
                        data = data.OrderBy(x => x.Orders.Count)
                                    .ToList();
                        break;
                }
            }
        }

        data = data.Skip((page - 1) * pageSize)
                                    .Take(pageSize).ToList();

        List<CustomerTableVM> Customertables = new();
        foreach (var item in data)
        {
            var element = new CustomerTableVM();
            element.CustomerId = item.CustomerId;
            element.Name = item.Name;
            element.Email = item.Email;
            element.CreatedOn = (DateTime)item.CreatedOn;
            element.PhoneNo = item.PhoneNo;
            element.TotalOrders = item.Orders.Count;
            Customertables.Add(element);
        }
        int totalPages = (int)Math.Ceiling(count / (double)pageSize);

        return new PaginatedListVM<CustomerTableVM>(Customertables, page, totalPages, pageSize, search, count);
    }

}
