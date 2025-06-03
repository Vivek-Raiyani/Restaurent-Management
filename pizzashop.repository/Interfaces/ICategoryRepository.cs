using pizzashop.data.Models;

namespace pizzashop.repository.Interfaces;

public interface ICategoryRepository
{

    public bool Add(MenuCategory category);

    public bool Update(MenuCategory category);

    public bool UpdateMany(List<MenuCategory> categories);

    public MenuCategory Read(int categoryid);

    public IEnumerable<MenuCategory> ReadAll();


}
