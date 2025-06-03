using pizzashop.data.Models;
using pizzashop.data.ViewModels;

namespace pizzashop.services.Interfaces;

public interface IMenuService
{

  public IEnumerable<CategoryVM> CategorySidebar();

  public IEnumerable<ItemCategory> CategoryList();

  public IEnumerable<ModifierGroupNameVM> ModifierSidebar();

  public ItemListVM ItemTable(int categoryid, int page, int pagesize, string search);

  public ModifierTableVM ModifierTable(int groupid, int page, int pagesize, string search);

  public ModifierTableVM ModifierTableModal(int groupid, int page, int pagesize, string search, List<ModiferModalVM> modifiders);

  public CategoryVM ReadCategory(int categoryid);
  public AddEditItemVM ReadItem(int itemid);
  public AddEditModifier ReadModifier(int modifierid);
  public List<ItemGroupData> ReadItemMappings(int itemid);

  public ItemGroupData ShowMapping(int groupid);
  public AddEditGroup ReadGroup(int groupid);

  public bool AddEditCategory(CategoryVM category);
  public bool AddEditItem(AddEditItemVM item);

  public bool AddEditModifier(AddEditModifier modifier);

  public bool  AddEditGroup(AddEditGroup group);

  public bool CheckConstrain(string name , string value );

  public bool Delete(MenuDelete menuDelete);
}
