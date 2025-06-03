using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.repository.Interfaces;

namespace pizzashop.repository.Implementations;

public class ItemModifierGroupMappingRepository : IItemModifierGroupMappingRepository
{

    private readonly PizzashopContext _db;

    public ItemModifierGroupMappingRepository(PizzashopContext db)
    {
        _db = db;
    }

    // add

    public bool AddMany( List<ItemModifierGroup> mapping){
        try{
            _db.ItemModifierGroups.AddRange(mapping);
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

    // update
    public bool UpdateMany( List<ItemModifierGroup> mapping){
        try{
            _db.ItemModifierGroups.UpdateRange(mapping);
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
    public List<ItemModifierGroup> ReadGroupItems(int groupid){
        return _db.ItemModifierGroups
                .Where(m=> m.ModiferId == groupid && m.Isdeleted != true)
                .Include(m=> m.Item)
                .ToList();
    }

    public List<ItemModifierGroup> ReadItemGroups(int itemid){
        return _db.ItemModifierGroups
                .Where(m=> m.ItemId == itemid && m.Isdeleted != true)
                .Include(m=> m.Modifer)
                .ThenInclude(m=> m.ModifierGroupMappings).ThenInclude(m=> m.Modifier)
                .ToList();
    }
    
}
