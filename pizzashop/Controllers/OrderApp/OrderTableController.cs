using Microsoft.AspNetCore.Mvc;
using pizzashop.Constants;
using pizzashop.data.ViewModels.OrderApp.Waiting;
using pizzashop.services.Interfaces.OrderApp;
using static pizzashop.Attributes.CustomAuthorize;

namespace pizzashop.Controllers.OrderApp;

public class OrderTableController : Controller
{
    private readonly IOrderTableServices _table;

    private readonly IWaitingListServices _waiting;

    public OrderTableController(IOrderTableServices table, IWaitingListServices waiting)
    {
        _table = table;
        _waiting = waiting;
    }

    [CustomAuthorize(UserRoles.Admin,UserRoles.Manager)]
    public IActionResult Index()
    {
        var section = _table.GetSections();

        return PartialView("../OrderApp/TablesPage", section);
    }

    public IActionResult AssignPV(int floorid, List<int> tableids)
    {
        var AssignVM = new AssignTableVM(){
        Floorid = floorid,
        Tokens = _waiting.GetWaitingList(floorid),
        Tableids = tableids,
        };

        return PartialView("../OrderApp/Table/_AssignoffcanvasPV", AssignVM);
    }

    [HttpPost]
    public IActionResult AssignTable(WaitingTokenVM token, List<int> tableids)
    {
        var orderid =_table.AssignTables(token: token, tableids: tableids);
        return Ok(orderid);
    }
}
