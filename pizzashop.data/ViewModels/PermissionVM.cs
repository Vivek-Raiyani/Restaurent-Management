using System.ComponentModel.DataAnnotations;

namespace pizzashop.data.ViewModels;

public class PermissionVM
{
    [Required]
    public string Name { get; set;} = null! ;

    [Required]
    public int PermissionTypeId { get; set;}

    [Required]
    public bool CanView { get; set;}

    [Required]

    public bool CanEdit { get; set;}

    [Required]
    public bool CanDelete { get; set;}
}
