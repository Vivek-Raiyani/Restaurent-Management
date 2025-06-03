using System.ComponentModel.DataAnnotations;

namespace pizzashop.data.ViewModels.TableSection;

public class SectionSidebarVM
{
    public int SectionId { get; set; }

    [Required(ErrorMessage = "Section Name is required")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Section Name must be between 3 and 30 characters.")]
    public string SectionName { get; set; } =null!;

    
    [Required(ErrorMessage = "Description is required")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 30 characters.")]
    public string Description { get; set; } =null!;
}
