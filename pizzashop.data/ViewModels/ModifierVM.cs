using System.ComponentModel.DataAnnotations;

namespace pizzashop.data.ViewModels;

public class ModifierVM
{
    [Required]
    public int ModGroupId { get; set; }

    [Required]
    public string ModifierName { get; set; } = null!;
}
