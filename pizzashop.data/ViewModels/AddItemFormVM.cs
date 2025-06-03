using pizzashop.data.Models;

namespace pizzashop.data.ViewModels;

public class AddItemFormVM
{
    
    public required AddItemVM ItemVM{ get; set; }

    public List<MenuCategory> CategoriesVM { get; set; } = new List<MenuCategory>();

    public List<IteamModiferListVM> ModifierList{ get; set; } = new List<IteamModiferListVM>();

    
}
