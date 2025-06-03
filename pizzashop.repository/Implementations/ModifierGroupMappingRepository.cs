using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.repository.Interfaces;

namespace pizzashop.repository.Implementations;

public class ModifierGroupMappingRepository : IModifierGroupMappingRepository
{

    private readonly PizzashopContext _db;

    public ModifierGroupMappingRepository(PizzashopContext pizzashopContext)
    {
        _db = pizzashopContext;
    }
    // add

    public bool AddMany( List<ModifierGroupMapping> mapping){
        try{
            _db.ModifierGroupMappings.AddRange(mapping);
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
    public bool UpdateMany( List<ModifierGroupMapping> mapping){
        try{
            _db.ModifierGroupMappings.UpdateRange(mapping);
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
    public List<ModifierGroupMapping> ReadGroupModifiers(int groupid){
        return _db.ModifierGroupMappings
                .Where(m=> m.ModifierGroupId == groupid && m.Isdeleted != true)
                .Include(m=> m.Modifier)
                .ToList();
    }

    public List<ModifierGroupMapping> ReadModifierGroups(int modifierid){
        return _db.ModifierGroupMappings
                .Where(m=> m.ModifierId == modifierid && m.Isdeleted != true)
                .Include(m=> m.ModifierGroup)
                .ToList();
    }


}
