using MedicioTemplate.Models;
using MedicioTemplate.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MedicioTemplate.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {

        readonly SignInManager<AppUser> _signInManager;

        readonly UserManager<AppUser> _userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM newUser)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = new AppUser
            {
                Name = newUser.Name,
                Surname = newUser.Surname,
                Email = newUser.Email,
                UserName = newUser.Username

            };

            IdentityResult result = await _userManager.CreateAsync(user, newUser.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM user, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser existed = await _userManager.FindByNameAsync(user.Username);
            if (existed == null)
            {


                ModelState.AddModelError(string.Empty, "Username or Password is not correct");
                return View();

            }
            var result = await _signInManager.PasswordSignInAsync(existed, user.Password, user.IsRemember, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Login is not available now, please try again later");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username or Password is not correct");
                return View();
            }
            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);

            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new {area=""});
        }




    }



    }
}



