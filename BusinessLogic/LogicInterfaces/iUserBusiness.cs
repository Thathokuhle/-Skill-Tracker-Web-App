namespace BusinessLogic.LogicInterfaces
{
    public interface IUserBusiness
    {
        Task<bool> IsUserAdmin(string email);
        Task<bool> HasFullAdminAccess(string email);
        Task<bool> IsUserActive(string email);
    }
}
