using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PoliziaWebApp.Interfaces;
using PoliziaWebApp.Models;

namespace PoliziaWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService authenticationService;

        public AccountController(IAuthService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Utente user)
        {
            try
            {
                var u = authenticationService.Login(user.Username, user.Password);
                if (u == null) return RedirectToAction("Index", "Home");

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, u.Username)
                };
                u.Roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r)));
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                     new ClaimsPrincipal(identity)
                     );
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
