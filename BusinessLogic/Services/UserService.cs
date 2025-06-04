using BusinessLogic.LogicInterfaces;

namespace BusinessLogic.Services
{ 
    public class UserService
    {
        
        public readonly iAccountManager _iAccount;

        public UserService(iAccountManager iAccount) 
        { 
           
            _iAccount = iAccount;
        }

        public async Task<bool> IsUserActive(string email)
        {
            var userDetails = await _iAccount.GetUserByEmail(email);
            return userDetails != null && userDetails.IsActive;
        }
        public async Task<bool> IsUserAdmin(string userEmail)
        {
            bool isadmin = await _iAccount.IsUserAdmin(userEmail);
            if (isadmin) return true;
            return false;

        }
    }
}