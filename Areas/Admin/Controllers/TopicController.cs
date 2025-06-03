using AdminPanelPractice.Areas.Admin.Data;
using AdminPanelPractice.Areas.Admin.Extentions;
using AdminPanelPractice.Areas.Admin.Models;
using AdminPanelPractice.DataContext;
using AdminPanelPractice.DataContext.Entities;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminPanelPractice.Areas.Admin.Controllers
{
    public class TopicController : AdminController
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
        public async Task<IActionResult> Create(TopicCreateViewModel createView)
        {
            var topics = await _dbContext.Topics.ToListAsync();
            var topicListItems = topics.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString()}).ToList();

            if (!ModelState.IsValid)
            {
                return View(createView);
            }

            if (!createView.CoverImageFile.IsImage())
            {
                ModelState.AddModelError("CoverImageFile", "Sekil secilmelidir!");

                return View(createView);
            }

            if (await _dbContext.Topics.AnyAsync(t => t.Title == createView.Title))
            {
                return View("CoverImageFile", "Bu adda topic artiq var");
            }

            var unicalCoverImageFileName = await createView.CoverImageFile.GenerateFile(FilePathConstants.TopicPath);


            await _dbContext.Topics.AddAsync(new Topic { Title = createView.Title, CoverImgUrl= unicalCoverImageFileName });
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] RequestModel requestModel)
        {
            var topic = await _dbContext.Topics.FindAsync(requestModel.Id);

            if (topic == null) return NotFound();

            var removedTopic = _dbContext.Topics.Remove(topic);
            await _dbContext.SaveChangesAsync();

            if (removedTopic != null)
            {
                System.IO.File.Delete(Path.Combine(FilePathConstants.TopicPath, topic.CoverImgUrl));
            }

            return Json(removedTopic.Entity);
        }
    }

    public class RequestModel
    {
        public int Id { get; set; }
        public int StartFrom { get; set; }
        public string? ImageName { get; set; }
    }
}
