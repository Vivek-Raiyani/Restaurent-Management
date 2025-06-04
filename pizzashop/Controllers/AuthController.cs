using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pizzashop.data.ViewModels;
using pizzashop.service.Interface;
using pizzashop.services.Interfaces;
using pizzashop.services.Utils;

namespace pizzashop.Controllers;

public class AuthController : Controller
{

    private readonly IUserService _userservice;
    private readonly IEmailService _mailService;

    private readonly IPasswordHash _passwordHash;
    private readonly IRoleService _roleService;

    private readonly IJwtService _jwtService;

    public AuthController(IUserService service, IEmailService mailService, IJwtService jwtService, IRoleService roleServicer, IPasswordHash passwordHash)
    {
        _userservice = service;
        _mailService = mailService;
        _jwtService = jwtService;
        _roleService = roleServicer;
        _passwordHash = passwordHash;
    }

    [HttpGet]
    public IActionResult Index()
    {
        if (!string.IsNullOrEmpty(Request.Cookies["UserData"]))
        {
            var user_ = _userservice.GetUser(Request.Cookies["UserData"] ?? "");
            SessionUtils.SetUser(HttpContext, user_);

            var token = _jwtService.GenerateJwtToken(user_.Email, user_.RoleName ?? "");
            CookieUtils.SaveJWTToken(Response, token);

            return RedirectToAction("Dashboard", "Admin");
        }

        CookieUtils.ClearCookies(HttpContext);

        return View("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(AuthVM user)
    {
        if (ModelState.IsValid)
        {
            var user_ = _userservice.GetUser(user.Email);
            if (user_ != null)
            {
                if (!user_.Status)
                {
                    TempData["error"] = "Contact Admin to get access";
                    return RedirectToAction("Index");
                }

                PasswordVerificationResult password_is_correct_1 = _passwordHash.PasswordVerificationResult(user.Password, user_);

                if (user_ != null && password_is_correct_1 == PasswordVerificationResult.Success)

                {
                    if (user.RememberMe)
                    {
                        CookieUtils.SaveUserData(HttpContext.Response, user.Email);
                    }

                    var token = _jwtService.GenerateJwtToken(user.Email, user_.RoleName);
                    if (token == null)
                    {
                        ModelState.AddModelError("Login", "Error occurened in login");

                        return View(user);
                    }
                    // Store token in cookie
                    CookieUtils.SaveJWTToken(Response, token);
                    // store user in session
                    SessionUtils.SetUser(HttpContext, user_);

                    // var jwt_token = Request.Cookies["SuperSecretAuthToken"];

                    if (user_.RoleName == "Chef")
                    {
                        TempData["success"] = "login success ";
                        return RedirectToAction("KotPage", "OrderApp");
                    }
                    TempData["success"] = "login success ";
                    return RedirectToAction("Dashboard", "Admin");

                }
                ModelState.AddModelError("Password", "Password is not correct");
                return View(user);
            }
            ModelState.AddModelError("Email", "Email not found");
            return View(user);

        }
        return View();

    }


    public IActionResult Logout()
    {
        SessionUtils.ClearSession(HttpContext);
        CookieUtils.ClearCookies(HttpContext);

        TempData["success"] = "Logout SuccesFull";
        return RedirectToAction("Index", "Auth");
    }

    [HttpGet]
    public IActionResult ForgetPass()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ResetPass()
    {
        var Email = Request.Query["Email"];
        var Token = Request.Query["Token"];

        ResetPassVM user = new ResetPassVM();
        user.Email = Email;
        user.Token = Int32.Parse(Token);

        return View(user);
    }

    [HttpPost]
    public IActionResult ResetPass(ResetPassVM reset)
    {
        var result = _userservice.VerifiyToken(reset.Email, reset.Token);
        if (!result)
        {
            TempData["error"]="token has expired";
            return RedirectToAction("ForgetPass");
        }

        if (reset.NewPassword == reset.Password)
        {
            var user = _userservice.GetUser(reset.Email);
            var phash = _passwordHash.PassHash(password: reset.Password, user: user);
            user.Password = phash;
            _userservice.EditUser(user);
            _userservice.UpdateToken(reset.Email, reset.Token);
            return RedirectToAction("Index");
        }
        return View(reset);
    }

    // needs to be converted to serices to send mail like did with jwt
    [HttpGet]
    public IActionResult Sendmail()
    {
        var email = Request.Query["Email"];
        Random rnd = new Random();
        var resettoken = rnd.Next(100000, 999999);
        var reseturl = Url.Action("resetpass", "auth", new { Email = email, Token = resettoken }, Request.Scheme);
        try
        {
            _mailService.SendMail(email, reseturl);
            _userservice.ResetToken(email, resettoken);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        Console.WriteLine("Email sent with MailKit!");
        return RedirectToAction("index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View();
    }

    
}
