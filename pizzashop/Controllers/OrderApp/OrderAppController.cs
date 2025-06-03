using Microsoft.AspNetCore.Mvc;
using pizzashop.Constants;
using static pizzashop.Attributes.CustomAuthorize;

namespace pizzashop.Controllers.OrderApp;


public class OrderAppController: Controller
{

    [CustomAuthorize(UserRoles.Admin,UserRoles.Manager,UserRoles.Chef)]
    public IActionResult KotPage ()
    {
        return View();
    }

     [CustomAuthorize(UserRoles.Admin,UserRoles.Manager)]
    public IActionResult WaitlistPage ()
    {
        return RedirectToAction("Index","WaitingList");
    }

     [CustomAuthorize(UserRoles.Admin,UserRoles.Manager)]
    public IActionResult TablesPage ()
    {
        return RedirectToAction("Index","OrderTable");
    }

     [CustomAuthorize(UserRoles.Admin,UserRoles.Manager)]
    public IActionResult MenuPage ()
    {
        return RedirectToAction("Index","OrderMenu");
    }
}
