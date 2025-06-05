using AdminPanelPractice.Areas.Admin.Data;
using AdminPanelPractice.Areas.Admin.Extentions;
using AdminPanelPractice.Areas.Admin.Models;
using AdminPanelPractice.DataContext;
using AdminPanelPractice.DataContext.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace AdminPanelPractice.Areas.Admin.Controllers
{
    public class SpeakerController : AdminController
    {
        private readonly AppDbContext _dbContext;
        public SpeakerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var speakers = await _dbContext.Speakers
                .Include(x => x.Topics)
                .Include(x => x.Podcasts)
                .ToListAsync();
            return View(speakers);
        }

        public async Task<IActionResult> Create()
        {
            var topics = await _dbContext.Topics.ToListAsync();
            var topicListItems = topics.Select(x => new SelectListItem(x.Title, x.Id.ToString())).ToList();

            var podcasts = await _dbContext.Podcasts.ToListAsync();
            var podcastListItems = podcasts.Select(x => new SelectListItem(x.Title, x.Id.ToString())).ToList();

            var speakerCreateModel = new SpeakerCreateViewModel
            {
                Username = "",
                Topics = topicListItems,
                Podcasts = podcastListItems,
                ProfileImg = null,
                ProfileImgUrl = ""
            };

            return View(speakerCreateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpeakerCreateViewModel model)
        {
            var topics = await _dbContext.Topics.ToListAsync();
            var topicListItems = topics.Select(x => new SelectListItem(x.Title, x.Id.ToString())).ToList();

            var podcasts = await _dbContext.Podcasts.ToListAsync();
            var podcastListItems = podcasts.Select(x => new SelectListItem(x.Title, x.Id.ToString())).ToList();

            if (!ModelState.IsValid)
            {
                model.Topics = topicListItems;
                model.Podcasts = podcastListItems;

                return View(model);
            }

            if (!model.ProfileImg.IsImage())
            {
                ModelState.AddModelError("ProfileImg", "Sekil secilmelidir!");
                model.Topics = topicListItems;
                model.Podcasts = podcastListItems;

                return View(model);
            }

            if (!model.ProfileImg.IsAllowedSize(1))
            {
                ModelState.AddModelError("ProfileImg", "Sekil hecmi 1mb-dan cox ola bilmez");
                model.Topics = topicListItems;
                model.Podcasts = podcastListItems;

                return View(model);
            }
            var unicalProfileImageFileName = await model.ProfileImg.GenerateFile(FilePathConstants.SpeakerPath);
            var speaker = new Speaker
            {
                Username = model.Username,
                Email = model.Email,
                WhatsAppLink = model.WhatsAppLink,
                InstagramLink = model.InstagramLink,
                FacebookLink = model.FacebookLink,
                YoutubeLink = model.YoutubeLink,
                IsVerified = model.IsVerified,
                ProfileImgUrl = unicalProfileImageFileName,
                Topics = await _dbContext.Topics
                    .Where(t => model.TopicIds.Contains(t.Id))
                    .ToListAsync(),
                Podcasts = await _dbContext.Podcasts
                    .Where(t => model.PodcastIds.Contains(t.Id))
                    .ToListAsync()
            };

            await _dbContext.Speakers.AddAsync(speaker);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] RequestModel requestModel)
        {
            var speaker = await _dbContext.Speakers
                 .Include(x => x.Topics)
                 .Include(x => x.Podcasts)
                .FirstOrDefaultAsync(x => x.Id == requestModel.Id);

            if (speaker == null) return NotFound();

            var removedSpeakers = _dbContext.Speakers.Remove(speaker);
            await _dbContext.SaveChangesAsync();

            if (removedSpeakers != null)
            {
                System.IO.File.Delete(Path.Combine(FilePathConstants.SpeakerPath, speaker.ProfileImgUrl));
            }

            return Json(removedSpeakers.Entity);
        }
    }
}