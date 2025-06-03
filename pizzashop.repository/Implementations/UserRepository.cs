using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.repository.Interface;

namespace pizzashop.repository.Implementation;

public class UserRepository : IUserRepository
{
    private readonly PizzashopContext _db;

    public UserRepository(PizzashopContext db)
    {
        _db = db;
    }

    #region  reading user data
    public User GetUser(string email)
    {   try
        {
        return _db.Users.SingleOrDefault(u => u.Email == email) ?? new User();
        }
        catch(DbUpdateException ex){
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
        }catch(Exception ex){
            Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
        }
        return new User();
    }

    public IEnumerable<User> GetUserList()
    {
        return _db.Users.Where(u => u.IsDeleted != true).ToList();
    }

    #endregion

    public int GetUserCount()
    {
        return _db.Users.Where(u => u.IsDeleted != true).Count();
    }

    public int GetUserCount(string search)
    {
        return _db.Users.Where(s => s.Fname.ToLower().Contains(search.ToLower()) || s.Lname.ToLower().Contains(search.ToLower()) && s.IsDeleted != true).Count();
    }

    public Role GetUserRole(int id)
    {

        return _db.Roles.SingleOrDefault(r => r.RoleId == id) ?? new Role();
    }

    #region  Pagination
    public List<User> PaginationUserList(int page, int pageSize, int usera, int userd, int rolea, int roled)
    {   
        var  data= _db.Users
                .Where(b => b.IsDeleted != true)
                .OrderBy(b => b.UserId);

        if (usera == 1)
        {
        data = (IOrderedQueryable<User>)data
                    .OrderBy(b => b.Fname)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
        }
        if (userd == 1)
        {
            data = (IOrderedQueryable<User>)data
                    .OrderByDescending(b => b.Fname)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
        }
        if (rolea == 1)
        {
            data = (IOrderedQueryable<User>)data
                    .OrderBy(b => b.RoleId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
        }
        if (roled == 1)
        {
             data = (IOrderedQueryable<User>)data
                    .OrderByDescending(b => b.RoleId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
        }
        return data.Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
    }


    public List<User> PaginationUserList(int page, int pageSize, string search, int usera, int userd, int rolea, int roled)
    { 
        var data = _db.Users.Where(s => s.Fname.ToLower().Contains(search.ToLower()) || s.Lname.ToLower().Contains(search.ToLower()))
                .Where(b => b.IsDeleted != true)
                .OrderBy(b => b.UserId);
        if (usera == 1)
        {
        data = (IOrderedQueryable<User>)data
                    .OrderBy(b => b.Fname)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
        }
        if (userd == 1)
        {
            data = (IOrderedQueryable<User>)data
                    .OrderByDescending(b => b.Fname)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
        }
        if (rolea == 1)
        {
            data = (IOrderedQueryable<User>)data
                    .OrderBy(b => b.RoleId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
        }
        if (roled == 1)
        {
             data = (IOrderedQueryable<User>)data
                    .OrderByDescending(b => b.RoleId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
        }
        return data.ToList();
    }

    public int PaginationCount(string search)
    {
        if (string.IsNullOrEmpty(search))
        {
            return _db.Users.Count();
        }
        else
        {
            return GetUserCount(search);
        }
    }

    #endregion

    #region update Fucntionality
    public void UpdateUser(User user)
    {
        try
        {
            _db.Update(user);
            _db.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }

    }
    #endregion

    #region AddUser Fucntionality
    public string AddUser(User user)
    {
        try
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return "";
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
            return ex.InnerException?.Message ?? ex.Message;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Exception: {ex.Message}\n{ex.StackTrace}");
            return ex.Message;
        }
    }
    #endregion

    #region Delete functionality
    public void RemoveUser(string email)
    {
        try
        {
            var user = GetUser(email);
            user.IsDeleted = true;
            _db.Update(user);
            _db.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return;
    }

    // no need of it i guss
    public void RemoveUsers(List<string> emails)
    {
        try
        {
            foreach (var mail in emails)
            {
                var user = GetUser(mail);
                user.IsDeleted = true;
                _db.Update(user);
                _db.SaveChanges();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return;
    }
    #endregion

    #region  new repo


    // #region  Add
    // public bool Add(User user){
    //     try{
    //         _db.Users.Add(user);
    //         _db.SaveChanges();
    //         return true;
    //     }catch(DbUpdateException ex){
    //         Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
    //     }catch(Exception ex){
    //         Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
    //     }
    //     return false;
    // }


    // public bool AddMany(List<User> userList){
    //     try{
    //         // List<User> users = new List<User>();
    //         // foreach(User user in userList){
    //         //     users.Add(user);
    //         // }
    //         _db.Users.AddRange(userList);
    //         _db.SaveChanges();
    //         return true;
    //     }catch(DbUpdateException ex){
    //         Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
    //     }catch(Exception ex){
    //         Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
    //     }
    //     return false;
    // }

    // #endregion

    // #region  Update And Delete
    // public bool Update(User user){
    //     try{
    //         _db.Users.Update(user);
    //         _db.SaveChanges();
    //         return true;
    //     }catch(DbUpdateException ex){
    //         Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
    //     }catch(Exception ex){
    //         Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
    //     }
    //     return false;
    // }

    // public bool UpdateMany(List<User> userList){
    //     try{
    //         // List<User> users = new List<User>();
    //         // foreach(User user in userList){
    //         //     users.Add(user);
    //         // }
    //         _db.Users.UpdateRange(userList);
    //         _db.SaveChanges();
    //         return true;
    //     }catch(DbUpdateException ex){
    //         Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
    //     }catch(Exception ex){
    //         Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
    //     }
    //     return false;
    // }

    // #endregion

    // #region  Read 
    // public User Read(int userid){
    //     try{
    //         var user = _db.Users.Include(u=> u.Role).SingleOrDefault(u=> u.UserId == userid && u.IsDeleted != true);
    //          return user;
    //     }
    //     catch(Exception ex){
    //         Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
    //         return null;
    //     }
    // }

    // public IEnumerable<User> ReadAll(){
    //     try{
    //         var user = _db.Users.Include(u=> u.Role).Where(u=> u.IsDeleted != true);
    //          return user;
    //     }
    //     catch(Exception ex){
    //         Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
    //         return new List<User>();
    //     }
    // }

    // #endregion

    #endregion

}

