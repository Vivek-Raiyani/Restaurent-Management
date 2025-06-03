using pizzashop.data.Models;
using pizzashop.data.ViewModels.OrderApp.Kot;
using pizzashop.repository.Interfaces;
using pizzashop.repository.Interfaces.OrderApp;
using pizzashop.repository.Interfaces.Orders;
using pizzashop.services.Interfaces.OrderApp;

namespace pizzashop.services.Implementations.OrderApp;

public class KotServices : IKotServices
{
    // private readonly IKotRepository _kot;
    private readonly ICategoryRepository _category;
    private readonly IOrderRepository _order;

    private readonly IOrderDetailsRepository _orderdetails;

    public KotServices(ICategoryRepository category, IOrderRepository order, IOrderDetailsRepository orderDetails)
    {
        _category = category;
        _order = order;
        _orderdetails = orderDetails;
    }


    public IEnumerable<OrderAppCategoryVM> GetCategories()
    {
        var DbCategories = _category.ReadAll();
        var categories = DbCategories.Select(
            c => new OrderAppCategoryVM()
            {
                Id = c.CategoryId,
                Name = c.MenuName,
            }
        );

        return categories;

    }

    public PaginatedKotVM<OrderCardVM> KotPagination(int categoryid, string status, int size = 1, int page = 1)
    {
        var ordercard = _order.KotOrderList().Where(o => o.OrderStatus != "completed" && o.OrderStatus != "cancelled");
        var orders = ordercard.Select(s => new OrderCardVM
        {
            Orderid = s.OrderId,
            CreatedOn = (DateTime)s.CreatedOn,
            OrderInstruction = s.OdrComment ?? "",
            Section = s.OrderTables.First().Table.Section.SecName,
            Tabels = s.OrderTables.Select(t => t.Table.TblName).ToList(),
            Items = s.OrderDetails.Select(d => new KotItemVM
            {
                OrderDetailsid = d.OrderDetailsId,
                Categoryid = d.Item.CategoryId,
                Item = d.Item.IteamName ?? string.Empty,
                Quantity = d.Quantity - d.Prepared,
                Prepared = d.Prepared,
                Instructions = d.Instructions ?? "",
                Modifier = d.OrderItemModifiers.Select(m => new KotModifiers
                {
                    Modiferid = (int)m.ModifierId,
                    Name = m.Modifier.ModName,
                }).ToList()
            }).ToList()
        }).ToList();

        if (categoryid != 0)
        {
            orders = orders.Where(o => o.Items.Where(i => i.Categoryid == categoryid).Any()).ToList();
            foreach (var order in orders)
            {
                order.Items = order.Items.Where(item => item.Categoryid == categoryid).ToList();
            }
        }
        if (status == "ready")
        {
            foreach (var order in orders)
            {
                order.Items = order.Items.Where(i => i.Prepared > 0).ToList();
            }
            orders = orders.Where(o => o.Items.Count > 0).ToList();
        }
        else
        {
            foreach (var order in orders)
            {
                order.Items = order.Items.Where(item => item.Quantity > 0).ToList();
            }
        }


        orders = orders.Where(o => o.Items.Count > 0).ToList();
        int totalPages = (int)MathF.Ceiling(orders.Count / (float)size);
        orders = orders.Skip((page - 1) * size).Take(size).ToList();

        var kotpagination = new PaginatedKotVM<OrderCardVM>(items: orders, pageIndex: page, totalPages: totalPages, pagesize: size, status: status);

        return kotpagination;

    }

    public IEnumerable<OrderCardVM> KotOrdersList(int categoryid, string status, int size = 3, int page = 1)
    {
        var ordercard = _order.KotOrderList().Where(o => o.OrderStatus != "completed" && o.OrderStatus != "cancelled");
        var orders = ordercard.Select(s => new OrderCardVM
        {
            Orderid = s.OrderId,
            CreatedOn = (DateTime)s.CreatedOn,
            OrderInstruction = s.OdrComment ?? "",
            Section = s.OrderTables.First().Table.Section.SecName,
            Tabels = s.OrderTables.Select(t => t.Table.TblName).ToList(),
            Items = s.OrderDetails.Select(d => new KotItemVM
            {
                OrderDetailsid = d.OrderDetailsId,
                Categoryid = d.Item.CategoryId,
                Item = d.Item.IteamName ?? string.Empty,
                Quantity = d.Quantity - d.Prepared,
                Prepared = d.Prepared,
                Modifier = d.OrderItemModifiers.Select(m => new KotModifiers
                {
                    // Map Modifier properties here
                    Modiferid = (int)m.ModifierId,
                    Name = m.Modifier.ModName,
                }).ToList()
            }).ToList()
        }).ToList();

        if (categoryid != 0)
        {
            orders = orders.Where(o => o.Items.Where(i => i.Categoryid == categoryid).Count() > 0).ToList();
            foreach (var order in orders)
            {
                order.Items = order.Items.Where(item => item.Categoryid == categoryid).ToList();
            }
        }
        if (status == "ready")
        {
            foreach (var order in orders)
            {
                order.Items = order.Items.Where(i => i.Prepared > 0).ToList();
            }
            return orders.Where(o => o.Items.Count > 0);
        }
        foreach (var order in orders)
        {
            order.Items = order.Items.Where(item => item.Quantity > 0).ToList();
        }


        return orders.Where(o => o.Items.Count > 0);

    }

    public OrderModalVM KotOrders(int categoryid, string status, int orderid)
    {
        var ordercard = _order.KotOrder(orderid);

        var orders = new OrderModalVM()
        {
            Orderid = ordercard.OrderId,
            Items = ordercard.OrderDetails.Where(o => o.IteamStatus != "cancelled").Select(d => new KotItemVM
            {
                OrderDetailsid = d.OrderDetailsId,
                Categoryid = d.Item.CategoryId,
                Item = d.Item.IteamName ?? string.Empty,
                Quantity = d.Quantity - d.Prepared,
                Prepared = d.Prepared,
                Modifier = d.OrderItemModifiers.Select(m => new KotModifiers
                {
                    // Map Modifier properties here
                    Modiferid = (int)m.ModifierId,
                    Name = m.Modifier.ModName,
                }).ToList()
            }).ToList()
        };

        if (categoryid != 0)
        {
            orders.Items = orders.Items.Where(i => i.Categoryid == categoryid).ToList();
        }
        if (status == "ready")
        {
            orders.Items = orders.Items.Where(i => i.Prepared > 0).ToList();
            return orders;
        }
        orders.Items = orders.Items.Where(item => item.Quantity > 0).ToList();


        return orders;

    }


    public bool UpdateKotOrder(KotOrderUpdate order)
    {
        foreach (var odr in order.Detailsids)
        {
            var details = _orderdetails.Read(odr.Detailsid);
            if (order.Status == "ready")
            {
                details.Prepared += (short)odr.Quantity;
            }
            else
            {
                details.Prepared -= (short)odr.Quantity;
            }
            var result = _orderdetails.AddUpdate(details);
            if (!result)
            {
                return false;
            }
        }
        return true;
    }
}
