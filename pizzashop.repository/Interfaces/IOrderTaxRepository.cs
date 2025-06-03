using pizzashop.data.Models;

namespace pizzashop.repository.Interfaces;

public interface IOrderTaxRepository
{

    public bool AddUpdateMany(List<OrderTax> Taxes, int status);
    public bool AddUpdate(OrderTax Taxes);
    public OrderTax Read(int Taxesid);
    public IEnumerable<OrderTax> ReadAll(int orderid);

}
