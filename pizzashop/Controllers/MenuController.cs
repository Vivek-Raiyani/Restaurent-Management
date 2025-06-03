using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pizzashop.Constants;
using pizzashop.data.ViewModels;
using pizzashop.services.Interfaces;
using static pizzashop.Attributes.CustomAuthorize;

namespace pizzashop.Controllers;

[ServiceFilter(typeof(PermissionFilter))]
public class MenuController : Controller
{
    private readonly IMenuService _menu;
    private readonly IUploadService _upload;

    public MenuController(IMenuService menu, IUploadService upload)
    {
        _menu = menu;
        _upload = upload;
    }
    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager)]
    public IActionResult Index()
    {
        return View("../Menu/NewMenu/Main");
    }

    #region sidebars and tables
    public IActionResult ItemSidebar()
    {
        var categories = _menu.CategorySidebar();

        return PartialView("../Menu/NewMenu/Sidebars/CategorySidebar", categories);
    }

    public IActionResult CategoryJson()
    {
        var groups = _menu.CategoryList();
        // Returning data in form of json
        return Ok(groups);
    }


    public IActionResult ModifierGroupSidebar()
    {
        var groups = _menu.ModifierSidebar();

        return PartialView("../Menu/NewMenu/Sidebars/ModifierSidebar", groups);
    }

    public IActionResult ModifierGroupSidebarJson()
    {
        var groups = _menu.ModifierSidebar().Where(c => c.ModiferCount > 0);
        return Ok(groups);
    }

    public IActionResult ItemTable(int categoryid, int page = 1, int pagesize = 3, string search = "")
    {
        var table = _menu.ItemTable(categoryid: categoryid, page: page, pagesize: pagesize, search: search);

        return PartialView("../Menu/NewMenu/DataTables/ItemTable", table);
    }

    public IActionResult ModifierTable(int groupid, int page = 1, int pagesize = 1, string search = "")
    {
        var table = _menu.ModifierTable(groupid: groupid, page: page, pagesize: pagesize, search: search);

        return PartialView("../Menu/NewMenu/DataTables/ModifierTable", table);
    }

    public IActionResult ExistingModifierTable(int page = 1, int pagesize = 5, string search = "")
    {
        var table = _menu.ModifierTable(groupid: 0, page: page, pagesize: pagesize, search: search);

        return PartialView("../Menu/NewMenu/Modals/existingmodifier", table);
    }

    #endregion

    #region  category
    public IActionResult AddEditCategory(int categoryid = 0)
    {
        if (categoryid == 0)
        {
            return PartialView("../Menu/NewMenu/Modals/editcategory");
        }

        var category = _menu.ReadCategory(categoryid: categoryid);

        return PartialView("../Menu/NewMenu/Modals/editcategory", category);
    }

    [HttpPost]
    public IActionResult AddEditCategory(CategoryVM category)
    {
        var result = _menu.AddEditCategory(category);
        // need to return resnse code with message
        if (result)
        {
            if (category.CategoryID == 0)
            {
                return Ok(new { succes = true, message = "category Added  SuccessFully" });
            }
            else
            {
                return Ok(new { succes = true, message = "category Updated  SuccessFully" });
            }
        }

        return Ok(new { succes = false, message = "Error occured while Adding" });
    }

    #endregion

    #region item 
    public IActionResult AddEditItem(int itemid = 0)
    {

        var item = new AddEditItemVM();

        if (itemid == 0)
        {
            item.Categories = _menu.CategoryList().ToList();
            return PartialView("../Menu/NewMenu/Modals/addedititem", item);
        }

        item = _menu.ReadItem(itemid: itemid);

        return PartialView("../Menu/NewMenu/Modals/addedititem", item);
    }

    public IActionResult ItemGroups(int itemid)
    {
        if (itemid != 0)
        {
            var mappings = _menu.ReadItemMappings(itemid: itemid);
            // passing mapping in form of json
            return Ok(mappings);
        }
        return Ok();
    }

    public IActionResult GetItemGroups(int groupid)
    {
        var group = _menu.ShowMapping(groupid: groupid);
        return Ok(group);
    }


    [HttpPost]
    public IActionResult AddEditItem(AddEditItemVM item)
    {
        if (item.FormFile != null)
        {
            var path = _upload.Upload(Image: item.FormFile, folder_name: item.Name);
            item.Img = $"{Request.Scheme}://{Request.Host}/{path}";
        }
        // to happen inside service
        if (!item.GroupString.IsNullOrEmpty())
        {
            var groups = JsonSerializer.Deserialize<List<ItemGroupMappingVM>>(item.GroupString);
            item.Groups = groups;
        }

        var result = _menu.AddEditItem(item);
        if (result)
        {
            if (item.ItemId == 0)
            {
                return Ok(new { succes = true, message = "Item Added  SuccessFully" });
            }
            else
            {
                return Ok(new { succes = true, message = "Item Updated  SuccessFully" });
            }

        }

        return Ok(new { succes = false, message = "Error occured" });
    }

    #endregion


    public IActionResult AddEditModifierGroup(int groupid = 0)
    {
        if (groupid == 0)
        {
            return PartialView("../Menu/NewMenu/Modals/editgroup");
        }

        var group = _menu.ReadGroup(groupid: groupid);

        return PartialView("../Menu/NewMenu/Modals/editgroup", group);
    }

    [HttpPost]
    public IActionResult AddEditModifierGroup(AddEditGroup group)
    {
        // to happen inside service
        var groups = JsonSerializer.Deserialize<List<ModifierNameVM>>(group.ModifierString);
        group.Modifier = groups;

        var result = _menu.AddEditGroup(group: group);
        if (result)
        {
            if (group.Id == 0)
            {
                return Ok(new { succes = true, message = "Group Added  SuccessFully" });
            }
            else
            {
                return Ok(new { succes = true, message = "Group Updated  SuccessFully" });
            }

        }

        return Ok(new { succes = false, message = "Error occured" });
    }

    public IActionResult AddEditModifier(int modifierid = 0)
    {

        if (modifierid == 0)
        {
            var modifierplaceholder = new AddEditModifier()
            {
                Groups = _menu.ModifierSidebar().Select(s => new ModifierVM()
                {
                    ModGroupId = s.GroupId,
                    ModifierName = s.GroupName
                }).ToList()
            };

            return PartialView("../Menu/NewMenu/Modals/editmodifier", modifierplaceholder);
        }

        var modifier = _menu.ReadModifier(modifierid: modifierid);
        return PartialView("../Menu/NewMenu/Modals/editmodifier", modifier);
    }

    [HttpPost]
    public IActionResult AddEditModifier(AddEditModifier modifier)
    {
        // need to convert it inservice
        var groups = JsonSerializer.Deserialize<List<int>>(modifier.Groupstr);
        modifier.GroupIds = groups;

        var result = _menu.AddEditModifier(modifier);
        if (result)
        {
            if (modifier.ModiferId == 0)
            {
                return Ok(new { succes = true, message = "Modifier Added  SuccessFully" });
            }
            else
            {
                return Ok(new { succes = true, message = "Modifier Updated  SuccessFully" });
            }

        }

        return Ok(new { succes = false, message = "Error occured" });

    }


    // mass delete
    [HttpDelete]
    public IActionResult Delete(MenuDelete menuDelete)
    {
        var result = _menu.Delete(menuDelete);
        if (result)
        {

            return Ok(new { succes = true, message = menuDelete.Name + "deleted  SuccessFully" });

        }

        return Ok(new { succes = false, message = "Error occured" });

    }


    #region  Constrain check Endpoint


    public IActionResult CheckConstrians(string name, string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Ok();
        }

        if (_menu.CheckConstrain(name: name, value: value))
        {
            return Ok();
        }
        return Ok(value + " is already present");
    }

    #endregion
}
