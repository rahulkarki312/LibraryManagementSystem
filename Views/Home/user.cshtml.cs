using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Views
{

    [Authorize]
    public class userModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        public ApplicationUser? appUser;

        public userModel(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public void OnGet()
        {
            var task = userManager.GetUserAsync(User);
            task.Wait();
            appUser = task.Result;
        }
    }
}
