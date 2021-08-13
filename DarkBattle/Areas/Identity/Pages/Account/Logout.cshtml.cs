using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DarkBattle.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using DarkBattle.Services.Interface;
using System.Security.Claims;

namespace DarkBattle.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Player> signInManager;
        private readonly IPlayerService playerService;


        public LogoutModel(SignInManager<Player> signInManager,
                            IPlayerService playerService = null)
        {
            this.signInManager = signInManager;
            this.playerService = playerService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await signInManager.SignOutAsync();
            var playerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            this.playerService.LogOut(playerId);
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
