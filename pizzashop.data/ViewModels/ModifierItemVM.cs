namespace pizzashop.data.ViewModels;

public class ModifierItemVM
{   
    public int MappingId { get; set; }
    public int ModifierId { get; set; }

    public string ModName { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public float Rate { get; set; }

    public short Quantity { get; set; }


}
