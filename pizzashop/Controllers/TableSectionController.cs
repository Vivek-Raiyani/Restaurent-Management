using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using pizzashop.Constants;
using pizzashop.data.Models;
using pizzashop.data.ViewModels.TableSection;
using pizzashop.services.Interfaces.TableSection;
using static pizzashop.Attributes.CustomAuthorize;

namespace pizzashop.Controllers;


[ServiceFilter(typeof(PermissionFilter))]
public class TableSectionController : Controller
{

    private readonly ITableServics _tableservice;
    private readonly ISectionServices _sectionservice;

    public TableSectionController(ITableServics tableservice, ISectionServices sectionservice)
    {
        _tableservice = tableservice;
        _sectionservice = sectionservice;
    }

    #region Section Crud

    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager)]
    public IActionResult Index()
    {
        var sections = _sectionservice.GetAllSections();

        return View("index", sections);
    }


    #region  Update Section
    [HttpGet]
    public IActionResult UpdateSection(int SectionId)
    {
        var section = _sectionservice.GetSectionById(SectionId);

        return PartialView("_EditSectionPV", section);
    }

    [HttpPost]
    public IActionResult UpdateSection(SectionSidebarVM section)
    {
        bool result = _sectionservice.UpdateSection(section);

        if (result)
        {
           return Ok(new { succes = true, message = "section updated SuccessFully"});
        }

        return Ok(new { succes = false, message = "Error occured while updating"});
    }
    #endregion

    #region  Delete Section
    [HttpDelete]
    public IActionResult DeleteSection(int SectionId)
    {
        bool result = _sectionservice.DeleteSection(SectionId);

        if (result)
        {
           return Ok(new { succes = true, message = "section deleted SuccessFully"});
        }

        return Ok(new { succes = false, message = "Error occured while deleted"});
    }
    #endregion

    #region  Create section

    [HttpGet]
    public IActionResult AddNewSection()
    {
        return PartialView("_AddSectionPV");
    }

    [HttpPost]
    public IActionResult AddNewSection(SectionSidebarVM section)
    {
        bool result = _sectionservice.AddNewSection(section);

        if (result)
        {
           return Ok(new { succes = true, message = "Section Added SuccessFully"});
        }

        return Ok(new { succes = false, message = "Error occured while Adding"});
    }
    #endregion

    #endregion

    #region  Section Tables

    public IActionResult GetSectionTables(int SectionId, int pageIndex = 1, int pageSize = 3, string search = "")
    {
        var SectionTables = _sectionservice.GetSectionTables(SectionId: SectionId, page: pageIndex, pageSize: pageSize, search: search);

        ViewBag.SectionId = SectionId;
        return PartialView("_SectionTablesPV", SectionTables);
    }



    #endregion


    #region Tables

    #region  Add Table
    [HttpGet]
    public IActionResult NewAddTable()
    {
        IEnumerable<SectionSidebarVM> sections = _sectionservice.GetAllSections();
        AddEditTableVM RequestModel = new()
        {
            Sections = sections
        };

        return PartialView("_AddTablePV", RequestModel);
    }

    [HttpPost]
    public IActionResult NewAddTable(AddEditTableVM tableVM)
    {
        var result = _tableservice.AddTable(tableVM);

        if (result)
        {
           return Ok(new { succes = true, message = "Table Added SuccessFully"});
        }

        return Ok(new { succes = false, message = "Error occured while Adding"});
    }

    #endregion

    #region Update Table
    [HttpGet]
    public IActionResult UpdateTable(int tableId)
    {
        IEnumerable<SectionSidebarVM> sections = _sectionservice.GetAllSections();
        AddEditTableVM table = _tableservice.GetTableVM(tableId);

        table.Sections = sections;

        return PartialView("_EditTablePV", table);
    }

    [HttpPost]
    public IActionResult UpdateTable(AddEditTableVM tableVM)
    {
        if (ModelState.IsValid)
        {
            var result = _tableservice.UpdateTable(tableVM);

            if (result)
        {
           return Ok(new { succes = true, message = "table updated SuccessFully"});
        }

        return Ok(new { succes = false, message = "Error occured while updating"});

        }
        tableVM.Sections = _sectionservice.GetAllSections();
        return PartialView("_EditTablePV", tableVM);
    }

    #endregion

    #region DeleteTable
    public IActionResult DeleteTable(int tableId)
    {
        var result = _tableservice.DeleteTable(tableId);
       if (result)
        {
           return Ok(new { succes = true, message = "table deleted  SuccessFully"});
        }

        return Ok(new { succes = false, message = "Error occured while deleting"});
    }

    [HttpDelete]
    // need to have a return type
    public IActionResult DeleteMultipleTables(List<int> tableids)
    {
        var result = _tableservice.DeleteMultipleTables(tableids);
        if (result)
        {
           return Ok(new { succes = true, message = "table deleted  SuccessFully"});
        }

        return Ok(new { succes = false, message = "Error occured while deleting"});
    }
    #endregion

    #endregion

    #region Constrain check Endpoint

    public IActionResult CheckSectionName(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            return Ok();
        }
        
         if( _sectionservice.CheckConstrain(value : value ) )
        {
            return Ok();
        }
        return Ok( value + "already exist");
    }

    public IActionResult CheckTableName(string value , int sectionId)
    {
        if(string.IsNullOrEmpty(value))
        {
            return Ok();
        }

         if( _tableservice.CheckConstrain(value : value , sectionId : sectionId) )
        {
            return Ok();
        }
        return Ok( value + "already exist");
    }

    #endregion
}
