using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Microsoft.AspNetCore.Http;
using pizzashop.services.Interfaces;
using pizzashop.service.Interface;
using pizzashop.data.ViewModels;

public class PermissionFilter : IActionFilter
{
    private readonly IRoleService _roleService;
    private readonly IJwtService _jwtService;
    private readonly IpermissionServices _permission;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PermissionFilter(IRoleService roleService, IJwtService jwtService, IHttpContextAccessor httpContextAccessor, IpermissionServices permission)
    {
        _roleService = roleService;
        _jwtService = jwtService;
        _httpContextAccessor = httpContextAccessor;
        _permission = permission;
    }


    public void OnActionExecuting(ActionExecutingContext context)
    {

        var userRoleId = 0;
        var principal = _jwtService.ValidateToken(_httpContextAccessor.HttpContext.Request.Cookies["SuperSecretAuthToken"]);
        if (principal == null)
        {
            context.Result = new RedirectResult("/Validation/Login");
            return;
        }
        if (principal != null)
        {
            userRoleId = _roleService.GetRoleId(principal.FindFirst("Role").Value);
        }

        var rolePermissions = _permission.GetAllPermissions(userRoleId);

        var actionName = context.ActionDescriptor.RouteValues["controller"];

        bool canView = CheckUserPermission(actionName, rolePermissions, permissionType: "CanView");
        bool canAddEdit = CheckUserPermission(actionName, rolePermissions, permissionType: "CanAddEdit");
        bool canDelete = CheckUserPermission(actionName, rolePermissions, permissionType: "CanDelete");

        // If the action doesn't meet permission criteria, return "Permission Denied"
        if (!canView)
        {
            context.Result = new ForbidResult();
            return;
        }

        // Continue normal execution
        context.HttpContext.Items["CanAddEdit"] = canAddEdit;
        context.HttpContext.Items["CanDelete"] = canDelete;

        var method = context.HttpContext.Request.Method;

        bool isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest" ||
                context.HttpContext.Request.Headers["Accept"].ToString().Contains("application/json");

        if (method == "POST")
        {

            if (!Convert.ToBoolean(context.HttpContext.Items["CanAddEdit"]))
            {
                

                if (isAjax)
                {
                    context.Result = new JsonResult(new { error = "Permission Denied" })
                    {
                        StatusCode = StatusCodes.Status403Forbidden,
                        ContentType = "application/json"
                    };
                }
                else
                {
                    context.HttpContext.Items["Error"] = "Permission Denied";
                    context.HttpContext.Response.Headers.Add("X-Toastr-Message", "Permission Denied");
                    context.HttpContext.Response.Headers.Add("X-Toastr-Type", "error");
                    context.Result = new RedirectResult(context.HttpContext.Request.Headers["Referer"].ToString());
                }
            }
            return;
        }

        if (method == "DELETE")
        {
            if (!Convert.ToBoolean(context.HttpContext.Items["CanDelete"]))
            {
                if (isAjax)
                {
                    context.Result = new JsonResult(new { error = "Permission Denied" })
                    {
                        StatusCode = StatusCodes.Status403Forbidden,
                        ContentType = "application/json"
                    };
                }
                else
                {
                    context.HttpContext.Items["Error"] = "Permission Denied";
                    context.HttpContext.Response.Headers.Add("X-Toastr-Message", "Permission Denied");
                    context.HttpContext.Response.Headers.Add("X-Toastr-Type", "error");
                    context.Result = new RedirectResult(context.HttpContext.Request.Headers["Referer"].ToString());
                }
            }
            return;
        }

    }



    public void OnActionExecuted(ActionExecutedContext context)
    {
        // No action needed after the action executes
    }

    private bool CheckUserPermission(string actionName, PermissionListVM rolePermissions, string permissionType)
    {
        return rolePermissions.Permissions.Any(rp =>
            rp.Name == actionName &&
            (permissionType == "CanView" && rp.CanView ||
             permissionType == "CanAddEdit" && rp.CanEdit ||
             permissionType == "CanDelete" && rp.CanDelete));
    }
}

