using pizzashop.data.Models;

namespace pizzashop.repository.Interfaces;

public interface IModifierGroupRepository
{

    public bool Add(ModifierGroup group);

    public bool Update(ModifierGroup group);

    public ModifierGroup Read(int groupid);

    public List<ModifierGroup> ReadAll();
    public bool UpdateMany(List<ModifierGroup> groups);
}
