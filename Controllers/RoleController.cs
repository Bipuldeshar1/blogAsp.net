using blogg.Models;
using blogg.Models.viewModel.RoleViewModel;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string Id) {
            ViewBag.roleId = Id;
            var role = await roleManager.FindByIdAsync(Id);
            if (role == null) 
            {
                return RedirectToAction("RoleNotFound", "Error");
            }
      
            var model = new List<UserRoleViewModel>();
            foreach(var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId= user.Id,
                    UserName=user.UserName,
                    
                };
                if(await userManager.IsInRoleAsync(user, role.Name)) {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
             model.Add(userRoleViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel>model,string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                return RedirectToAction("RoleNotFound", "Error");
            }
            for(int i = 0; i < model.Count; i++)
            {
             var user=await userManager.FindByIdAsync(Convert.ToString(userManager.GetUserId(User)!));
                
                IdentityResult result=null;
                if (model[i].IsSelected &&  !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                   result=await userManager.AddToRoleAsync(user,role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name)) {
                result=await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < model.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole",new {Id = Id});
                    }
                }
            }
            return RedirectToAction("EditRole", new { Id = Id });
            
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("ListRoles", "Role");
            }
            var result= await roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles", "Role");
            }
            else
            {
            
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListRoles", "Role");
            }
            return View("ListRoles","Role");
        } 
      }
}
