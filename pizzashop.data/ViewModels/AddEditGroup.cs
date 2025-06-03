using System.ComponentModel.DataAnnotations;

namespace pizzashop.data.ViewModels;

public class AddEditGroup
{

    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; } = null!;

    public string ModifierString { get; set; } = null!;
    public List<ModifierNameVM> Modifier { get; set; } = new List<ModifierNameVM>();

    public List<int> ModifierIds { get; set; } = new List<int>();

}


public class ModifierNameVM
{

    public int MappingId { get; set; }
    public string Name { get; set; } = null!;

    public int Id { get; set; }
}
