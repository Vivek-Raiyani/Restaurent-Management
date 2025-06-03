using Org.BouncyCastle.Math.EC.Rfc7748;
using pizzashop.data.Models;
using pizzashop.data.ViewModels;
using pizzashop.repository.Interfaces;
using pizzashop.repository.Interfaces.OrderApp;
using pizzashop.repository.Interfaces.Orders;
using pizzashop.repository.Interfaces.Tax;
using pizzashop.services.Interfaces.OrderApp;

namespace pizzashop.services.Implementations.OrderApp;

public class OrderMenuServices : IOrderMenuServices
{
    private readonly ICategoryRepository _category;
    private readonly IItemRepository _item;
    private readonly IOrderRepository _order;
    private readonly IOrderTaxRepository _ordertax;
    private readonly IReviewRepository _review;
    private readonly IOrderDetailsRepository _details;
    private readonly ITaxRepository _tax;

    private readonly IItemModifierGroupMappingRepository _itemgroup;
    public OrderMenuServices(ICategoryRepository category, IItemRepository item, IOrderRepository order, IItemModifierGroupMappingRepository itemgroup,
                                IOrderDetailsRepository details, ITaxRepository tax, IReviewRepository review, IOrderTaxRepository ordertax)
    {
        _category = category;
        _item = item;
        _order = order;
        _itemgroup = itemgroup;
        _tax = tax;
        _details = details;
        _review = review;
        _ordertax = ordertax;
    }

    public MenuCategories OrderAppMenuCategories()
    {
        var menu = new MenuCategories();
        menu.Cateogries = _category.ReadAll().Select(c => new CategoryVM()
        {
            CategoryID = c.CategoryId,
            CategoryName = c.MenuName
        }).ToList();
        return menu;
    }

    public List<CategoryItems> CategoryItems(int categoryid, string search)
    {
        IEnumerable<MenuItem> item;
        if (categoryid == 0)
        {
            item = _item.ReadWithoutCategory(search: search);
        }
        else if (categoryid == -1)
        {
            item = _item.ReadWithoutCategory(search: search).Where(i => i.IsFavourite == true);
        }
        else
        {
            item = _item.ReadAll(categoryid: categoryid, search: search);
        }

        return item.Select(c => new CategoryItems()
        {
            Id = c.IteamId,
            Name = c.IteamName,
            Img = c.IteamImg,
            Type = c.ItemType,
            IsFav = (bool)c.IsFavourite,
            Price = (float)c.Rate,
        }).ToList();
    }

    public List<ItemGroups> ItemGroups(int itemid)
    {
        return _itemgroup.ReadItemGroups(itemid: itemid).Select(i => new ItemGroups()
        {
            GroupName = i.Modifer.ModifierName,
            Max = i.Maximum,
            Min = i.Minimun,
            Modifiers = i.Modifer.ModifierGroupMappings.Select(m => new GroupMods()
            {
                Name = m.Modifier.ModName,
                Price = (float)m.Modifier.Rate,
                ModifierId = (int)m.ModifierId
            }).ToList()
        }).ToList();
    }

    public string FavToggle(int itemid)
    {
        MenuItem item = _item.Read(itemid);
        if (item != null)
        {
            item.IsFavourite = item.IsFavourite ? false : true;
            return _item.Update(item) ? "" : "error occored";
        }
        return "item not found";

    }

    public OrderDisplay OrderDisplay(int orderid)
    {
        var order = _order.OrderAppDetails(orderid: orderid);
        var taxes = _tax.ReadAll();
        var details = new OrderDisplay()
        {
            Orderid = orderid,
            CustomerId = (int)order.CustomerId,
            Instruction = order.OdrComment,
            OrderStauts= order.OrderStatus,
            FloorName = order.OrderTables.First().Table.Section.SecName,
            TableNames = order.OrderTables.Select(m => m.Table.TblName).ToList(),

            Items = order.OrderDetails.Where(i => i.IteamStatus != "cancelled").Select(o => new OrderDetailsVM()
            {
                OrderId = orderid,
                DetailsId = o.OrderDetailsId,
                ItemId = o.ItemId,
                Name = o.Item.IteamName,
                Quantity = o.Quantity,
                Prepared = o.Prepared,
                Price = o.Item.Rate,
                Instructions = o.Instructions,
                DefaultTax = o.Item.DefaultTax,
                TaxPercentage = o.Item.TaxPercentage,

                Modifiers = o.OrderItemModifiers.Select(m => new OrderModifiers()
                {
                    ModiferId = m.ModifierId,
                    Name = m.Modifier.ModName,
                    Price = (float)m.Modifier.Rate,
                }).ToList()

            }).ToList(),

            Taxes = taxes.Select(t => new OrderTaxis()
            {
                Name = t.TaxName,
                Amount = t.TaxAmount,
                IsDefault = t.IsDefault,
                TaxId = t.TaxId,
                Type = t.TaxType
            }).ToList()
        };

        var dbtaxids = order.OrderTaxes.Select(s => s.TaxId).ToList();
        foreach (var tax in details.Taxes)
        {
            if (dbtaxids.Contains(tax.TaxId))
            {
                var newtax = order.OrderTaxes.First(t => t.TaxId == tax.TaxId);
                if (newtax != null)
                {
                    tax.OrderTaxid = newtax.OrderTaxId;
                }
            }
        }

        details.PaymentMethod = order.Payments.Any() ? order.Payments.First().PaymentMethod : "cash";

        return details;
    }

