using BusinessLogic.LogicInterfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewLogic;

namespace SkillTrackerApp.Pages
{
    public class IndexModel : PageModel
    {

        public IndexModel(ILogger<IndexModel> logger)
        {

        }

        public void OnGetAsync()
        {
            Page();
        }
    }
}