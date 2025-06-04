using DataLogic;
using DataLogic.Account;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.GenericRepository;
using Repository.RepositoryInterfaces;

namespace Repository.ImplementedRepositories.AccountRepositories
{ 
    public class AccountRepository: GenericRepository<GetUserByEmail>,iAccountRepository
    {
        private DefaultContext _context;
        public AccountRepository(DefaultContext dataContext) : base(dataContext) 
        {
            _context = dataContext;
        }
        public async Task<GetUserByEmail?> GetUserByEmail(string email, CancellationToken token = default)
        {
            object[] parameters =
            {
                new SqlParameter("@email", email)
            };
            const string query = "EXEC [GetUserByEmail] @email";
            var queryResult = await _context.Set<GetUserByEmail>().FromSqlRaw(query, parameters).ToListAsync(token);
            return queryResult.FirstOrDefault();
        }

        public async Task CreateAccount(CreateAccount account)
        {
            SqlParameter[] parameters =
            {
                    new SqlParameter("@FirstName", account.FirstName ),
                    new SqlParameter("@LastName", account.LastName ),
                    new SqlParameter("@EmailAddress", account.EmailAddress ),
                    new SqlParameter("@DateOfBirth", account.DateOfBirth ),
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC [CreateAccount] @FirstName, @LastName, @EmailAddress, @DateOfBirth", parameters);
        }

        public async Task<UserRole?> GetUserRole(string email, CancellationToken token = default)
        {
            object[] parameters =
            {
                new SqlParameter("@email", email)
            };
            const string query = "EXEC [GetUserRole] @email";
            var queryResult =await _context.Set<UserRole>().FromSqlRaw(query, parameters).ToListAsync(token);
            return queryResult.FirstOrDefault();
        }
    }
}