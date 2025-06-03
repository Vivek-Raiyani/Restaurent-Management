

using System.ComponentModel.DataAnnotations;

namespace pizzashop.data.ViewModels;


public class AddEditModifier
{
    public List<int> GroupIds { get; set; } = new List<int>();

    public int ModiferId { get; set; }
    
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 characters.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 30 characters.")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Rate is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Rate must be greater than 0.")]
    public float Rate { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    [Range(0, 100, ErrorMessage = "Quantity must be > 0")]
    public short Quantity { get; set; }

    
    public string Unit { get; set; } = null!;

    public string Groupstr { get; set; } = null!;
    public List<ModifierVM> Groups { get; set; } = new List<ModifierVM>();

}


public class MappingModel{
    public int MappingId { get; set; }
    public int ModiferId { get; set; }
    public string Name { get; set; } = null!;
}
