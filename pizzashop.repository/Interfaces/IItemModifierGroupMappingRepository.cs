using pizzashop.data.Models;

namespace pizzashop.repository.Interfaces;

public interface IItemModifierGroupMappingRepository
{
       public bool AddMany( List<ItemModifierGroup> mapping);

       public bool UpdateMany( List<ItemModifierGroup> mapping);

       public List<ItemModifierGroup> ReadGroupItems(int groupid);

       public List<ItemModifierGroup> ReadItemGroups(int itemid);

}
