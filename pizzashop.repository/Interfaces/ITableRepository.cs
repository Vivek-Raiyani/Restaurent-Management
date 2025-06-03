using pizzashop.data.Models;
using pizzashop.data.ViewModels.TableSection;

namespace pizzashop.repository.Interfaces.TableSection;

public interface ITableRepository
{

     public TableDetail GetTableById(int TableID);
     public List<TableDetail> GetTableAll();

     public List<TableDetail> GetSectionTableAll(int SectionID);

     public bool AddTable(TableDetail table);

     public bool UpdateTable(AddEditTableVM tableVM);

     public bool UpdateTable(TableDetail table);

     public bool DeleteTable(int tableid);

     public bool DeleteMultipleTables(List<int> tableid);
}
