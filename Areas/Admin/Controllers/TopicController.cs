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

        public async Task<IActionResult> Update(int id)
        {
            var topic = await _dbContext.Topics.FindAsync(id);
            if (topic == null) return NotFound();

            var viewModel = new TopicUpdateViewModel
            {
                Id = topic.Id,
                Title = topic.Title,
                CoverImgUrl = topic.CoverImgUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(TopicUpdateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var topic = await _dbContext.Topics.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (topic == null) return NotFound();

            topic.Title = model.Title;

            if (model.CoverImageFile != null)
            {
                if (!model.CoverImageFile.IsImage())
                {
                    ModelState.AddModelError("CoverImageFile", "Sekil secilmelidir!");
                    return View(model);
                }

                if (!model.CoverImageFile.IsAllowedSize(1))
                {
                    ModelState.AddModelError("CoverImageFile", "Sekil hecmi 1mb-dan cox ola bilmez");
                    return View(model);
                }

                var uniqueFileName = await model.CoverImageFile.GenerateFile(FilePathConstants.TopicPath);

                if (!string.IsNullOrWhiteSpace(topic.CoverImgUrl))
                {
                    var oldImagePath = Path.Combine(FilePathConstants.TopicPath, topic.CoverImgUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                topic.CoverImgUrl = uniqueFileName;
            }

            _dbContext.Topics.Update(topic);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }



    public class RequestModel
    {
        public int Id { get; set; }
        public int StartFrom { get; set; }
        public string? ImageName { get; set; }
    }
}
