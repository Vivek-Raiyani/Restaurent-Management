using System.Text.Json;
using Microsoft.AspNetCore.Http;
using pizzashop.data.Models;
using pizzashop.data.ViewModels;

namespace pizzashop.services.Utils;

public static class SessionUtils
{
    /// <summary>
    /// Method to store user details in session
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="user"></param>
    public static void SetUser(HttpContext httpContext, ProfileVM user)
    {
        if (user != null)
        {
            string userData = JsonSerializer.Serialize(user);
            httpContext.Session.SetString("UserData", userData);

            // Store simple string in Session
            //httpContext.Session.SetString("UserId", user.Id.ToString());
        }
    }

    /// <summary>
    /// Method to retrieve user details from session
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static ProfileVM? GetUser(HttpContext httpContext)
    {
        // Check session first
        string? userData = httpContext.Session.GetString("UserData");

        if (string.IsNullOrEmpty(userData))
        {
            // If session is empty, check the cookie
            httpContext.Request.Cookies.TryGetValue("UserData", out userData);
        }
        try{
            return string.IsNullOrEmpty(userData) ? null : JsonSerializer.Deserialize<ProfileVM>(userData);
        }catch(Exception ex){
            Console.WriteLine(ex.Message);
            return null;
        }
        
    }

    /// <summary>
    /// Method to clear all Session data
    /// </summary>
    /// <param name="httpContext"></param>
    public static void ClearSession(HttpContext httpContext) => httpContext.Session.Clear();
}

