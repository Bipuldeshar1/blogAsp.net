using blogg.data;
using blogg.Models;
using blogg.Models.viewModel.BlogViewModel;
using blogg.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace blogg.Controllers
{
  
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
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var product= await context.blogModels.FindAsync(id);
            if(product == null)
            {
                return RedirectToAction("Read", "Blog");
            }
            var pro = new PostModel() { 
            Title= product.Title,
            Description= product.Description,
          
            };
            ViewData["Id"] = product.Id;
            ViewData["img"] = product.ImageUrl;
            return View(pro);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Edit(int id,PostModel postModel)
        {
            var product = await context.blogModels.FindAsync(id);
            if (product == null)
            {
                return RedirectToAction("Read", "Blog");
            }
            if (ModelState.IsValid)
            {
                string Imageurl = "";
                if (postModel.ImageFile != null && postModel.ImageFile.Length > 0)
                {

                    Imageurl = await imageService.uploadImageAsync(postModel.ImageFile);
                }

                product.Title = postModel.Title;
                product.Description=postModel.Description;
                product.ImageUrl = Imageurl;
              
                await context.SaveChangesAsync();
                return RedirectToAction("Read", "Blog");

            }
            return View(product);

        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product =await context.blogModels.FindAsync(id);
            if(product == null)
            {
                return RedirectToAction("Read", "Blog");
            }
            context.blogModels.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Read","Blog");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var blog = context.blogModels.Include(e => e.appUSer).Include(e=>e.commentModels).SingleOrDefault(b => b.Id == id);

            if (blog == null)
            {
                return NotFound();
            }
        
            return View(blog);
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var blogs = await context.blogModels.ToListAsync();
            return View(blogs);
        }
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(int blogId,string content)
        {
            var userId=userManager.GetUserId(User);
            var cmt = new CommentModel()
            {
                BlogId =blogId,
                UserId = userId,
                Comment = content
            };
          
            context.cmtModel.Add(cmt);
            await context.SaveChangesAsync();

            return RedirectToAction("Detail", new{id=blogId});
        }
    }
}
