using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using pizzashop.Constants;
using pizzashop.data.Models;
using pizzashop.data.ViewModels;
using pizzashop.service.Interface;
using pizzashop.services.Interfaces;
using pizzashop.services.Utils;
using static pizzashop.Attributes.CustomAuthorize;

namespace pizzashop.Controllers;

[ServiceFilter(typeof(PermissionFilter))]
public class UserController : Controller
{
    private readonly IUserService _userservice;
    private readonly IRoleService _roleservice;

    private readonly IEmailService _emailServices;
    private readonly IUploadService _uploadservice;

    public UserController(IUserService service, IRoleService roleService, IUploadService uploadservice, IEmailService emailServices)
    {
        _userservice = service;
        _roleservice = roleService;
        _uploadservice = uploadservice;
        _emailServices = emailServices;
    }

    #region  Add 
    [HttpGet]
    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager)]
    public IActionResult AddUser()
    {
        var profile = new ProfileVM()
        {
            Roles = _roleservice.GetAllRoles()
        };
        return View(profile);
    }

    [HttpPost]
    public IActionResult AddUser(ProfileVM profile)
    {
        ModelState.Remove("Password");
        if (ModelState.IsValid)
        {
            // handling image upload
            if (profile.MyImage != null)
            {
                var path = _uploadservice.Upload(Image: profile.MyImage, folder_name: profile.Username);
                profile.ProfileImg = $"{Request.Scheme}://{Request.Host}/{path}";
            }

            // adding user to db
            // need to handle exception here
            _userservice.AddUser(profile);
            // if user is not added the no email tosend
            // email sending connection
            try
            {
                _emailServices.NewUserMail(profile);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("UserMain", "Admin");
            }

            TempData["success"] = "User Added Succesfully";
            return RedirectToAction("UserMain", "Admin");
        }

        // error message
        var message = string.Join(" | ", ModelState.Values
        .SelectMany(v => v.Errors)
        .Select(e => e.ErrorMessage));

        TempData["error"] = message;
        profile.Roles = _roleservice.GetAllRoles();
        return View(profile);
    }
    #endregion

    #region  Edit user

    [HttpGet]
    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager)]
    public IActionResult EditUser(string email)
    {
        var user = _userservice.GetUser(email);
        user.Roles = _roleservice.GetAllRoles();

        return View(user);
    }


    [HttpPost]
    public IActionResult EditUser(ProfileVM profile)
    {
        ModelState.Remove("Password");
        if (ModelState.IsValid)
        {
            // handling the img upload
            if (profile.MyImage != null)
            {
                var path = _uploadservice.Upload(Image: profile.MyImage, folder_name: profile.Username);
                profile.ProfileImg = $"{Request.Scheme}://{Request.Host}/{path}";
            }

            // edit user 
            _userservice.EditUser(profile);

            ViewBag.roles = _roleservice.GetAllRoles();
            TempData["success"] = "User Edit Succesfully";
            return RedirectToAction("UserMain", "Admin");
        }
        // error message to be passed
        TempData["error"] = "error Occured";
        profile.Roles = _roleservice.GetAllRoles();
        return View(profile);

    }
    #endregion

    #region Delete User
    [HttpDelete]
    public IActionResult DeleteUser(string email)
    {
        try
        {
            _userservice.RemoveUser(email);
            // TempData["success"] = "User Deleted Succesfully";
            return Ok(new { success = true, message = "User Deleted Succesfully" });
        }
        catch (Exception e)
        {
            // TempData["error"] = "error " + e.Message + "Occured";
            // return RedirectToAction("Users", "Admin");
            return Ok(new { error = true, message = "Error occured while Deleting" });
        }
    }
    #endregion

    #region  Profile
    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager, UserRoles.Chef)]
    public IActionResult Profile()
    {
        var profile = SessionUtils.GetUser(HttpContext);

        return View(profile);
    }

    [HttpPost]
    public IActionResult Profile(ProfileVM user)
    {
        ModelState.Remove("Password");
        if (ModelState.IsValid)
        {
            // handling uploads
            if (user.MyImage != null)
            {
                var path = _uploadservice.Upload(Image: user.MyImage, folder_name: user.Username);
                user.ProfileImg = $"{Request.Scheme}://{Request.Host}/{path}";
            }

            _userservice.EditUser(user);

            TempData["success"] = "Profile Updated Succesfully";
            return View(user);
        }

        TempData["error"] = "Error Occures";
        return View(user);
    }


    #endregion

    #region Change Password
    [HttpGet]
    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager, UserRoles.Chef)]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager, UserRoles.Chef)]
    public IActionResult ChangePassword(PasswordChangeVM pass)
    {

        if (ModelState.IsValid)
        {
            var sessionUser = SessionUtils.GetUser(HttpContext);
            var result = _userservice.ChangePassword(sessionUser, pass.NewPassword, oldpass: pass.Password);
            if (result)
            {
                if (User.IsInRole("Chef"))
                {
                    TempData["success"] = "Password Changed Succesfully";
                    return RedirectToAction("KotPage", "OrderApp");
                }
                TempData["success"] = "Password Changed Succesfully";
                return RedirectToAction("UserMain", "Admin");
            }

        }
        TempData["error"] = "Old Password didnt match";
        return View();
    }

    #endregion

    #region  Constrain check Endpoint
    // fileds -- username, email, phone
    public IActionResult CheckConstrians(string field, string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Ok();
        }

        if (_userservice.CheckConstrians(field: field, value: value))
        {
            return Ok();
        }
        return Ok(field + " is already taken");
    }

    #endregion
}
