using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.data.ViewModels.TableSection;
using pizzashop.repository.Interfaces.TableSection;

namespace pizzashop.repository.Implementations.TableSection;

public class SectionRepository : ISectionRepository
{   

    private readonly PizzashopContext _db;

    public SectionRepository(PizzashopContext db)
    {
        _db = db;
    }



    #region  Section Tables

    // returns the count needed for pagination
    public int PaginationTableCount(string search,int SectionId)
    {
        if (string.IsNullOrEmpty(search))
        {   
            // var count =0;
            var count = _db.TableDetails.Where(t => t.SectionId == SectionId && t.IsDeleted != true).Count();
            return count;
        }
        else
        {   
            var count = _db.TableDetails.Where(s =>s.SectionId ==SectionId && s.IsDeleted != true && s.TblName.ToLower().Contains(search.ToLower())).Count();
            return count;
        }
    }

    // return the iteam list 
    public List<TableDetail> PaginationTable(int page, int pageSize, string search, int SectionId)
    {
        if (string.IsNullOrEmpty(search))
        {
            return _db.TableDetails.Where(b => b.IsDeleted != true && b.SectionId == SectionId)
                    .OrderBy(b => b.TableId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
        }
        else
        {
            return _db.TableDetails.Where(s => s.TblName.ToLower().Contains(search.ToLower()))
                    .Where(b => b.IsDeleted != true && b.SectionId == SectionId)
                    .OrderBy(b => b.TableId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
        }
    }


    #endregion


    #region  new section repository

    // add section
    public bool Add(Section sec)
    {
        try
        {
            _db.Sections.Add(sec);
            _db.SaveChanges();
            return true;
        }
        catch(DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
        }
        return false;
    }

    // update section
    public bool Update(Section sec)
    {
        try
        {
            _db.Sections.Update(sec);
            _db.SaveChanges();
            return true;
        }
        catch(DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
        }
        return false;
    }
    
    public bool UpdateMany(List<Section> sections)
    {
        try
        {
            _db.Sections.UpdateRange(sections);
            _db.SaveChanges();
            return true;
        }
        catch(DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
        }
        return false;
    }

    // read 

    public Section Read(int sectionid)
    {
        try
        {
           return _db.Sections.SingleOrDefault(s=> s.SectionId == sectionid && s.IsDeleted != true) ?? new Section();
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
        }
        return new Section();
    }

    public IEnumerable<Section> ReadAll()
    {
        try
        {
           return _db.Sections.Where(s=> s.IsDeleted != true).OrderBy(s=> s.SectionId);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
        }
        return new List<Section>();
    }

  
    #endregion
}
