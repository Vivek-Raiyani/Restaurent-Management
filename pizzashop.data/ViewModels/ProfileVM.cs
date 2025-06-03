namespace pizzashop.data.ViewModels;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;
using pizzashop.data.Models;

public class ProfileVM
{

   
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name must be between 3 and 50 characters.")]
    public string Fname { get; set; } = null!;

   [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name must be between 3 and 50 characters.")]
    public string Lname { get; set; } = null!;

    [Required(ErrorMessage = "Username is required.")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters.")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = null!;

   
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;


    public string? ProfileImg { get; set; }

    [Required(ErrorMessage = "Country is required.")]
    public string Country { get; set; } = null!;

    [Required(ErrorMessage = "State is required.")]
    public string State { get; set; } = null!;

    [Required(ErrorMessage = "City is required.")]
    public string City { get; set; } = null!;

    [Required(ErrorMessage = "ZipCode is required.")]
    [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zipcode format.")]
    public string Zipcode { get; set; } = null!;

    [Required(ErrorMessage = "Address is required.")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Invalid phone number format.")]
    public string PhoneNo { get; set; } = null!;

    public string? RoleName { get; set; } 

    public int RoleId { get; set; }

    public bool Status { get; set; }

    public IFormFile? MyImage { set; get; }

    public List<Role> Roles { get; set; } = new List<Role>();
}
