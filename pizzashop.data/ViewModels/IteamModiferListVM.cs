namespace pizzashop.data.ViewModels;

public class IteamModiferListVM
{

    public int GroupId { get; set; }

    public string Name { get; set; } = null!;

     public int Min { get; set; }

    public int Max { get; set; }

    public List<ItemModifierVM> Modifiers { get; set; } = new List<ItemModifierVM>();

}
