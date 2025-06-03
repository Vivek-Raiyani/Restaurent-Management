using pizzashop.data.Models;

namespace pizzashop.repository.Interfaces;

public interface IModifierGroupMappingRepository
{

     public bool AddMany( List<ModifierGroupMapping> mapping);
     public bool UpdateMany( List<ModifierGroupMapping> mapping);
     public List<ModifierGroupMapping> ReadGroupModifiers(int groupid);
     public List<ModifierGroupMapping> ReadModifierGroups(int modifierid);

}
