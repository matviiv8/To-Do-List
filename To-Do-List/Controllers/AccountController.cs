using Microsoft.AspNetCore.Mvc;
using To_Do_List.Models;
using To_Do_List.ViewModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace To_Do_List.Controllers
{
    public class AccountController : Controller
    {
        private UserContext context;

        public AccountController(UserContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register (Register model)
        {
            if (ModelState.IsValid)
            {
                User user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if(user == null)
                {
                    user = new User { Email = model.Email, Password = model.Password};
                    Role userRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "user");

                    if(userRole != null)
                    {
                        user.Role= userRole;
                    }

                    context.Users.Add(user);

                    await context.SaveChangesAsync();

                    await Authentificate(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect login and(or) password");
                }
            }


            return View(model);
        }

        private async Task Authentificate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                User user = await context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    await Authentificate(user);

                    return RedirectToAction("Index" ,"Home");
                }
                ModelState.AddModelError(string.Empty, "Incorrect login and(or) password");
            }
            return View(model);
        }
    }
}
