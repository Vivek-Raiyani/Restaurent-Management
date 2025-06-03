using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.data.ViewModels.Taxes;
using pizzashop.repository.Interfaces.Tax;

namespace pizzashop.repository.Implementations.Tax;

public class TaxRepositroy : ITaxRepository
{

    private readonly PizzashopContext _db;

    public TaxRepositroy(PizzashopContext db)
    {
        _db = db;
    }

    #region  new tax repo
    public bool Add(Taxis tax)
    {
        try
        {
            _db.Taxes.Add(tax);
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

    public bool Update(Taxis tax)
    {
        try
        {
            _db.Taxes.Update(tax);
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
    
    public Taxis Read (int taxid){
        try{
            var tax = _db.Taxes.SingleOrDefault(x => x.TaxId == taxid && x.IsDeleted != true) ?? new Taxis();
            return tax;
        }
        catch(Exception ex){
             Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
             Console.WriteLine($"Exception: {ex.Message}\n{ex.StackTrace}");
        }
        return new Taxis();
    }

    public IEnumerable<Taxis> ReadAll(string search =""){
        if(string.IsNullOrEmpty(search))
        {
             return _db.Taxes.Where(x=> x.IsDeleted != true).OrderBy(t=> t.TaxId);
        }
        else
        {
             return _db.Taxes.Where(x=> x.IsDeleted != true && x.TaxName.ToLower().Contains(search.ToLower()) ).OrderBy(t=> t.TaxId);
        }
       
    }

    public IEnumerable<Taxis> ReadAllNonDefault(){
        return _db.Taxes.Where(x=> x.IsDeleted != true && x.IsDefault != true).OrderBy(t=> t.TaxId);
    }
    
    
    #endregion
}
