using pizzashop.data.Models;

namespace pizzashop.repository.Interfaces.Customers;

public interface ICustomerRepository
{
    public Customer ExisitingCustomerCheck(string email);
     public Customer AddUpdateCustomer (Customer customer);

    public int ExportCustomerCount(string search, int status ,DateTime start, DateTime end);

    public IEnumerable<Customer> GetExportCustomers(string search, int status, DateTime start, DateTime end);
    
    // ----------=================--=-==-=-=-==--=-
    public IEnumerable<Customer> ReadAll();
    public Customer Read(int customerid);
    public IEnumerable<Customer> ReadOrderProgress();
    public IEnumerable<Customer> Search(string search);

}
