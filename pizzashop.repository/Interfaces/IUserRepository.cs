using pizzashop.data.Models;
using pizzashop.data.ViewModels;

namespace pizzashop.repository.Interface;

public interface IUserRepository
{
    // returns a single user data
    public User GetUser(string email);
    
    // returns no of entries in db
    public int GetUserCount();
    
    // retunes no of user in db based of search filter in name
    public int GetUserCount(string search);

    // returns the list of user in db
    public IEnumerable<User> GetUserList();
    
    // retunes the role of user
    public Role GetUserRole (int id);

    // gives paignation user data 
    // public List<User> PaginationUserList( int page, int pageSize);
    public List<User> PaginationUserList(int page, int pageSize,int usera , int userd, int rolea, int roled);
    
    // gives pagination user data based on searchfilter
    // public List<User> PaginationUserList( int page, int pageSize , string search);
    public List<User> PaginationUserList(int page, int pageSize, string search,int usera , int userd, int rolea, int roled);

    // get no of user for pagination 
    public int PaginationCount(string search);

    // to update an existing user
    public void UpdateUser(User user);

    // to add new user
     public string AddUser(User user);

    // to delete single user
    public void RemoveUser(string email);

    //to delete multiple user
    public void RemoveUsers(List<string> emails);
}
