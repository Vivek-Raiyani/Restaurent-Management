using pizzashop.data.Models;
using pizzashop.data.ViewModels.TableSection;

namespace pizzashop.services.Interfaces.TableSection;

public interface ITableServics
{

    public bool AddTable(AddEditTableVM tableVM);

    public bool UpdateTable(AddEditTableVM table);

    public bool DeleteTable(int tableId);

    public TableDetail GetTableById(int tableId);

    public AddEditTableVM GetTableVM(int tableId);

    public bool DeleteMultipleTables(List<int> tableIds);

    // public bool CheckConstrain( string value );
    public bool CheckConstrain( string value , int sectionId);
}
