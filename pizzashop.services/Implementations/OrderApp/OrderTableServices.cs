using pizzashop.data.Models;
using pizzashop.data.ViewModels.OrderApp.Tables;
using pizzashop.data.ViewModels.OrderApp.Waiting;
using pizzashop.repository.Interfaces.Customers;
using pizzashop.repository.Interfaces.OrderApp;
using pizzashop.repository.Interfaces.Orders;
using pizzashop.repository.Interfaces.TableSection;
using pizzashop.services.Interfaces.OrderApp;

namespace pizzashop.services.Implementations.OrderApp;

public class OrderTableServices : IOrderTableServices
{
    private readonly IOrderTableRepository _ordetable;
    private readonly ISectionRepository _section;

    private readonly IOrderRepository _order;
    private readonly ITableRepository _table;
    private readonly ICustomerRepository _customer;

    private readonly IWaitingListRepository _waiting;

    public OrderTableServices(IOrderTableRepository ordetable, ISectionRepository section, ICustomerRepository customer,
            ITableRepository table, IOrderRepository order, IWaitingListRepository waiting)
    {
        _ordetable = ordetable;
        _section = section;
        _customer = customer;
        _table = table;
        _order = order;
        _waiting = waiting;
    }

    public IEnumerable<OrderSectionVM> GetSections()
    {
        var DbSections = _section.ReadAll().ToList();
        var Sections = new List<OrderSectionVM>();
        foreach (var floor in DbSections)
        {
            var section = new OrderSectionVM
            {
                Id = floor.SectionId,
                Name = floor.SecName
            };
            var tables = _ordetable.GetSectionTables(floor.SectionId);
            var tableVMS = new List<OrderTableVM>();
            foreach (var table in tables)
            {
                var tableVM = new OrderTableVM();
                tableVM.Id = table.TableId;
                tableVM.Name = table.TblName;
                tableVM.Status = table.TableStatus;
                tableVM.Capacity = (int)table.Capacity;
                if (table.OrderTables.Count > 0)
                {
                    tableVM.OrderId = (int)table.OrderTables.OrderByDescending(o => o.OrderTableId).First().OrderId;
                }
                else
                {
                    tableVM.OrderId = 0;
                }
                var subtotal = table.OrderTables.FirstOrDefault();
                if (subtotal != null)
                {
                    tableVM.OrderDuration = (DateTime)subtotal.Order.CreatedOn;
                    if ((float)subtotal.Order.Total != null)
                    {
                        tableVM.SubTotal = (float)subtotal.Order.Total;
                    }
                    else
                    {
                        tableVM.SubTotal = 0;
                    }
                }
                tableVMS.Add(tableVM);
            }
            section.Tables = tableVMS;
            Sections.Add(section);
        }
        return Sections;
    }

    public int AssignTables(WaitingTokenVM token, List<int> tableids)
    {

        List<string> customerEmails = _customer.ReadOrderProgress().Select(c => c.Email).ToList();
        List<string> tokenEmails = _waiting.GetAllWaitingList().Select(c => c.CustEmail).ToList();


         int tablecapacity =0;
        foreach (var table in tableids)
        {
            var tab = _table.GetTableById(table);
            if (tab.TableStatus != "available")
            {
                return 0;
            }
            tablecapacity += (int)tab.Capacity;
        }

        if (tablecapacity < token.Persons)
        {
            return 0;
        }



        // checking id customer exist
        var customer = _customer.ExisitingCustomerCheck(token.Email);
        bool result = false;
        if (customer.CustomerId != 0 )
        {
            // edit customer details
            customer.Name = token.Name;
            customer.PhoneNo = token.Phone;
            // customer.UpdatedOn = DateTime.Now;
            customer = _customer.AddUpdateCustomer(customer);
        }
        else
        {
            // add new customer
            var newcustomer = new Customer();
            newcustomer.Name = token.Name;
            newcustomer.PhoneNo = token.Phone;
            newcustomer.Email = token.Email;
            newcustomer.CreatedOn = DateTime.Now;
            customer = _customer.AddUpdateCustomer(newcustomer);
        }
        if (customer == null)
        {
            return 0;
        }

        var order = new Order()
        {
            CustomerId = customer.CustomerId,
            NoOfPpl = token.Persons,
            Total = 0,
            CreatedOn = DateTime.Now,
        };

        order = _order.AddNewOrder(order);
        if (order == null)
        {
            return 0;
        }

        // saving oorder tables
        foreach (var table in tableids)
        {
            var ordertable = new OrderTable()
            {
                OrderId = order.OrderId,
                TableId = table
            };
            // updating order status
            result = _ordetable.AssignOrderTable(ordertable);
            if (result)
            {
                var tabledetails = _table.GetTableById(table);
                tabledetails.TableStatus = "occupied";
                _table.UpdateTable(tabledetails);
            }
        }

        if (!result)
        {
            return 0;
        }

        // creating ppayments
        // var paymnet = new Payment()
        // {
        //     CustomerId = customer.CustomerId,
        //     OrderId = order.OrderId,
        // };

        // if present n waitlist then remove it

        var waitingtoken = _waiting.WaitlistToken(token.Email);
        if (waitingtoken != null)
        {
            result = _waiting.DeleteWaitingList(waitingtoken);
        }

        return order.OrderId;
    }


}
