using pizzashop.data.Models;

namespace pizzashop.repository.Interfaces;

public interface IModifiersRepository
{
    public bool AddUpdate(Modifier modifier);

    public bool UpdateMany(List<Modifier> modifiers);

     public Modifier Read(int modifierid);

     public IEnumerable<Modifier> ReadGroup(int groupid, string search ="");

     public IEnumerable<Modifier> ReadAll(string search = "");
}
