using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace pizzashop.data.ViewModels;

public class AddItemVM
{
    public int Id { get; set; }

    [Required]
    [Range(1, short.MaxValue, ErrorMessage = "CategoryId must be >= 1.")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100,MinimumLength =3, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string IteamName { get; set; } = null!;

    public string type { get; set; } = null!;

    [Required(ErrorMessage = "Rate is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Rate must be greater than 0.")]
    public float Rate { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    [Range(0, 100, ErrorMessage = "Quantity must be > 0")]
    public short Quantity { get; set; }

    public string Units { get; set; } = null!;

    public bool Available { get; set; }

    public bool DefaultTax { get; set; }

    [Required(ErrorMessage = "Tax percentage is required")]
    [Range(0, 100, ErrorMessage = "Tax percentage must be between 0 and 100.")]
    public short TaxPercentage { get; set; }

    [Required(ErrorMessage = "ShortCode is required")]
    [Range(0,  short.MaxValue, ErrorMessage = "ShortCode need to be greated than 0")]
    public short ShortCode { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; } = null!;

    public string IteamImg { get; set; } = null!;

    [Required(ErrorMessage = "Item image is required.")]
    public IFormFile formFile{ get; set; } = null!;

    
}
