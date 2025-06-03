using System.ComponentModel.DataAnnotations;

namespace pizzashop.data.ViewModels;

public class AuthVM
{
    
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = null!;
    
    [Display (Name = "Password")]
    [Required]
    public string Password { get; set; } = null!;

    public bool RememberMe {get;set;} 

}
