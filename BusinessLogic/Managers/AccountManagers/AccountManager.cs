using AutoMapper;
using BusinessLogic.LogicInterfaces;
using BusinessLogic.StaticMappings;
using DataLogic.Account;
using Repository.RepositoryInterfaces;
using ViewLogic.AccountViews;

namespace BusinessLogic.Managers.AccountManagers
{ 
    public class AccountManager: iAccountManager
    {
        public iAccountRepository _iAccount;

        public AccountManager(iAccountRepository iAccount) 
        { 
            _iAccount = iAccount;
        }

        public async Task<GetUserByEmailView?> GetUserByEmail(string? email)
        {
            if(email != null)
            {
                var userDetails = await _iAccount.GetUserByEmail(email);
                if (userDetails != null)
                    return ObjectMapper.Mapper.Map<GetUserByEmail, GetUserByEmailView>(userDetails);
                return null;
            }
            return null;
        }

        public async Task CreateAccount(CreateAccountView account)
        {
            var userDetails = ObjectMapper.Mapper.Map<CreateAccountView, CreateAccount>(account);
            await _iAccount.CreateAccount(userDetails);
        }

        public async Task<bool> IsUserAdmin(string email)
        {
            var userRole = await _iAccount.GetUserRole(email);
            return userRole != null && (userRole.Role == "Administrator" || userRole.Role == "ViewAdmin");
        }

        public async Task<bool> HasFullAdminAccess(string email)
        {
            var userRole = await _iAccount.GetUserRole(email);
            return userRole != null && userRole.Role == "Administrator";
        }

        public async Task<bool> IsUserActive(string email)
        {
            var userDetails = await _iAccount.GetUserByEmail(email);
            return userDetails != null && userDetails.IsActive;
        }
    }
}