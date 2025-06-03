using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.repository.Interfaces;

namespace pizzashop.repository.Implementations;

public class ItemRepository : IItemRepository
{

    private readonly PizzashopContext _db;

    public ItemRepository(PizzashopContext db)
    {
        _db = db;
    }

    // add we need tp create mapping entries as well
    public bool Add(MenuItem item)
    {
        try
        {
            _db.MenuItems.Add(item);
            _db.SaveChanges();
            return true;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Exception: {ex.Message}\n{ex.StackTrace}");
        }
        return false;
    }

    // update only update the item the mapping will be update seprate
    public bool Update(MenuItem item)
    {
        try
        {
            _db.MenuItems.Update(item);
            _db.SaveChanges();
            return true;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Exception: {ex.Message}\n{ex.StackTrace}");
        }
        return false;
    }

    // delete many
    public bool DeleteMany(List<MenuItem> items)
    {
        try
        {
            _db.MenuItems.UpdateRange(items);
            _db.SaveChanges();
            return true;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Exception: {ex.Message}\n{ex.StackTrace}");
        }
        return false;
    }

    // read

    public MenuItem Read(int itemid)
    {
        try
        {
            return _db.MenuItems.Include(i=> i.Category).SingleOrDefault(m => m.IteamId == itemid && m.IsDeleted != true) ?? new MenuItem();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Exception: {ex.Message}\n{ex.StackTrace}");
            return new MenuItem();
        }
    }

    public IEnumerable<MenuItem> ReadAll(int categoryid, string search = "")
    {
        if (string.IsNullOrEmpty(search))
        {
            return _db.MenuItems
                    .Where(b => b.IsDeleted != true && b.CategoryId == categoryid)
                    .OrderBy(b => b.IteamId)
                    .ToList();
        }
        else
        {
            return _db.MenuItems.Where(s => s.IteamName.ToLower().Contains(search.ToLower()))
                    .Where(b => b.IsDeleted != true && b.CategoryId == categoryid)
                    .OrderBy(b => b.IteamId)
                    .ToList();
        }
    }

    public IEnumerable<MenuItem> ReadWithoutCategory( string search = "")
    {
        if (string.IsNullOrEmpty(search))
        {
            return _db.MenuItems
                    .Where(b => b.IsDeleted != true)
                    .OrderBy(b => b.IteamId)
                    .ToList();
        }
        else
        {
            return _db.MenuItems.Where(s => s.IteamName.ToLower().Contains(search.ToLower()))
                    .Where(b => b.IsDeleted != true)
                    .OrderBy(b => b.IteamId)
                    .ToList();
        }
    }


}
