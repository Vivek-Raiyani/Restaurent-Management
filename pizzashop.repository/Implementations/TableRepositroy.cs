using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.data.ViewModels.TableSection;
using pizzashop.repository.Interfaces.TableSection;

namespace pizzashop.repository.Implementations.TableSection;

public class TableRepositroy : ITableRepository
{
    private readonly PizzashopContext _db;

    public TableRepositroy(PizzashopContext db)
    {
        _db = db;
    }

    #region Read Section

    public TableDetail GetTableById(int TableID)
    {
        try
        {
            return _db.TableDetails.SingleOrDefault(t => t.IsDeleted != true && t.TableId == TableID) ?? new TableDetail();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occured -- " + ex.Message);
            return new TableDetail();
        }
    }

    public List<TableDetail> GetTableAll()
    {
        try
        {
            return _db.TableDetails.Where(t => t.IsDeleted != true).OrderBy(t=> t.TableId).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occured -- " + ex.Message);
            return new List<TableDetail>();
        }
    }

    public List<TableDetail> GetSectionTableAll(int SectionID)
    {
        try
        {
            // login to be imlemented
            return _db.TableDetails.Where(t => t.IsDeleted != true && t.SectionId == SectionID).OrderBy(t=> t.TableId).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occured -- " + ex.Message);
            return new List<TableDetail>();
        }
    }
    #endregion


    #region  Add Table 

    public bool AddTable(TableDetail table)
    {
        try
        {
            _db.TableDetails.Add(table);
            _db.SaveChanges();
            return true;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
            // throw;
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Exception: {ex.Message}\n{ex.StackTrace}");
            throw;
        }
    }
    #endregion

    #region  Delete Table
    public bool DeleteTable(int tableid){
        try{
            var table =  _db.TableDetails.Find(tableid) ;
            
            if (table == null){
                return false;
            }

            if(table.TableStatus != "available" )
            {
                return false;
            }
            table.IsDeleted = true;
            _db.TableDetails.Update(table);
            _db.SaveChanges();
            return true;
        }catch (DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
            // throw;
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Exception: {ex.Message}\n{ex.StackTrace}");
            throw;
        }
    }
    
    public bool DeleteMultipleTables(List<int> tableid){
        try{
            foreach(var id in tableid){
                var table =  _db.TableDetails.Find(id);
                if(table == null)
                {
                    return false;
                }
                table.IsDeleted = true;
                _db.TableDetails.Update(table);
            }
            _db.SaveChanges();
            return true;
        }catch (DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
            // throw;
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Exception: {ex.Message}\n{ex.StackTrace}");
            throw;
        }
    }
    #endregion

    #region Update Table

    public bool UpdateTable( AddEditTableVM tableVM){
        try{
            var currenttable =  _db.TableDetails.Find(tableVM.TableId);
            if(currenttable == null)
            {
                return false;
            }
            currenttable.TblName = tableVM.TableName;
            currenttable.Capacity = tableVM.Capacity;
            currenttable.SectionId = tableVM.SectionId;
            currenttable.TableStatus = tableVM.Status;

            _db.TableDetails.Update(currenttable);
            _db.SaveChanges();
            return true;
        }catch (DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
            // throw;
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Exception: {ex.Message}\n{ex.StackTrace}");
            throw;
        }
    }
    
    public bool UpdateTable(TableDetail table){
        try{
            _db.TableDetails.Update(table);
            _db.SaveChanges();
            return true;
        }catch(DbUpdateException e){
            Console.WriteLine(e.Message);
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }
        return false;
    }
    #endregion


    #region  new table repository

    // add table   
    public bool Add(TableDetail table){
        try{
            _db.TableDetails.Add(table);
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

    // update
    public bool Update(TableDetail table){
        try{
            _db.TableDetails.Update(table);
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
    
    public bool UpdateMany(List<TableDetail> tables){
        try{
            _db.TableDetails.UpdateRange(tables);
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

    public TableDetail Read(int tableid){
        try{
           return _db.TableDetails.SingleOrDefault(s=> s.TableId == tableid && s.IsDeleted != true) ?? new TableDetail();
        }
        catch(Exception ex){
            Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
        }
        return new TableDetail();
    }

    public IEnumerable<TableDetail> ReadAll(){
        try{
           return _db.TableDetails.Where(s=> s.IsDeleted != true).OrderBy(s=>s.TableId);
        }
        catch(Exception ex){
            Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
        }
        return new List<TableDetail>();
    }

    public IEnumerable<TableDetail> ReadSection(int sectionid){
        try{
           return _db.TableDetails.Where(s=> s.IsDeleted != true && s.SectionId == sectionid).OrderBy(s=>s.TableId);
        }
        catch(Exception ex){
            Console.WriteLine($"Exception: {ex.Message}\n{ex.InnerException?.Message}");
        }
        return new List<TableDetail>();
    }

    // pagination read
 
    public List<TableDetail> PaginationTable(string search, int sectionid)
    {
        if (string.IsNullOrEmpty(search))
        {
            return _db.TableDetails.Where(b => b.IsDeleted != true && b.SectionId == sectionid)
                    .OrderBy(b => b.TableId)
                    .ToList();
        }
        else
        {
            return _db.TableDetails.Where(s => s.TblName.ToLower().Contains(search.ToLower()))
                    .Where(b => b.IsDeleted != true && b.SectionId == sectionid)
                    .OrderBy(b => b.TableId)
                    .ToList();
        }
    }



    #endregion


}
