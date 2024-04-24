using blogg.data;
using blogg.Models;
using blogg.Models.viewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace blogg.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUSer> signInManager;
        private readonly UserManager<AppUSer> userManager;

        public AccountController(SignInManager<AppUSer> signInManager,UserManager<AppUSer>userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult Register() {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel) {
            var user= new AppUSer{ 
             Email = registerModel.Email,
             PhoneNumber=Convert.ToString(registerModel.PhoneNumber),
             UserName=registerModel.UserName,
             NickName=registerModel.UserName,

            };
            if (ModelState.IsValid)
            {
                var result = await userManager.CreateAsync(user,registerModel.Password);
                if(result.Succeeded) {
                 // await userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(registerModel);
         
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await userManager.FindByEmailAsync(loginModel.Email);
            string returnUrl = HttpContext.Request.Query["ReturnUrl"]!;
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync
            (user.UserName!, loginModel.Password, false, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Read", "Blog");
                }

                ModelState.AddModelError("", "incorrect login cred");
                return View(loginModel);
            }
            return View(loginModel);
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var x =  userManager.GetUserId(User);

            var user=await userManager.FindByIdAsync(x);
            if (ModelState.IsValid)
            {
                var result = await userManager.ChangePasswordAsync(user,changePasswordModel.CurrentPassword,changePasswordModel.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
