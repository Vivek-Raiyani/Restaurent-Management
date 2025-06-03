using pizzashop.data.Models;
using pizzashop.data.ViewModels;

namespace pizzashop.service.Interface;

public interface IUserService
{

    public ProfileVM GetUser(string email);

    public User GetUserModel(string email);

    // public Task AddUserAsync(ProfileVM user);

    public void EditUser(ProfileVM profile);
    
    public void AddUser(ProfileVM user);

    public void RemoveUser(string email);
    public void RemoveUsers( List<string> emails);

    public bool ChangePassword(ProfileVM user,string password,string oldpass);

    // public PaginatedListVM<UserlistVM> PaginationUserList(int page, int pageSize, string search);
    public PaginatedListVM<UserlistVM> PaginationUserList(int page, int pageSize, string search,int usera , int userd, int rolea, int roled);

    public bool CheckConstrians(string field, string value);
    public DashboardVM Dashboard(int days, DateTime start, DateTime end);
    public DashboardChartJson DashboardChart (int days, DateTime start, DateTime end);

    public bool ResetToken(string email, int token);
     public bool VerifiyToken(string email, int token);

     public bool UpdateToken(string email, int token);

}
