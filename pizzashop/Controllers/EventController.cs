using Microsoft.AspNetCore.Mvc;

namespace pizzashop.Controllers;

public class EventController : Controller
{


    public EventController()
    {

    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetEventTable(DateTime startdate, DateTime enddate, string search = "", string status = "allstatus", int time = 1)
    {
        // var order = _orderService.Pagination(page: pageIndex, pageSize: pageSize, search: search, status: status, time: time, startdate: startdate, enddate: enddate,
        //                     sortname: sortname, sorttype: sorttype, sortbit: sortbit);


        return PartialView("EventTable");
    }

    public IActionResult AddEvent()
    {
        return View();
    }
}
