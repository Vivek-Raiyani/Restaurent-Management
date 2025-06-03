using System.ComponentModel.DataAnnotations;

namespace pizzashop.data.ViewModels;

public class PasswordChangeVM
{
    [Display(Name = "OldPassword")]
    [Required]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword),ErrorMessage = "Password didnt match")]
    public string ConfirmPassword { get; set; } = null!;

}
