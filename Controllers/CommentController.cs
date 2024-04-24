using blogg.data;
using blogg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blogg.Controllers
{
    public class CommentController : Controller
    {
        private readonly AppDbContext context;

        public CommentController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> FetchComment(CommentModel model)
        {
            var comment = await context.CommentModels.FindAsync(model.BlogId);
            return View(comment);
        }
    }
}
