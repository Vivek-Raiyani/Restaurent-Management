using System.ComponentModel.DataAnnotations;
using pizzashop.data.ViewModels.OrderApp.Tables;

namespace pizzashop.data.ViewModels.OrderApp.Waiting;

public class WaitingTokenVM
{
    public int TokenId { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name must be between 3 and 50 characters.")]
    public string Name { get; set; } = null!;

     [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Invalid phone number format.")]
    public string Phone { get; set; } = null!;

    [Required]
    [Range(1, short.MaxValue, ErrorMessage = "persons must be at least 1.")]
    public int  Persons { get; set; } = 0;

    [Required]
    [Range(1, short.MaxValue, ErrorMessage = "Section must exist")]
    public int Sectionid { get; set; } = 0;

    public IEnumerable<OrderSectionVM>? SectionVMs { get; set; }
}

public class AssignTableVM{

    public int Floorid { get; set; }

    public IEnumerable<WaitingTokenVM>? Tokens { get; set;} 

    public List<int> Tableids { get; set; } = new List<int>();
}