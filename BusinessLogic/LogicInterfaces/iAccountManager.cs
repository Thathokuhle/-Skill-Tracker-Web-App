
using ViewLogic.AccountViews;

namespace BusinessLogic.LogicInterfaces
{
    public interface iAccountManager
    {
        Task<GetUserByEmailView?> GetUserByEmail(string? email);
        Task CreateAccount(CreateAccountView account);
        Task<bool> IsUserAdmin(string? email);
        Task<bool> HasFullAdminAccess(string email);
        Task<bool> IsUserActive(string email);
    }
}
