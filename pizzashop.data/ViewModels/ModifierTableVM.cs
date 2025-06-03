namespace pizzashop.data.ViewModels;

public class ModifierTableVM
{
    public int ModiferGroupId { get; set; }

    public required PaginatedListVM<ModifierItemVM> Modifiers { get; set; }
}
