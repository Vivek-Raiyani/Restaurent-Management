using pizzashop.data.Models;

namespace pizzashop.repository.Interfaces;

public interface IItemRepository
{

     public bool Add(MenuItem item);

     public bool Update(MenuItem item);

     public bool DeleteMany(List<MenuItem> items);

     public MenuItem Read(int itemid);

     public IEnumerable<MenuItem> ReadAll(int categoryid, string search = "");

     public IEnumerable<MenuItem> ReadWithoutCategory( string search = "");
}
