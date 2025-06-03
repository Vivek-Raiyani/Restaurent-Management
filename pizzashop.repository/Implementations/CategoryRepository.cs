using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.repository.Interfaces;

namespace pizzashop.repository.Implementations;

public class CategoryRepository : ICategoryRepository
{
    private readonly PizzashopContext _db;

    public CategoryRepository(PizzashopContext db)
    {
        _db = db;
    }

    // add 
    public bool Add(MenuCategory category){
        try{
            _db.MenuCategories.Add(category);
            _db.SaveChanges();
            return true;
        }
        catch(DbUpdateException ex){
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
        }catch(Exception ex){
            Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
        }
        return false;
    }

    // update MenuCategory
    public bool Update(MenuCategory category){
        try{
            _db.MenuCategories.Update(category);
            _db.SaveChanges();
            return true;
        }
        catch(DbUpdateException ex){
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
        }catch(Exception ex){
            Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
        }
        return false;
    }
    
    public bool UpdateMany(List<MenuCategory> categories){
        try{
            _db.MenuCategories.UpdateRange(categories);
            _db.SaveChanges();
            return true;
        }
        catch(DbUpdateException ex){
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
        }catch(Exception ex){
            Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
        }
        return false;
    }

    // read 

    public MenuCategory Read(int categoryid){
        try{
           return _db.MenuCategories.SingleOrDefault(s=> s.CategoryId == categoryid && s.IsDeleted != true) ?? new MenuCategory();
        }
        catch(Exception ex){
            Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
        }
        return new MenuCategory();
    }

    public IEnumerable<MenuCategory> ReadAll(){
        try{
           return _db.MenuCategories.Where(s=> s.IsDeleted != true).OrderBy(s=> s.CategoryId);
        }
        catch(Exception ex){
            Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
        }
        return new List<MenuCategory>();
    }

}
