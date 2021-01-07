using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store_Web.Data.Enteties;
using Store_Web.Helpers;
using Store_Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper userHelper;

        public AccountController(IUserHelper userHelper)
        {
            this.userHelper = userHelper;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to Login");

            return this.View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await this.userHelper.LogoutAsync();
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.UserName);
                if (user == null)
                {
                    user = new User
                    {
                        FristName = model.FristName,
                        LastName = model.LastName,
                        Email = model.UserName,
                        UserName = model.UserName
                    };

                    var result = await this.userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);
                    }

                    var loginViewModel = new LoginViewModel
                    {
                        Username = model.UserName,
                        Password = model.Password,
                        RememberMe = false

                    };

                    var rslt = await this.userHelper.LoginAsync(loginViewModel);

                    if (rslt.Succeeded)
                    {
                        return this.RedirectToAction("Index", "Home");
                    }

                    this.ModelState.AddModelError(string.Empty, "The user couldn't be Login");
                    return this.View(model);

                }
                this.ModelState.AddModelError(string.Empty, "Username is already in use");
            }
            return this.View(model);
        }


        public async Task<IActionResult> ChangeUser()
        {
            var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new ChangeUserViewModel();

            if(user != null)
            {
                model.FristName = user.FristName;
                model.LastName = user.LastName;
            }
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult>ChangeUser(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    user.FristName = model.FristName;
                    user.LastName = model.LastName;
                    var rspns = await this.userHelper.ChangePasswordAsync(user);
                    if (rspns.Succeeded)
                    {
                        this.ViewBag.UserMessage = "Update Succeed";
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, rspns.Errors.FirstOrDefault().Description);
                    }


                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User wasn't found");
                }

            }
            return this.View(model);
        }


        public IActionResult ChangePassword()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                   
                    var rspns = await this.userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (rspns.Succeeded)
                    {
                        this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, rspns.Errors.FirstOrDefault().Description);
                    }


                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User wasn't found");
                }

            }
            return this.View(model);
        }












    }
}
