using blogg.data;
using blogg.Models;
using blogg.Models.viewModel.BlogViewModel;
using blogg.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blogg.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly AppDbContext context;
        private readonly UserManager<AppUSer> userManager;
        private readonly IImageService imageService;

        public BlogController(AppDbContext context,
            UserManager<AppUSer> userManager,IImageService imageService)
        {
            this.context = context;
            this.userManager = userManager;
            this.imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var blogs = await context.blogModels.ToListAsync();
            return View(blogs);
        }
        [HttpGet]
       public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostModel post)
        {

            if (ModelState.IsValid)
            {
                string Imageurl="";
                if (post.ImageFile != null && post.ImageFile.Length > 0)
                {

                    Imageurl = await imageService.uploadImageAsync(post.ImageFile);
                }


                var blog = new BlogModel()
                {
                    Title = post.Title,
                    Description = post.Description,
                    AuthorId = userManager.GetUserId(User)!,
                    ImageUrl = Imageurl,


                };
                context.blogModels.Add(blog);
                await context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
        
            }
            return View(post);
        }
    }
}
