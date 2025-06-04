using BusinessLogic.Services;
using DataLogic.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SkillTrackerApp.DataLogic.User;

public class CheckUserActiveMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CheckUserActiveMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User?.Identity?.IsAuthenticated == true)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var userService = scope.ServiceProvider.GetRequiredService<UserService>();
                var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
                var userEmail = context.User.Identity.Name;
                if (userEmail != null)
                {
                    var isActive = await userService.IsUserActive(userEmail);
                    if (!isActive)
                    {
                        await signInManager.SignOutAsync();
                        context.Response.Redirect("/Identity/Account/Login");
                        return;
                    }
                }
            }
        }

        await _next(context);

    }
}
