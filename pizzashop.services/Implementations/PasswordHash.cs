using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using pizzashop.data.Models;
using pizzashop.data.ViewModels;
using pizzashop.repository.Interface;
using pizzashop.services.Interfaces;
using pizzashop.services.Utils;

namespace pizzashop.services.Implementations;

public class PasswordHash : IPasswordHash
{
    
    private readonly IUserRepository _repo;


    public PasswordHash(IUserRepository repository)
    {
        _repo = repository;
    }
    
    public  string PassHash(string password, ProfileVM user){
        var passhash = new PasswordHasher<ProfileVM>();
        return passhash.HashPassword(user, password);
    }

    public PasswordVerificationResult PasswordVerificationResult(string password, ProfileVM user){
        var passhash = new PasswordHasher<ProfileVM>();
        try{
            PasswordVerificationResult password_bool = passhash.VerifyHashedPassword(user, user.Password, password);
            return password_bool;
        }catch(Exception e){
            Console.WriteLine(e.Message);
            return Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed;
        }
        
        
    }
}
