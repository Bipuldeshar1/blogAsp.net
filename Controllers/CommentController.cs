//using blogg.data;
//using blogg.Models;
//using blogg.Models.viewModel.BlogViewModel;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace blogg.Controllers
//{
//    public class CommentController : Controller
//    {
//        private readonly AppDbContext context;

//        public CommentController(AppDbContext context)
//        {
//            this.context = context;
//        }
//        [HttpGet]
//        public async Task<IActionResult> FetchComment(string id)
//        {
//            var comment = await context.CommentModels.Where(c=>c.BlogId==id).ToListAsync();
            
//            return View(comment);
//        }
       
//        [HttpPost]
//        [Authorize]
//        public async Task<IActionResult> AddComment(BLogDetailViewModel model)
//        {
//            var cmt = new CommentModel() {
//                BlogId = model.Comments.BlogId,
//            userId = model.Comments.userId,
//            comment=model.Comments.comment,
//            };
//            var result = await context.CommentModels.AddAsync(cmt);
//            await context.SaveChangesAsync();

//            return RedirectToAction("Read", "Blog");
//        }
//    }
//}
