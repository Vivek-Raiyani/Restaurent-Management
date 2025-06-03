
using pizzashop.data.Models;
using pizzashop.data.ViewModels;
using pizzashop.data.ViewModels.CustomerVM;


namespace pizzashop.services.Interfaces.Customers;

public interface ICustomerService
{


    public CustomerDetailsVM CustomerDetails(int CustomerId);

    // public CustomerExportVM GetExportCustomer(string search, int time=0);
    public CustomerExportVM GetExportCustomer(string search, DateTime from , DateTime to ,int status=1);

    public PaginatedListVM<CustomerTableVM> PaginationCustomer(int page, int pageSize, string search, int status, DateTime StartDate,
                    DateTime Enddate, string sortname = "", string sorttype = "", int sortbit = 0);
    
}
