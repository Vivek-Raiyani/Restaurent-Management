using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pizzashop.Constants;
using pizzashop.data.ViewModels;
using pizzashop.services.Interfaces.OrderApp;
using static pizzashop.Attributes.CustomAuthorize;

namespace pizzashop.Controllers.OrderApp;

public class OrderMenuController :Controller
{
    private readonly IOrderMenuServices _menu;

    public OrderMenuController (IOrderMenuServices menu)
    {
        _menu = menu;
    }

    [CustomAuthorize(UserRoles.Admin,UserRoles.Manager)]
    public IActionResult Index(int orderid=0)
    {   
        if(orderid==0)
        {
            var categories = _menu.OrderAppMenuCategories();
            categories.OrderId = orderid;

            return PartialView("../OrderApp/MenuPage",categories);
        }
        else
        {
            var categories = _menu.OrderAppMenuCategories();
            categories.OrderId = orderid;

            return PartialView("../OrderApp/MenuPage",categories);
        }
       
    }
    public IActionResult MenuItems(int categoryid, string search,int orderid = 0)
    {
        if(orderid ==0)
        {
            ViewBag.order = false;
        }
        else
        {
            ViewBag.order=true;
        }
        var items = _menu.CategoryItems(categoryid:categoryid, search:search);

        return PartialView("../OrderApp/Menu/Itemdisplay",items);
    }

    public IActionResult FavToggle(int itemid)
    {
        string result = _menu.FavToggle(itemid);
        if(result.IsNullOrEmpty()){
            return Ok();
        }
        return Ok(result);
    }

    public IActionResult OrderDetails( int orderid)
    {
        var OrderDetail = _menu.OrderDisplay(orderid:orderid);
       
        return PartialView("../OrderApp/Menu/OrderPreview",OrderDetail);
    }

    public IActionResult ItemModiferModal(int itemid)
    {
        var groups = _menu.ItemGroups(itemid:itemid);
     
        return PartialView("../OrderApp/Menu/modifiermodal",groups);
    }

    public IActionResult CustomeDetailModal(int customerId , int orderId)
    {
        var customer = _menu.OrderCustomer(orderId:orderId, customerId:customerId);
    
        return PartialView("../OrderApp/Menu/customerdetail", customer);
    }


    public IActionResult UpdateOrder(SaveOrderVM order)
    {
        // insde service
        var Details =  JsonSerializer.Deserialize<List<OrderItemVM>>(order.DetailsString);
        foreach(var item in Details){
            item.ModifierIds = JsonSerializer.Deserialize<List<int>>(item.ModifierStr);
        }
        order.Details = Details;
        order.Tax =  JsonSerializer.Deserialize<List<OrderTaxSave>>(order.TaxString);
        _menu.SaveOrder(order);
        // return message
        return Ok();
    }

    // delete item endpoint

    public IActionResult CompleteOrder(CompleteOrderVM order)
    {
        // inside service
        order.Tax =  JsonSerializer.Deserialize<List<OrderTaxSave>>(order.TaxString);
        
        _menu.OrderComplete(order:order);
        
        return Ok();
    }

    public IActionResult CancelOrder(int orderId)
    {
        return Ok(_menu.OrderCancel(orderId));
    }

    public IActionResult DeleteItem(int Detailsid)
    {
        
        return Ok(_menu.DeleteOrderItem(Detailsid));
    }

    public IActionResult CheckPrepared(int Detailsid)
    {
        return Ok(_menu.CheckPrepared(Detailsid));
    }

    public IActionResult CheckOrderComplete(int orderId)
    {
        bool result = _menu.CheckOrderComplete(orderId);
        return Ok(result);
    }

    public IActionResult CustomerReview(OrderReview review ){
        return Ok(_menu.OrderReview(review));
    }
}
