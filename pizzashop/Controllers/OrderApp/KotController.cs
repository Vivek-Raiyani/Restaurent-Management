using Microsoft.AspNetCore.Mvc;
using pizzashop.data.ViewModels.OrderApp.Kot;
using pizzashop.services.Interfaces.OrderApp;

namespace pizzashop.Controllers.OrderApp;

public class KotController : Controller
{
    private readonly IKotServices _kot;

    public KotController(IKotServices kot)
    {
        _kot = kot;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _kot.GetCategories();
        return Ok(categories);
    }


    [HttpGet]
    public IActionResult OrderList(int id, string status, int page = 1)
    {
        var kot = _kot.KotPagination(id, status, page:page);

        ViewBag.status = status;
        return PartialView("../OrderApp/Kot/_OrderListPV", kot);
    }

    [HttpGet]
    public IActionResult KotOrderModal(int categoryid, string status, int orderid)
    {
        var kot = _kot.KotOrders(categoryid: categoryid, status: status, orderid: orderid);

        ViewBag.status = status;
        return PartialView("../OrderApp/Kot/_KotStatusModalPV", kot);
    }

    // modal data endpoint hit
    [HttpPost]
    public IActionResult KotOrderModal(KotOrderUpdate order)
    {
        var result = _kot.UpdateKotOrder(order);
        if (result)
        {
            ViewData["success"] = "ready";
            return RedirectToAction("KotPage", "OrderApp");
        }

        ViewData["error"] = "error";
        return RedirectToAction("KotPage", "OrderApp");
    }



}
