using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.repository.Interfaces;

namespace pizzashop.repository.Implementations;

public class ResetTokenReposiotry: IResetTokenReposiotry
{

    private readonly PizzashopContext _db;

    public ResetTokenReposiotry(PizzashopContext db)
    {
        _db = db;
    }

    public bool Add(ResetToken token)
    {
        try
        {
            _db.ResetTokens.Add(token);
            _db.SaveChanges();
            return true;
        }
        catch(DbUpdateException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception ex)
        {   
            Console.WriteLine(ex.Message);
        }
        return false;
    }

     public bool Update(ResetToken token)
    {
        try
        {
            _db.ResetTokens.Update(token);
            _db.SaveChanges();
            return true;
        }
        catch(DbUpdateException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception ex)
        {   
            Console.WriteLine(ex.Message);
        }
        return false;
    }

    public ResetToken Read(string email, int token)
    {
        try
        {
            return _db.ResetTokens.SingleOrDefault(t=> t.Email == email && t.Token == token) ?? new ResetToken();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new ResetToken();
        }
    }

}