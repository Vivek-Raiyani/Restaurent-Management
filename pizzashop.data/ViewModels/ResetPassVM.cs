using System.ComponentModel.DataAnnotations;

namespace pizzashop.data.ViewModels;

public class ResetPassVM
{
    [Display (Name = "Email")]
    [Required]
    public string Email { get; set; } = null!;

    [Display (Name = "Token")]
    [Required]
    public int Token { get; set; } 
    
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    [DataType(DataType.Password)]
    public string? NewPassword { get; set; }
    
}
