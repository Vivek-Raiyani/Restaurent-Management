using Microsoft.AspNetCore.Mvc;
using pizzashop.Constants;
using pizzashop.data.ViewModels.OrderApp.Waiting;
using pizzashop.services.Interfaces.OrderApp;
using static pizzashop.Attributes.CustomAuthorize;

namespace pizzashop.Controllers.OrderApp;

[CustomAuthorize(UserRoles.Admin, UserRoles.Manager)]
public class WaitingListController : Controller
{

    private readonly IWaitingListServices _waiting;
    private readonly IOrderTableServices _table;

    public WaitingListController(IWaitingListServices waiting, IOrderTableServices table)
    {
        _waiting = waiting;
        _table = table;
    }


    public IActionResult Index()
    {
        var section = _table.GetSections();

        return PartialView("../OrderApp/WaitlistPage", section);
    }

    public IActionResult TokenTable(int floorid = 0)
    {
        var tokens = _waiting.GetWaitingTable(floorid);

        return PartialView("../OrderApp/Waiting/_TokenList", tokens);
    }

    [HttpGet]
    public IActionResult AddWaitingToken(int floorid = 0)
    {
        var token = new WaitingTokenVM()
        {
            SectionVMs = _waiting.GetSections(floorid)
        };

        return PartialView("../OrderApp/Table/_AssignTableForm", token);
    }

    [HttpGet]
    public IActionResult UpdateWaitingToken(int tokenid = 0)
    {
        var token = new WaitingTokenVM();

        if (tokenid > 0)
        {
            token = _waiting.GetWaitingToken(tokenid);
            token.SectionVMs = _waiting.GetSections(token.Sectionid);
        }
        else
        {
            var floor = _waiting.GetSections(tokenid);
            token.SectionVMs = floor;
        }

        return PartialView("../OrderApp/Waiting/_NewToken", token);
    }

    [HttpGet]
    public IActionResult AssignTableModal(int floorid = 0)
    {
        var sectionVMs = _waiting.GetSections(0);

        ViewBag.tokenfloor = floorid;
        return PartialView("../OrderApp/Waiting/_AssignTableModal", sectionVMs);
    }

    public IActionResult AvailableTables(int floorid = 0)
    {
        var tableVMs = _waiting.GetAvailableTables(floorid);

        return Ok(tableVMs);
    }

    public IActionResult TokenDetails(int tokenid)
    {
        var token = _waiting.GetWaitingToken(tokenid: tokenid);

        return Ok(token);
    }

    public IActionResult TokenCount()
    {
        return Ok(_waiting.WaitinfTokenCount());
    }

    [HttpPost]
    public IActionResult AddWaitingToken(WaitingTokenVM token)
    {
        var result = _waiting.UpdateWaitingList(token);

        if (result)
        {
            TempData["success"] = "Token added successfully";
            return RedirectToAction("WaitlistPage", "OrderApp");
        }

        TempData["error"] = "Couldnt add token";
        return RedirectToAction("WaitlistPage", "OrderApp");
    }

    [HttpPost]
    public IActionResult Delete(int tokenid)
    {
        var result = _waiting.Delete(tokenid);
        
        if (result)
        {
            TempData["success"] = "Token Deleted Suceesfully";
            return RedirectToAction("WaitlistPage", "OrderApp");
        }

            TempData["error"] = "Error occured";
        return RedirectToAction("WaitlistPage", "OrderApp");
    }


    #region  check contrains

    // for fetching data of user via email
    public IActionResult CheckEmail(string email)
    {
        EmailCustomer customer = _waiting.GetEmailCustomer(email :email);
        return Ok( customer);
    }


    #endregion

}
