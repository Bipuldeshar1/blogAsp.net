using blogg.Models;
using blogg.Models.viewModel.RoleViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace blogg.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUSer> userManager;

        public RoleController(RoleManager<IdentityRole>roleManager,
            UserManager<AppUSer>userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityrole = new IdentityRole {
                    Name = model.RoleName
                };

              var result= await roleManager.CreateAsync(identityrole);
                if(result.Succeeded)
                {
                    return RedirectToAction("ListRoles","Role");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("ListRoles", "Role");
            }

            // Fetch users who belong to the role
            var usersInRole = await userManager.GetUsersInRoleAsync(role.Name);

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                Users = usersInRole.Select(user => user.UserName).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                return RedirectToAction("ListRoles", "Role");
            }
            else
            {
                role.Name = model.RoleName;
              var result=await  roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var e in result.Errors) {
                    ModelState.AddModelError("", e.Description);
                        }

            }
            

            return View(model);
        }

    }
}
