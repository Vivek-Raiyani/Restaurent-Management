using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using pizzashop.data.Models;
using pizzashop.repository.Interfaces;

namespace pizzashop.repository.Implementations;

public class ModifiersRepository : IModifiersRepository
{
    private readonly PizzashopContext _db;

    public ModifiersRepository(PizzashopContext db)
    {
        _db = db;
    }

    // add update
    public bool AddUpdate(Modifier modifier)
    {
        try
        {
            if (modifier.ModifierId == 0)
            {
                _db.Modifiers.Add(modifier);
                _db.SaveChanges();
            }
            else
            {
                _db.Modifiers.Update(modifier);
                _db.SaveChanges();
            }
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

    // update many
    public bool UpdateMany(List<Modifier> modifiers)
    {
        try
        {
            _db.Modifiers.UpdateRange(modifiers);
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
    public Modifier Read(int modifierid)
    {
        try
        {
            return _db.Modifiers.Include(m=> m.ModifierGroupMappings).SingleOrDefault(m => m.ModifierId == modifierid && m.IsDeleted != true) ?? new Modifier();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Exception: {ex.Message}\n{ex.StackTrace}");
            return new Modifier();
        }
    }

    public IEnumerable<Modifier> ReadGroup(int groupid, string search ="")
    {
        if (string.IsNullOrEmpty(search))
        {
           return _db.ModifierGroupMappings.Where(g => g.ModifierGroupId == groupid && g.Isdeleted != true)
                        .Include(m => m.Modifier)
                        .Select(m => m.Modifier)
                        .OrderBy(m=> m.ModifierId)
                        .ToList();
        }
        else
        {
            return _db.ModifierGroupMappings.Where(g => g.ModifierGroupId == groupid && g.Isdeleted != true)
                        .Include(m => m.Modifier)
                        .Select(m => m.Modifier)
                        .Where(s => s.ModName.ToLower().Contains(search.ToLower()))
                        .OrderBy(m=> m.ModifierId)
                        .ToList();
        }
    }

    public IEnumerable<Modifier> ReadAll(string search = "")
    {
        if (string.IsNullOrEmpty(search))
        {
           return _db.Modifiers.Where(g => g.IsDeleted != true).OrderBy(m=> m.ModifierId).ToList();
        }
        else
        {
            return _db.Modifiers.Where(g => g.IsDeleted != true)
                        .Where(s => s.ModName.ToLower().Contains(search.ToLower()))
                        .OrderBy(m=> m.ModifierId)
                        .ToList();
        }
    }
    
}
