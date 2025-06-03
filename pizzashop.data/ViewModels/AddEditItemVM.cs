using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace pizzashop.data.ViewModels;

public class AddEditItemVM
{
    [Required]
    [Range(1, short.MaxValue, ErrorMessage = "CategoryId must be >= 1.")]
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;

    public int ItemId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    [Required(ErrorMessage = "Rate is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Rate must be greater than 0.")]
    public float Rate { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    [Range(1, 100, ErrorMessage = "Quantity must be > 0")]
    public short Quantity { get; set; }

    public string Unit { get; set; } = null!;

    public bool Available { get; set; }

    public bool DefaultTax { get; set; }

    [Required(ErrorMessage = "Tax percentage is required")]
    [Range(1, 100, ErrorMessage = "Tax percentage must be between 0 and 100.")]

    public short TaxPercentage { get; set; }

    [Required(ErrorMessage = "ShortCode is required")]
    [Range(1, short.MaxValue, ErrorMessage = "ShortCode need to be greated than 0")]
    public short ShortCode { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; } = null!;

    public string Img { get; set; } = null!;

    public IFormFile? FormFile { get; set; }

    public List<ItemCategory> Categories { get; set; } = new List<ItemCategory>();

    public string GroupString { get; set; } = null!;

    public List<ItemGroupMappingVM> Groups { get; set; } = new List<ItemGroupMappingVM>();
}


public class ItemGroupMappingVM
{

    public int Mappingid { get; set; }
    public int GroupId { get; set; }
    public int Min { get; set; }
    public int Max { get; set; }
}

public class ItemGroupData
{

    public int MappingId { get; set; }

    public int GroupId { get; set; }

    public string? Name { get; set; }
    public int Max { get; set; }

    public int Min { get; set; }

    public List<ItemModifierDisplay> Modifiers { get; set; } = new List<ItemModifierDisplay>();
}


public class ItemModifierDisplay
{

    public string Name { get; set; } = null!;

    public float Rate { get; set; }
}


