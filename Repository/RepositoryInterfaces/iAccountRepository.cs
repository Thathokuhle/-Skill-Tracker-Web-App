using DataLogic.Account;

namespace Repository.RepositoryInterfaces
{
    public interface iAccountRepository
    {
        Task<GetUserByEmail?> GetUserByEmail(string email, CancellationToken token = default);
        Task CreateAccount(CreateAccount account);
        Task<UserRole?> GetUserRole(string email, CancellationToken token = default);
    }
}
