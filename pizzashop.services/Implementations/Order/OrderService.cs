using Microsoft.EntityFrameworkCore.Metadata.Internal;
using pizzashop.data.Models;
using pizzashop.data.ViewModels;
using pizzashop.data.ViewModels.Orders;
using pizzashop.repository.Interfaces.Orders;
using pizzashop.services.Interfaces.Orders;



namespace pizzashop.services.Implementations.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepo;

    public OrderService(IOrderRepository orderRepo)
    {
        _orderRepo = orderRepo;
    }

    public int getAvgRating(Review r)
    {
        if (r != null)
        {
            return (int)((r.Ambience + r.ServiceRatings + r.Food) / 3);
        }
        else
        {
            return 0;
        }

    }

    public OrderExportVM ExportOrders(string search, string status = "", int time = 1)
    {
        var data = _orderRepo.ExportOrderData(status);
        var OrderExport = new OrderExportVM();
        OrderExport.Search = search;
        OrderExport.Status = status;
        if (!string.IsNullOrEmpty(search))
        {
            data = data.Where(t => t.Customer.Name.ToLower().Contains(search.ToLower()) || t.OrderId.ToString() == search);
        }
        var startdate = DateTime.Today;
        var enddate = DateTime.Today;
        var defaultdatevalue = new DateTime(0001, 01, 01, 00, 00, 00);
        switch (time)
        {
            case 7:
                startdate = startdate.AddDays(-7);
                OrderExport.Date = "Last 7 Days";
                data = data.Where(t => t.CreatedOn >= startdate && t.CreatedOn <= enddate);
                OrderExport.Record = data.Count();
                break;
            case 30:
                startdate = startdate.AddDays(-30);
                OrderExport.Date = "Last 30 Days";
                data = data.Where(t => t.CreatedOn >= startdate && t.CreatedOn <= enddate);
                OrderExport.Record = data.Count();
                break;
            case 0:
                var today = DateTime.Today;
                startdate = new DateTime(today.Year, today.Month, 1, 00, 00, 01);
                enddate = DateTime.Today.AddDays(1);
                OrderExport.Date = "Current Month";
                data = data.Where(t => t.CreatedOn >= startdate && t.CreatedOn <= enddate);
                OrderExport.Record = data.Count();
                break;
            default:
                OrderExport.Date = "All time";
                OrderExport.Record = data.Count();
                break;
        }

        //  OrderExport.Record = totalcount;
        List<OrderListVM> orderData = new List<OrderListVM>();
        foreach (var item in data)
        {
            var element = new OrderListVM();
            element.OrderID = item.OrderId;
            element.OrderDate = (DateTime)item.CreatedOn;
            element.Customer = item.Customer.Name;
            element.Status = item.OrderStatus;
            element.PaymentMod = item.Payments.Select(p => p.PaymentMethod).FirstOrDefault() ?? "pending";
            element.Rating = item.Review == null ? 0 : getAvgRating(item.Review);
            element.Total = (float)item.Total;
            orderData.Add(element);
        }
        OrderExport.OrderData = orderData;
        return OrderExport;
    }
    public OderDetailsVM GetOrderDetail(int orderID)
    {
        var orderdata = _orderRepo.GetOrderDetails(orderID);
        var OrderDetail = new OderDetailsVM();
        OrderDetail.OrderID = orderID;
        OrderDetail.Status = orderdata.OrderStatus;
        OrderDetail.PlacedOn = (DateTime)orderdata.CreatedOn;
        OrderDetail.ModifiedOn = orderdata.UpdatedOn ?? DateTime.Now;
        // OrderDetail.OrderDuration ;
        OrderDetail.PaidOn = (DateTime)orderdata.Payments.First().PaymentDate;
        OrderDetail.InvoiceId = orderdata.Payments.First().PayId;
        OrderDetail.PaymentMethod = orderdata.Payments.First().PaymentMethod;
        OrderDetail.OrderTotal = orderdata.Total;

        // adding customer data
        OrderDetail.Customer.Name = orderdata.Customer.Name;
        OrderDetail.Customer.Email = orderdata.Customer.Email;
        OrderDetail.Customer.Phone = orderdata.Customer.PhoneNo;
        OrderDetail.Customer.Ppl = (int)orderdata.NoOfPpl;

        // adding table and section data
        if (orderdata.OrderTables.Count() > 0)
        {
            var tables = orderdata.OrderTables;
            OrderDetail.Tables.Section = orderdata.OrderTables.First().Table.Section.SecName;
            foreach (var table in tables)
            {
                OrderDetail.Tables.TableNames.Add(table.Table.TblName);
            }
        }


        // adding order items data
        foreach (var item in orderdata.OrderDetails)
        {
            var orderitem = new OrderDetailsItemVm();
            orderitem.Name = item.Item.IteamName;
            orderitem.Quantity = (int)item.Quantity;
            orderitem.Price = (float)item.Item.Rate;
            // orderitem.total =  to be calculated

            // adding order modifers 
            foreach (var mod in item.OrderItemModifiers)
            {
                var modifier = new OrderDetailsModifierVM();
                modifier.Name = mod.Modifier.ModName;
                modifier.Price = (float)mod.Modifier.Rate;
                // modifier.total

                orderitem.Modifiers.Add(modifier);
            }

            OrderDetail.Items.Add(orderitem);
        }

        // taxes
        OrderDetail.OrderTax = (List<OrderTax>)orderdata.OrderTaxes;
        return OrderDetail;
    }

    public PaginatedListVM<OrderListVM> Pagination(int page, int pageSize, string search, string status, int time, DateTime startdate,
                        DateTime enddate, string sortname, string sorttype, int sortbit)
    {
        int count = 0;
        List<Order> orders = _orderRepo.Pagination(search: search, status: status);

        if (time != 0 && time != 1)
        {
            startdate = DateTime.Today.AddDays(-time);
            enddate = DateTime.Today.AddDays(1);
        }

        else if (time == 0)
        {
            var today = DateTime.Today;
            startdate = new DateTime(today.Year, today.Month, 1, 00, 00, 01);
            enddate = DateTime.Today.AddDays(1);
        }

        var defaultdatevalue = new DateTime(0001, 01, 01, 00, 00, 00);

        if (time != 1)
        {
            orders = orders.Where(t => t.IsDeleted != true && t.CreatedOn >= startdate && t.CreatedOn <= enddate).ToList();
        }
        else
        {
            // from the date selected
            if (startdate != defaultdatevalue)
            {
                if (enddate == defaultdatevalue)
                {
                    enddate = DateTime.Today.AddDays(1);
                }
                orders = orders.Where(t => t.IsDeleted != true && t.CreatedOn >= startdate && t.CreatedOn <= enddate).ToList();
            }
            // till the date selected
            if (enddate != defaultdatevalue)
            {
                orders = orders.Where(t => t.IsDeleted != true && t.CreatedOn <= enddate).ToList();
            }

        }
        if (sortbit == 1)
        {
            if (sorttype == "desc")
            {
                switch (sortname)
                {
                    case "order":
                        orders = orders.OrderByDescending(x => x.OrderId).ToList();
                        break;
                    case "date":
                        orders = orders.OrderByDescending(x => x.CreatedOn).ToList();
                        break;
                    case "customer":
                        orders = orders.OrderByDescending(x => x.Customer.Name).ToList();
                        break;
                    case "amount":
                        orders = orders.OrderByDescending(x => x.Payments.Select(p => p.OrderTotal).FirstOrDefault()).ToList();
                        break;
                }

            }
        }
        else
        {
            switch (sortname)
            {
                case "order":
                    orders = orders.OrderBy(x => x.OrderId).ToList();
                    break;
                case "date":
                    orders = orders.OrderBy(x => x.CreatedOn).ToList();
                    break;
                case "customer":
                    orders = orders.OrderBy(x => x.Customer.Name).ToList();
                    break;
                case "amount":
                    orders = orders.OrderBy(x => x.Payments.Select(p => p.OrderTotal).FirstOrDefault()).ToList();
                    break;
            }
        }

        count = orders.Count;
        orders = orders.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        List<OrderListVM> orderList = new();
        foreach (var item in orders)
        {
            OrderListVM element = new()
            {
                OrderID = item.OrderId,
                OrderDate = (DateTime)item.CreatedOn,
                Customer = item.Customer.Name,
                Status = item.OrderStatus,
                PaymentMod = item.OrderStatus != "pending"? item.Payments.Select(p => p.PaymentMethod).First() : "-",
                Rating = item.Review == null ? 0 : getAvgRating(item.Review),
                Total = (float)item.Total,
            };
            orderList.Add(element);
        }
        int totalPages = (int)Math.Ceiling(count / (double)pageSize);


        return new PaginatedListVM<OrderListVM>(orderList, page, totalPages, pageSize, search);
    }



}


