using System.ComponentModel.DataAnnotations;
using pizzashop.data.Models;

namespace pizzashop.data.ViewModels;

public class EditModifierGroupUIVM
{
    [Required]
    public  required ModifierGroup Group { get; set; } 

    public List<ExistingModifierVM> Modifiers { get; set;} = new List<ExistingModifierVM>();
}
