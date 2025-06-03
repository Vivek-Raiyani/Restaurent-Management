namespace pizzashop.data.ViewModels;

public class ExistingModifierVM
{
    public int ModifierId { get; set;}
    public string ModifierName { get; set;} = null!;
}

public class ModiferModalVM{
    public int MappingId { get; set;}
    public int Id { get; set;}
    public string Name { get; set;} = null!;
}