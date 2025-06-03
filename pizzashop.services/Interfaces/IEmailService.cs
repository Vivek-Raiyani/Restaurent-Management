using pizzashop.data.Models;
using pizzashop.data.ViewModels;

namespace pizzashop.services.Interfaces;

public interface IEmailService
{
    void SendMail(string email ,string reseturl);

    public void NewUserMail(ProfileVM user);
}
