using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Data;
using System.Security.Claims;
using Taskydo.Models;
using Taskydo.Services;

namespace Taskydo.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ApplicationDbContext context;

        public UserController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser() { Email = model.email, UserName = model.email };

            var result = await userManager.CreateAsync(user, password: model.password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
        }

        [AllowAnonymous]
        public IActionResult Login(string message = null)
        {
            if (message is not null)
            {
                ViewData["message"] = message;
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Console.WriteLine("ENtro");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(model.email,
                model.password, model.rememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                Console.WriteLine("Exito");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Incorrect username or password.");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public ChallengeResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectionUrl = Url.Action("RegisterExternalUser", values: new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectionUrl);

            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> RegisterExternalUser(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var message = "";

            if (remoteError is not null)
            {
                message = $"External provider error: {remoteError}";
                return RedirectToAction("Login", routeValues: new { message });
            }

            var info = await signInManager.GetExternalLoginInfoAsync();

            if (info is null)
            {
                message = "Error loading external login data";
                return RedirectToAction("Login", routeValues: new { message });
            }

            var externalLoginResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true);

            if (externalLoginResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            string email = "";

            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                email = info.Principal.FindFirstValue(ClaimTypes.Email);
            }
            else
            {
                message = "Error reading email from supplier user";
                return RedirectToAction("Login", routeValues: new { message });
            }

            var user = new IdentityUser { Email = email, UserName = email };

            var createUserResult = await userManager.CreateAsync(user);

            if (!createUserResult.Succeeded)
            {
                message = createUserResult.Errors.First().Description;
                return RedirectToAction("Login", routeValues: new { message });
            }

            var AddLoginResult = await userManager.AddLoginAsync(user, info);

            if (AddLoginResult.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: true, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }

            message = "An error occurred while adding the login";
            return RedirectToAction("Login", routeValues: new { message });
        }

        [HttpGet]
        [Authorize(Roles = Constants.RolAdmin)]
        public async Task<IActionResult> ListUsers(string message = null)
        {
            var users = await context.Users.Select(u => new UserViewModel
            {
                email = u.Email
            }).ToListAsync();

            var model = new ListUserViewModel();
            model.users = users;
            model.message = message;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Constants.RolAdmin)]
        public async Task<IActionResult> MakeAdmin(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound();
            }

            await userManager.AddToRoleAsync(user, Constants.RolAdmin);

            return RedirectToAction("ListUsers", 
                routeValues: new { message = $"Role assigned correctly to {email}" });
        }

        [HttpPost]
        [Authorize(Roles = Constants.RolAdmin)]
        public async Task<IActionResult> RemoveAdmin(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound();
            }

            await userManager.RemoveFromRoleAsync(user, Constants.RolAdmin);

            return RedirectToAction("ListUsers",
                routeValues: new { message = $"Role removed correctly to {email}" });
        }
    }
}
