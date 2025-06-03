using System.ComponentModel.DataAnnotations;

namespace pizzashop.data.ViewModels;

public class ItemtableVM
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public float Rate { get; set; }
    [Required]
    public string ItemType { get; set; } = null!;// veg , non veg ,as of now

    public string? Image { get; set; }

    [Required]
    public short? Quantity { get; set; }

    [Required]
    public bool? Available { get; set; }


}
