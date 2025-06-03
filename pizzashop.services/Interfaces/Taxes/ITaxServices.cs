using pizzashop.data.Models;
using pizzashop.data.ViewModels.Taxes;

namespace pizzashop.services.Interfaces.Taxes;

public interface ITaxServices
{

    public IEnumerable<TaxesListVM> GetAllTaxes(string search);

    public TaxesListVM GetTaxById(int taxid);

    public bool AddTax(TaxesListVM taxes);

    public bool EditTax(TaxesListVM taxes);

    public bool DeleteTax(int taxid);

    public bool CheckConstrain( string value );
}
