using Microsoft.AspNetCore.Identity;
using pizzashop.data.Models;
using pizzashop.data.ViewModels;

namespace pizzashop.services.Interfaces;

public interface IPasswordHash
{
    public string PassHash(string password, ProfileVM user);

    public PasswordVerificationResult PasswordVerificationResult(string password, ProfileVM profile);
}
