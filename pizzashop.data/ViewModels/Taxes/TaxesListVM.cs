using System.ComponentModel.DataAnnotations;

namespace pizzashop.data.ViewModels.Taxes;

public class TaxesListVM
{

    public int TaxId { get; set; }

    [Required(ErrorMessage = "Table Name requeired")]
    public string TaxName { get; set; } = null!;

    public string TaxType { get; set; } = null!;

    [Required]
    [Range(1, float.MaxValue, ErrorMessage = "TaxValue must greater than 1.")]
    public float TaxValue { get; set; }

    public bool IsEnabled { get; set; }

    public bool IsDefault { get; set; }
}
