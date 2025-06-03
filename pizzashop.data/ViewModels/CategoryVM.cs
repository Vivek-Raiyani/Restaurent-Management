using System.ComponentModel.DataAnnotations;

namespace pizzashop.data.ViewModels;

public class CategoryVM
{
    [Required]
    public int CategoryID { get; set; }

    [Required(ErrorMessage = "Category Name is required.")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Category Name must be between 3 and 30 characters.")]
    public string CategoryName { get; set;} = null!;

    [Required(ErrorMessage = "Description is required.")]
    public string CategoryDescription { get; set;} = null!;
    
}
