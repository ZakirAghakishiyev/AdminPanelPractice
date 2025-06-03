using AdminPanelPractice.Areas.Admin.Models;
using AdminPanelPractice.DataContext;
using AdminPanelPractice.DataContext.Entities;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminPanelPractice.Areas.Admin.Controllers
{
    public class TopicController : Controller
    {
        private readonly AppDbContext _dbContext;
        public TopicController(AppDbContext dbContext)
        {
                _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var topics = await _dbContext.Topics
                .ToListAsync();
            return View(topics);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TopicCreateView createView)
        {
            var topics = await _dbContext.Topics.ToListAsync();
            var topicListItems = topics.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString()}).ToList();

            if (await _dbContext.Topics.AnyAsync(t => t.Title == createView.Title))
            {
                return View();
            }


            await _dbContext.Topics.AddAsync(new Topic { Title = createView.Title, CoverImgUrl=createView.CoverImgUrl});
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
