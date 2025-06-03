using System.Diagnostics;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.repository.Interfaces;

namespace pizzashop.repository.Implementations;

public class ModifierGroupRepository: IModifierGroupRepository
{

    private readonly PizzashopContext _db;

    public ModifierGroupRepository(PizzashopContext db)
    {
        _db = db;
    }

    // add we need tp create mapping entries as well
    public bool Add(ModifierGroup group){
        try{
            _db.ModifierGroups.Add(group);
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

    // update only update the modifier group the mapping will be update seprate
    public bool Update(ModifierGroup group){
        try{
            _db.ModifierGroups.Update(group);
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

    public bool UpdateMany(List<ModifierGroup> groups){
        try{
            _db.ModifierGroups.UpdateRange(groups);
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

    public ModifierGroup Read(int groupid){
        try{
            return _db.ModifierGroups.Include(m=> m.ModifierGroupMappings).ThenInclude(m=> m.Modifier)
                    .SingleOrDefault(m=> m.ModGroupId == groupid && m.IsDeleted != true) ?? new ModifierGroup();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Exception: {ex.Message}\n{ex.StackTrace}");
            return new ModifierGroup();
        }
    }

    public List<ModifierGroup> ReadAll(){
        return _db.ModifierGroups.Include(m=> m.ModifierGroupMappings).Where(m => m.IsDeleted != true).ToList();
    }

}
