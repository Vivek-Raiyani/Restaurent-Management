using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.repository.Interfaces.OrderApp;

namespace pizzashop.repository.Implementations.OrderApp;

public class OrderTableRepository : IOrderTableRepository
{
    private readonly PizzashopContext _db;

    public OrderTableRepository(PizzashopContext db)
    {
        _db = db;
    }

    public IEnumerable<TableDetail> GetSectionTables(int SectionId)
    {
        try
        {

            return _db.TableDetails.Include(t=> t.OrderTables).ThenInclude(d=>d.Order)
                    .OrderByDescending(t=>t.TableId).Where(t=> t.IsDeleted!=true && t.SectionId==SectionId).OrderBy(t=> t.TableId);

        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<TableDetail>();
        }
    }

    // add order table
    public bool AssignOrderTable(OrderTable table)
    {
        try
        {
            _db.OrderTables.Add(table);
            _db.SaveChanges();
            return true;
        }
        catch(DbUpdateException e)
        {
            Console.WriteLine(e.Message);
        }
        catch(Exception e)
        {
             Console.WriteLine(e.Message);
        }
        return false;
    }

    public int GetTableOrder(int tableid)
    {
        try
        {
            var order =_db.OrderTables.Where(t=> t.TableId == tableid).OrderByDescending(s=> s.TableId).First();
            if(order == null)
            {
                return 0;
            }
            else
            {
                return (int)order.OrderId;
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }

    
}
