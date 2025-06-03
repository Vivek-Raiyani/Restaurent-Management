using pizzashop.data.Models;

namespace pizzashop.repository.Interfaces;

public interface IResetTokenReposiotry
{
    public bool Add(ResetToken token);
    public ResetToken Read(string email, int token);

    public bool Update(ResetToken token);
}