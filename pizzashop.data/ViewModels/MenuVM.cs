using System.Security.Cryptography.X509Certificates;

namespace pizzashop.data.ViewModels;

public class MenuVM
{
    
    public List<CategoryVM> Categories { get; set;}  = new List<CategoryVM>();

    public List<ModifierVM> Modifiers { get; set;} = new List<ModifierVM>();
}
