using Microsoft.AspNetCore.Mvc;
using pizzashop.Constants;
using pizzashop.data.ViewModels.Taxes;
using pizzashop.services.Interfaces.Taxes;
using static pizzashop.Attributes.CustomAuthorize;

namespace pizzashop.Controllers;

[ServiceFilter(typeof(PermissionFilter))]
public class TaxesController : Controller
{
    private readonly ITaxServices _taxService;

    public TaxesController(ITaxServices taxService)
    {
        _taxService = taxService;
    }


    public IActionResult TaxTable(string search = "")
    {
        var taxes = _taxService.GetAllTaxes(search);
        ViewBag.Search = search;

        return PartialView("_taxTablePV", taxes);
    }

    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager)]
    public IActionResult Index()
    {
        return View();
    }

    #region  add tax
    [HttpGet]
    public IActionResult AddTax()
    {
        return PartialView("_AddTaxPV");
    }

    [HttpPost]
    public IActionResult AddTax(TaxesListVM tax)
    {
        var result = _taxService.AddTax(tax);

        if (result)
        {
           return Ok(new { succes = true, message = "tax Added SuccessFully"});
        }

        return Ok(new { succes = false, message = "Error occured while Adding"});
    }
    #endregion

    #region edit tax

    [HttpGet]
    public IActionResult EditTax(int id)
    {
        var tax = _taxService.GetTaxById(id);
        return PartialView("_EditTaxPV", tax);
    }

    [HttpPost]
    public IActionResult EditTax(TaxesListVM tax)
    {
        var result = _taxService.EditTax(tax);

        if (result)
        {
           return Ok(new { succes = true, message = "tax Update SuccessFully"});
        }

        return Ok(new { succes = false, message = "Error occured while updating"});
    }

    #endregion

    #region  delete tax
    [HttpDelete]
    public IActionResult DeleteTax(int taxid)
    {
        var result = _taxService.DeleteTax(taxid);

        if (result)
        {
            return Ok(new { succes = true, message = "tax Deleted SuccessFully"});
        }

        return Ok(new { succes = false, message = "Error occured while deleting"});
    }
    #endregion

    #region Constrain check Endpoint

    public IActionResult CheckTaxname(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            return Ok();
        }
        
        if( _taxService.CheckConstrain(value : value ) )
        {
            return Ok();
        }
        return Ok( value + "already exist");
    }

    #endregion


}
