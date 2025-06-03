using pizzashop.data.Models;
using pizzashop.data.ViewModels.TableSection;
using pizzashop.repository.Implementations.TableSection;
using pizzashop.repository.Interfaces.TableSection;
using pizzashop.services.Interfaces.TableSection;

namespace pizzashop.services.Implementations.TableSection;

public class TableServices : ITableServics
{
    private readonly ITableRepository _tableRepo;

    public TableServices (ITableRepository tableRepositroy){
        _tableRepo = tableRepositroy;
    }


    public bool AddTable(AddEditTableVM tableVM){
        var table =  new TableDetail();
        table.TblName = tableVM.TableName;
        table.Capacity = tableVM.Capacity;
        table.SectionId = tableVM.SectionId;
        table.TableStatus = tableVM.Status;
        var result = _tableRepo.AddTable(table);
        return result;
    }
    

    public bool UpdateTable(AddEditTableVM table){
        var result = _tableRepo.UpdateTable(table);
        return result;
    }

    public bool DeleteTable(int tableId){
        var result = _tableRepo.DeleteTable(tableId);
        return result;
    }

    public bool DeleteMultipleTables(List<int> tableIds){
        var result = _tableRepo.DeleteMultipleTables(tableIds);
        return result;
    }

    public TableDetail GetTableById(int tableId){
        return _tableRepo.GetTableById(tableId);
    }

    public AddEditTableVM GetTableVM(int tableId){
        TableDetail table = _tableRepo.GetTableById(tableId);
        AddEditTableVM tableVM = new AddEditTableVM(){
            TableName = table.TblName,
            TableId = table.TableId,
            Capacity = table.Capacity,
            SectionId = table.SectionId,
            Status = table.TableStatus,
        };
        return tableVM;
    }


    #region  check constrain

    public bool CheckConstrain( string value , int sectionId)
    {
        return _tableRepo.GetSectionTableAll(SectionID : sectionId).FirstOrDefault( o=> o.TblName == value) == null ;
    }

    #endregion
    
}
