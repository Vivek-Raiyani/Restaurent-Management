using pizzashop.data.Models;

namespace pizzashop.repository.Interfaces.OrderApp;

public interface IOrderTableRepository
{

    public IEnumerable<TableDetail> GetSectionTables(int SectionId);

     public bool AssignOrderTable(OrderTable table);

     public int GetTableOrder(int tableid);
}
