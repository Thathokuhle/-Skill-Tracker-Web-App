using BusinessLogic.LogicInterfaces;
using BusinessLogic.Token;
using Microsoft.AspNetCore.Mvc;
using SkillTrackerApp.DataLogic;
using System.Diagnostics;

namespace SkillTrackerApp.Controllers
{
    public class HomeController : Controller
    {
        public iAccountManager _manager;
        public HomeController(iAccountManager iAccount)
        {
            _manager = iAccount;
        }
        public async Task<string> Index()
        {
            if (User?.Identity?.Name == null)
            {
                return "User Not Found";
            }

            var user = await _manager.GetUserByEmail(User.Identity.Name);

            if (user == null) { return "User Not Found"; }

            return TokenBusiness.GenerateJsonWebToken(user);
        }
    }
}