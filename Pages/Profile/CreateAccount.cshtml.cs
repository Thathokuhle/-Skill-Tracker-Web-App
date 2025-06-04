using BusinessLogic.LogicInterfaces;
using BusinessLogic.URLEncryptionBusiness;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewLogic.AccountViews;

namespace SkillTrackerApp.Pages.Profile
{
    public class CreateAccountModel : PageModel
    {
        [BindProperty]
        public CreateAccountView createAccount { get; set; }
        public GetUserByEmailView? userAccount { get; set; }
        public string SuccessMessage { get; set; } = string.Empty;
        [BindProperty]
        public string? EmailAddress { get; set; }

        public iAccountManager _manager;
        private readonly IEncryptDecrypt _decrypt;

        public CreateAccountModel(iAccountManager manager, IEncryptDecrypt encryptDecrypt)
        {
            _manager = manager;
            _decrypt = encryptDecrypt;
            createAccount = new CreateAccountView();
            userAccount = new GetUserByEmailView();
        }
        public async Task<IActionResult> OnGet(string emailAddress)
        {
            string email = _decrypt.DecryptString(emailAddress);
            EmailAddress = email;
            if (EmailAddress == null) return RedirectToPage("/Identity/Account/Login");

            userAccount = await _manager.GetUserByEmail(EmailAddress);
            if (userAccount != null)
            {
                SuccessMessage = "Email Already Exists, Please Login";
                return RedirectToPage("/Identity/Account/Login");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _manager.CreateAccount(createAccount);
                SuccessMessage = "Account Succesfully Created. Please Login With Your Email";
                return Redirect("/Index");
            }
            else
            {
                EmailAddress = createAccount.EmailAddress;
                return Page();
            }
        }
    }
}
