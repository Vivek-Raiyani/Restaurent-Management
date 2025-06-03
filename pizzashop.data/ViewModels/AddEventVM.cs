using System.ComponentModel.DataAnnotations;

namespace pizzashop.data.ViewModels;

public class AddEventVM
{
    [Required]
    public string Customer { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public DateTime EventDate { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    [Required]
    public string EventType { get; set; } = null!;
    [Required]
    public string Status { get; set; } = null!;
    [Required]
    public int Persons { get; set; }

    public bool Dinein { get; set; }

    public bool Ac { get; set; }

    public string? Instructions { get; set; }

    public string? Address { get; set; }

}