    // only details with modifer and  taxes
    public bool OrderUpdate(List<UpdateOrderDetails> items, List<int> tax, int orderid)
    {
        var orderitems = items.Select(i => new OrderDetail()
        {
            OrderDetailsId = i.DetailsId,
            OrderId = i.OrderId,
            ItemId = i.Itemid,
            Instructions = i.Instruction,
            Quantity = (short)i.Quantity,
            OrderItemModifiers = i.ModifierIds.Select(m => new OrderItemModifier()
            {
                ModifierId = m,
                OrderDetailsId = i.DetailsId,
            }).ToList()
        });
        var additems = orderitems.Where(i => i.OrderDetailsId == 0).ToList();
        var updateitems = orderitems.Where(i => i.OrderDetailsId > 0).ToList();

        var result = _details.AddUpdateMany(details: additems, status: 0);
        if (!result)
        {
            return false;
        }
        result = _details.AddUpdateMany(details: updateitems, status: 1);
        if (!result)
        {
            return false;
        }
        // order tax repo left
        // var taxes = _tax.ReadAllNonDefault().Where(x=> tax.Contains(x.TaxId)).Select(t=> new OrderTax(){
        //     OrderId = ,
        //     TaxId = ,
        // }).ToList();

        var taxpresent = _ordertax.ReadAll(orderid: orderid).Select(x => x.TaxId);
        var addtax = _tax.ReadAll().Where(t => !taxpresent.Contains(t.TaxId))
                        .Select(t => new OrderTax()
                        {
                            OrderId = orderid,
                            TaxId = t.TaxId,
                            // is deleted filed
                        })
                        .ToList();
        result = _ordertax.AddUpdateMany(Taxes: addtax, status: 0);
        if (!result)
        {
            return false;
        }
        return true;
    }

    public string DeleteOrderItem(int detailsid)
    {
        OrderDetail details = _details.Read(detailsid: detailsid);
        if (details != null)
        {
            if (details.Prepared > 0)
            {
                return "can't delete item that are prepared ";
            }
            else
            {
                details.IteamStatus = "cancelled";
                return _details.AddUpdate(details) ? "" : "error occured";
            }
        }
        else
        {
            return "no such item found";
        }
    }

