using pizzashop.data.Models;
using pizzashop.data.ViewModels.Taxes;

namespace pizzashop.repository.Interfaces.Tax;

public interface ITaxRepository
{

    public bool Add(Taxis tax);
    public bool Update(Taxis tax);
    public Taxis Read(int taxid);
    public IEnumerable<Taxis> ReadAll(string search="");
    public IEnumerable<Taxis> ReadAllNonDefault();

}
