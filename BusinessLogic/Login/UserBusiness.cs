using BusinessLogic.LogicInterfaces;

namespace BusinessLogic.Login
{
    public class UserBusiness: IUserBusiness
    {
        private readonly iAccountManager _accountManager;

        public UserBusiness(iAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public async Task<bool> IsUserAdmin(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return await _accountManager.IsUserAdmin(email);
        }

        public async Task<bool> HasFullAdminAccess(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return await _accountManager.HasFullAdminAccess(email);
        }

        public async Task<bool> IsUserActive(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return await _accountManager.IsUserActive(email);
        }
    }
}
