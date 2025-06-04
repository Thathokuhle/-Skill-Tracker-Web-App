using BusinessLogic.LogicInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewLogic.AccountViews;

namespace SkillTrackerApp.Pages.Profile
{
    [Authorize]
    public class UserProfileModel : PageModel
    {

        [BindProperty]
        public GetUserByEmailView view { get; set; }
        public iAccountManager manager;

        public UserProfileModel(iAccountManager iManager)
        {
            manager = iManager;
            view = new GetUserByEmailView();

        }
        public async Task OnGet()
        {

            var email = User.Identity?.Name;
            if (email != null)
            {
                view = await manager.GetUserByEmail(email) ?? new GetUserByEmailView();
            }
            else
            {
                RedirectToPage("/Identity/Account/Login");
            }
        }
    }
}