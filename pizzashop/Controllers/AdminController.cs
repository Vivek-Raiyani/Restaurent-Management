using Microsoft.AspNetCore.Mvc;
using pizzashop.Constants;

using pizzashop.data.ViewModels;
using pizzashop.service.Interface;
using pizzashop.services.Interfaces;
using pizzashop.services.Utils;
using static pizzashop.Attributes.CustomAuthorize;

namespace pizzashop.Controllers;

public class AdminController : Controller
{
    private readonly IUserService _userservice;
    private readonly IRoleService _roleService;
    private readonly IpermissionServices _permissionServices;

    public AdminController(IUserService service, IRoleService roleService, IpermissionServices permissionServices)
    {
        _userservice = service;
        _roleService = roleService;
        _permissionServices = permissionServices;
    }


    public IActionResult UserProfile()
    {
        var user = SessionUtils.GetUser(httpContext: HttpContext);

        if (user != null)
        {
            return Ok(user.ProfileImg);
        }

        return Ok();
    }

    #region  Userlist

    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager)]
    public IActionResult UserMain()
    {
        return View("UserListMain");
    }

    public IActionResult Users(int pagesize = 3, int page = 1, int usera = 0, int userd = 0, int rolea = 0, int roled = 0)
    {
        var data = _userservice.PaginationUserList(page, pagesize, search: "", usera, userd, rolea, roled);

        ViewBag.userascending = 0;
        ViewBag.userdescending = 0;
        ViewBag.roleascending = 0;
        ViewBag.roledescending = 0;

        return PartialView(data);
    }

    [HttpPost]
    public IActionResult Users(string search, int pagesize = 3, int page = 1, int usera = 0, int userd = 0, int rolea = 0, int roled = 0)
    {
        var data = _userservice.PaginationUserList(page, pagesize, search, usera, userd, rolea, roled);

        ViewBag.userascending = usera;
        ViewBag.userdescending = userd;
        ViewBag.roleascending = rolea;
        ViewBag.roledescending = roled;

        return PartialView(data);
    }

    #endregion
    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager)]
    public IActionResult Roles()
    {
        ViewBag.roles = _roleService.GetAllRoles();

        return View();
    }

    #region  Permissions page
    [HttpGet]
    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager)]
    public IActionResult Permissions(int roleid)
    {
        var role_permission = _permissionServices.GetAllPermissions(roleid);

        return View(role_permission);
    }

    [HttpPost]
    public IActionResult Permissions(PermissionListVM permission)
    {
        _permissionServices.UpdatePermission(permission);

        return View(permission);
    }
    #endregion

    #region  dashboard

    [HttpGet]
    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager)]
    public IActionResult Dashboard( DateTime start, DateTime end,int days = 30)
    {
        return View();
    }


    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager)]
    public IActionResult DashboardPV( DateTime start, DateTime end,int days = 30)
    {
        DashboardVM dashboard =  _userservice.Dashboard(days, start, end);
        return PartialView("../Admin/DashboardPV",dashboard);
    }


    [HttpPost]
    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager)]
    public IActionResult DashboardJson( DateTime start, DateTime end,int days = 30)
    {
        DashboardVM dashboard =  _userservice.Dashboard(days, start, end);
        return Ok(dashboard);
    }

    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager)]
    public IActionResult DashboardChart (DateTime start, DateTime end,int days = 30)
    {
        return Ok(_userservice.DashboardChart(days, start, end));
    }
    #endregion
}