    public int CheckPrepared(int detailsid)
    {
        OrderDetail details = _details.Read(detailsid: detailsid);
        if (details != null)
        {
            return details.Prepared;
        }
        else
        {
            return -1;
        }
    }
    public bool SaveOrder(SaveOrderVM order)
    {
        bool result = true;

        var dbOrder = _order.OrderAppDetails(orderid: order.OrderId);
        var oldDetailsids = dbOrder.OrderDetails.Select(s => s.OrderDetailsId).ToList();

        foreach (var id in oldDetailsids)
        {
            var newDetails = order.Details.Where(o => o.DetailsId == id).FirstOrDefault();
            var oldDetails = dbOrder.OrderDetails.FirstOrDefault(s => s.OrderDetailsId == id);

            if (newDetails != null) // update quantity of item
            {
                oldDetails.Quantity = (short)(newDetails.Quantity >= oldDetails.Prepared ? newDetails.Quantity : oldDetails.Quantity);//could use single or default
                order.Details.Remove(newDetails);
                // error message if the quantity is reduced than one prepared
            }
            else // delete item logic
            {
                if (oldDetails.Prepared > 0)
                {
                    // return an error msg
                }
                else
                {
                    oldDetails.IteamStatus = "cancelled";
                }
            }
        }

        foreach (var details in order.Details)
        {
            var newDetails = new OrderDetail()
            {
                OrderId = order.OrderId,
                ItemId = details.ItemId,
                Quantity = (short)details.Quantity,
                Instructions = details.Instruct,
                OrderItemModifiers = details.ModifierIds.Select(s => new OrderItemModifier()
                {
                    ModifierId = s
                }).ToList()
            };
            dbOrder.OrderDetails.Add(newDetails);
        }

        dbOrder.OdrComment = order.OrderInstruct;

        var taxids = order.Tax.Select(t => t.TaxId).ToList();
        foreach (var tax in dbOrder.OrderTaxes)
        {
            if (taxids.Contains(tax.TaxId))
            {
                var newtax = order.Tax.First(t => t.TaxId == tax.TaxId);
                if (newtax != null)
                {
                    tax.Amount = newtax != null ? newtax.Amount : tax.Amount;
                    order.Tax.Remove(newtax);
                }
            }
            else
            {
                tax.IsDeleted = true;

            }
        }

        foreach (var tax in order.Tax)
        {
            OrderTax ordertax = new()
            {
                TaxId = tax.TaxId,
                Amount = tax.Amount
            };
            dbOrder.OrderTaxes.Add(ordertax);
        }

        foreach(var table in dbOrder.OrderTables){
            if(dbOrder.OrderDetails.Any()){
                table.Table.TableStatus = "running";
            }
        }

        if (dbOrder.Payments.Any())
        {
            foreach (var pay in dbOrder.Payments)
            {
                pay.CustomerId = dbOrder.CustomerId;
                pay.OrderId = dbOrder.OrderId;
                pay.PaymentMethod = order.PaymentMode ?? "cash";
                // pay.OrderTotal = order.Total;
                pay.PaymentDate = DateTime.Now;
            }
        }
        else
        {
            Payment payment = new()
            {
                PaymentMethod = order.PaymentMode ?? "cash",
                PaymentDate = DateTime.Now,
                CustomerId = dbOrder.CustomerId,
                OrderId = order.OrderId
            };
            dbOrder.Payments.Add(payment);
        }

        order.PaymentMode = dbOrder.Payments.First() != null ? dbOrder.Payments.First().PaymentMethod : "cash";

        _order.UpdateOrder(dbOrder);

        return result;
    }


    //  to add reviews
    public bool OrderReview(OrderReview review)
    {
        var orderreiew = new Review()
        {
            OrderId = review.OrderId,
            CustomerId = review.CustomerId,
            Food = review.Food,
            Ambience = review.Ambience,
            Descript = review.Descript ?? "",
            ServiceRatings = review.Service,
        };

        _review.AddUpdate(orderreiew);
        return true;
    }

    // complete order
    public bool CheckOrderComplete(int orderId)
    {
        Order orderdata = _order.OrderAppDetails(orderId);
        foreach (var details in orderdata.OrderDetails)
        {
            if (details.Prepared == details.Quantity)
            {
                continue;
            }
            return false;
        }
        return true;
    }

    public bool OrderComplete(CompleteOrderVM order)
    {
        var dbOrder = _order.OrderAppDetails(orderid: order.OrderId);
        dbOrder.OrderStatus = "completed";
        dbOrder.Total = order.Total;

        foreach (var tax in order.Tax)
        {
            OrderTax ordertax = new()
            {
                TaxId = tax.TaxId,
                Amount = tax.Amount
            };
            dbOrder.OrderTaxes.Add(ordertax);
        }

        // Payment payment = new()
        // {
        //     PaymentMethod = order.PaymentMode ?? "cash",
        //     OrderTotal = order.Total,
        //     PaymentDate = DateTime.Now,
        //     CustomerId = dbOrder.CustomerId,
        //     OrderId = order.OrderId
        // };
        foreach (var pay in dbOrder.Payments)
        {
            pay.PaymentMethod = order.PaymentMode ?? "cash";
            pay.OrderTotal = order.Total;
            pay.PaymentDate = DateTime.Now;
        }

        foreach (var table in dbOrder.OrderTables)
        {
            table.Table.TableStatus = "available";
        }

        var result = _order.UpdateOrder(dbOrder);

        if (result)
        {
            return true;
        }
        return false;
    }

    public bool OrderCancel(int orderid)
    {
        Order orderdata = _order.OrderAppDetails(orderid);
        foreach (var details in orderdata.OrderDetails)
        {
            if (details.Prepared > 0)
            {
                return false;
            }
        }
        orderdata.Total = 0;
        orderdata.IsDeleted = true;
        _order.UpdateOrder(orderdata);
        return true;
    }

    public MenuCustomerDetail OrderCustomer(int customerId, int orderId)
    {
        var customer = _order.ReadOrderCustomer(orderId: orderId, customerId: customerId);
        var details = new MenuCustomerDetail()
        {
            Name = customer.Customer.Name,
            Email = customer.Customer.Email,
            Phone = customer.Customer.PhoneNo,
            Persons = customer.NoOfPpl,
            CustomerId = customerId
        };
        return details;
    }
}
