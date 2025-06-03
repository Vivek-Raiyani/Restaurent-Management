using System.ComponentModel.DataAnnotations;

namespace pizzashop.data.ViewModels.TableSection;

public class AddEditTableVM
{

    public int TableId { get; set; }

    [Required(ErrorMessage = "Table Name requeired")]
    public string TableName { get; set;}=null!;

    [Required(ErrorMessage = "Status Required")]
    public string Status { get; set; } = null!;

    [Required]
    [Range(2, short.MaxValue, ErrorMessage = "Capacity must be at least 2.")]
    public short Capacity { get; set; } 

    [Required(ErrorMessage = "Section required")]
    public int SectionId { get; set; }

    public IEnumerable<SectionSidebarVM>? Sections { get; set; } 
}
