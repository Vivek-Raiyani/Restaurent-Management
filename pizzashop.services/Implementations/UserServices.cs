using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using pizzashop.data.Models;
using pizzashop.data.ViewModels;
using pizzashop.repository.Interface;
using pizzashop.repository.Interfaces;
using pizzashop.repository.Interfaces.Customers;
using pizzashop.repository.Interfaces.OrderApp;
using pizzashop.repository.Interfaces.Orders;
using pizzashop.service.Interface;
using pizzashop.services.Interfaces;


namespace pizzashop.service.Implementation;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly IRolesRepository _roleRepo;

    private readonly IPasswordHash _passwordHash;

    private readonly ICustomerRepository _customer;

    private readonly IOrderRepository _order;

    private readonly IWaitingListRepository _waiting;

    private readonly IItemRepository _item;
    private readonly IResetTokenReposiotry _reset;


    public UserService(IUserRepository repository, IRolesRepository roleRepo, IPasswordHash passwordHash,
     ICustomerRepository customer, IOrderRepository order, IWaitingListRepository waiting, IItemRepository item, IResetTokenReposiotry reset)
    {
        _userRepo = repository;
        _roleRepo = roleRepo;
        _passwordHash = passwordHash;
        _customer = customer;
        _order = order;
        _waiting = waiting;
        _item = item;
        _reset = reset;
    }

    public ProfileVM GetUser(string email)
    {
        User user = _userRepo.GetUser(email);
        Role role = _userRepo.GetUserRole(user.RoleId);

        ProfileVM profile = new()
        {
            Fname = user.Fname,
            Lname = user.Lname ?? "",
            Username = user.Username,
            Email = email,
            Password = user.Password,
            ProfileImg = user.ProfileImg,
            Country = user.Country,
            State = user.State,
            City = user.City,
            Zipcode = user.Zipcode,
            Address = user.Address,
            PhoneNo = user.PhoneNo,
            RoleName = role.RoleName,
            Status = user.Status
        };


        return profile;
    }

    public User GetUserModel(string email)
    {
        return _userRepo.GetUser(email);
    }

    public void EditUser(ProfileVM profile)
    {
        var roleid = _roleRepo.GetRoleId(profile.RoleName);
        var user = _userRepo.GetUser(profile.Email);

        user.Fname = profile.Fname;
        user.Lname = profile.Lname;
        user.Username = profile.Username;
        user.Email = profile.Email;
        user.Password = profile.Password ?? user.Password;
        if (profile.ProfileImg != null)
        {
            user.ProfileImg = profile.ProfileImg;
        }
        user.Country = profile.Country;
        user.State = profile.State;
        user.City = profile.City;
        user.Zipcode = profile.Zipcode;
        user.Address = profile.Address;
        user.PhoneNo = profile.PhoneNo;
        user.RoleId = roleid;
        user.Status = profile.Status;
        try
        {
            _userRepo.UpdateUser(user);
        }
        catch (Exception e)
        {
            Console.WriteLine("error in updating user");
        }


        return;
    }

    public void AddUser(ProfileVM profile)
    {
        var user = new User
        {
            Email = profile.Email,
            Fname = profile.Fname,
            Lname = profile.Lname,
            Address = profile.Address,
            City = profile.City,
            Country = profile.Country,
            State = profile.State,
            PhoneNo = profile.PhoneNo,
            RoleId = profile.RoleId,
            Username = profile.Username,
            Zipcode = profile.Zipcode,
            ProfileImg = profile.ProfileImg
        };

        var phash = _passwordHash.PassHash(password: profile.Password, user: profile);
        user.Password = phash;
        _userRepo.AddUser(user);
        return;
    }

    public void RemoveUser(string email)
    {
        _userRepo.RemoveUser(email);
        return;
    }

    public void RemoveUsers(List<string> emails)
    {
        foreach (var mail in emails)
        {
            _userRepo.RemoveUser(mail);
        }
        return;
    }

    public bool ChangePassword(ProfileVM profile, string password, string oldpass)
    {
        var PasswordMatch = _passwordHash.PasswordVerificationResult(oldpass, profile);
        if (PasswordMatch == PasswordVerificationResult.Success)
        {
            try
            {

                var user = _userRepo.GetUser(profile.Email);
                user.Password = _passwordHash.PassHash(user: profile, password: password);
                _userRepo.UpdateUser(user);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        else
        {
            Console.WriteLine(" passwords didn't match");
        }
        return false;
    }

    public PaginatedListVM<UserlistVM> PaginationUserList(int page, int pageSize, string search, int usera, int userd, int rolea, int roled)
    {
        var userlist = new List<User>();
        var count = 0;
        if (search.IsNullOrEmpty())
        {
            count = _userRepo.GetUserCount();
            userlist = _userRepo.PaginationUserList(page, pageSize, usera, userd, rolea, roled);

        }
        else
        {
            count = _userRepo.GetUserCount(search);
            userlist = _userRepo.PaginationUserList(page, pageSize, search, usera, userd, rolea, roled);
        }

        List<UserlistVM> user_data = new List<UserlistVM>();
        foreach (var user in userlist)
        {
            var role = _userRepo.GetUserRole(user.RoleId);
            UserlistVM user_view = new UserlistVM();
            user_view.Fname = user.Fname;
            user_view.Lname = user.Lname;
            user_view.Email = user.Email;
            user_view.ProfileImg = user.ProfileImg;
            user_view.PhoneNo = user.PhoneNo;
            user_view.Status = user.Status;
            user_view.RoleName = role.RoleName;
            user_data.Add(user_view);
        }
        int totalPages = (int)Math.Ceiling(count / (double)pageSize);
        

        return new PaginatedListVM<UserlistVM>(user_data, page, totalPages, pagesize: pageSize, search: search);


    }

    #region check constraind

    public bool CheckConstrians(string field, string value)
    {

        bool result = true;
        result = field switch
        {
            "email" => _userRepo.GetUserList().FirstOrDefault(o => o.Email == value) == null,
            "phone" => _userRepo.GetUserList().FirstOrDefault(o => o.PhoneNo == value) == null,
            _ => _userRepo.GetUserList().FirstOrDefault(o => o.Username == value) == null,
        };

        return result;
    }

    #endregion

    public DashboardVM Dashboard(int days, DateTime start, DateTime end)
    {
        switch (days)
        {
            case -1:
                break;
            case 30:
                end = DateTime.Now;
                start = DateTime.Now.AddDays(-30);
                break;
            case 7:
                end = DateTime.Now;
                start = DateTime.Now.AddDays(-7);
                break;
            case 1:
                end = DateTime.Now.AddDays(1);
                start = DateTime.Now;
                break;
            default:
                DateTime today = DateTime.Today;
                start = new DateTime(today.Year, today.Month, 1, 00, 00, 01);
                end = DateTime.Today.AddDays(1);
                break;
        };


        IEnumerable<Order> orders = _order.DashboardOrderdata().Where(t => t.CreatedOn >= start && t.CreatedOn <= end);
        if(!orders.Any()){
            return new DashboardVM(){
                AvgOrderWait = TimeSpan.Zero
            };
        }

        IEnumerable<Customer> customers = _customer.ReadAll().Where(t => t.CreatedOn >= start && t.CreatedOn <= end);

        int waitTokenCount = _waiting.GetAllWaitingList().Where(t => t.CreatedOn >= start && t.CreatedOn <= end).Count();

        TimeSpan totalWaitTime = TimeSpan.Zero; // Use TimeSpan to represent durations instead of DateTime
        int numOrders = orders.Count();
        List<OrderDetail> allDetails = new();

        foreach (var order in orders)
        {
            allDetails.AddRange(order.OrderDetails);
            DateTime updatedOn = order.UpdatedOn ?? DateTime.Now; // Use UpdatedOn or fallback to now
            totalWaitTime += (TimeSpan)(order.CreatedOn - updatedOn);
        }

        TimeSpan avgWaitTime = numOrders > 0 ? TimeSpan.FromTicks(totalWaitTime.Ticks / numOrders) : TimeSpan.Zero;

        List<DashboardItemVM> topSeller = allDetails.GroupBy(o => o.ItemId, (itemid, element) => new DashboardItemVM()
        {
            Name = element.First().Item.IteamName,
            Img = element.First().Item.IteamImg,
            OrderCount = element.Count()
        }).OrderByDescending(o => o.OrderCount).Take(3).ToList();

        List<DashboardItemVM> leastSeller = allDetails.GroupBy(o => o.ItemId, (itemid, element) => new DashboardItemVM()
        {
            Name = element.First().Item.IteamName,
            Img = element.First().Item.IteamImg,
            OrderCount = element.Count()

        }).OrderBy(o => o.OrderCount).Take(3).ToList();

        DashboardVM dashboard = new()
        {
            TotalSales = orders.Sum(o => o.Total),
            OrderCount = orders.Count(),
            AvgOrderValue = orders.Average(o => o.Total),
            NewCustomerCount = customers.Count(),
            TopSellingItem = topSeller,
            LeastSellingItem = leastSeller,
            AvgOrderWait = avgWaitTime,

        };

        return dashboard;


    }

    public DashboardChartJson DashboardChart(int days, DateTime start, DateTime end)
    {
        switch (days)
        {
            case -1:
                break;
            case 30:
                end = DateTime.Now.AddDays(1);
                start = DateTime.Now.AddDays(-29);
                break;
            case 7:
                end = DateTime.Now.AddDays(1);
                start = DateTime.Now.AddDays(-6);
                break;
            case 1:
                end = DateTime.Now.AddDays(1);
                start = DateTime.Now;
                break;
            default:
                DateTime today = DateTime.Today;
                start = new DateTime(today.Year, today.Month, 1, 00, 00, 01);
                end = DateTime.Today.AddDays(1);
                break;
        };
        // avg order, order count, total sales, waittime
        IEnumerable<Order> orders = _order.DashboardOrderdata().Where(t => t.CreatedOn >= start && t.CreatedOn <= end);
        
            IEnumerable<Customer> customers = _customer.ReadAll().Where(t => t.CreatedOn >= start && t.CreatedOn <= end);

            List<DashboardChartVM> revenuechart = new();

            for (var day = start; day < end; day = day.AddDays(1))
            {
                DashboardChartVM point = new()
                {
                    Day = day,
                    Value = orders.Where(o => o.CreatedOn >= day && o.CreatedOn < day.AddDays(1)).Sum(o => o.Total)
                };
                revenuechart.Add(point);
            }

            List<DashboardChartVM> customerchart = new();

            for (var day = start; day < end; day = day.AddDays(1))
            {
                DashboardChartVM point = new()
                {
                    Day = day,
                    Value = customers.Count(o => o.CreatedOn.HasValue && o.CreatedOn.Value.Date == day.Date),
                };
                customerchart.Add(point);
            }

            DashboardChartJson data = new()
            {
                Customer = customerchart,
                Revenue = revenuechart
            };
            return data;
        
    }

    public bool ResetToken(string email, int token)
    {
        return _reset.Add(new ResetToken(){
            Email = email,
            Token = token,
            CreatedOn =  DateOnly.FromDateTime(DateTime.Now),
            ValidUpto= DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
        });
    }

    public bool VerifiyToken(string email, int token)
    {
       ResetToken reset = _reset.Read(email,token);
        if (reset.ValidUpto < DateOnly.FromDateTime(DateTime.Now) || reset.Used == true)
        {
            return false;
        }
        else return true;
    }

    public bool UpdateToken(string email, int token)
    {
       ResetToken reset = _reset.Read(email,token);
        reset.Used = true;
        return _reset.Update(reset);
    }

}
