using System.ComponentModel.DataAnnotations;

namespace pizzashop.data.ViewModels;

public class EditModifierItemVM
{
    public int NewModifierGroupId { get; set; }

    public int ModifierId { get; set; }


    public int OldModifierGroupId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 characters.")]
    public string ModName { get; set; } = null!;

    [Required(ErrorMessage = "Rate is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Rate must be greater than 0.")]
    public float Rate { get; set; }

    public string Unit { get; set; } = null!;

    [Required(ErrorMessage = "Quantity is required")]
    [Range(0, 100, ErrorMessage = "Quantity must be > 0")]
    public short Quantity { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 30 characters.")]
    public string ModifierDesc { get; set; } = null!;

    public List<ModifierGroupNameVM> ModifierGroup { get; set; } = new List<ModifierGroupNameVM>();
}

