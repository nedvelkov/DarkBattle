namespace DarkBattle.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PlayersController : Controller
    {
        private readonly UserManager<IdentityUser> userManger;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public PlayersController(UserManager<IdentityUser> userManger,
                                SignInManager<IdentityUser> signInManager,
                                RoleManager<IdentityRole> roleManager)
        {
            this.userManger = userManger;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }


        public async Task<IActionResult> UserInfo()
        {
            var user = await this.userManger.GetUserAsync(this.User);

            return this.Json(user);
        }

        public async Task<IActionResult> AddAdmin()
        {
            if(await this.roleManager.RoleExistsAsync("Admin") == false)
            {
                await this.roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin"
                });
            }
            return Redirect("/");
        }

    }
}
