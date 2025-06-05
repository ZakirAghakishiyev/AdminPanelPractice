using AdminPanelPractice.DataContext.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPanelPractice.Areas.Admin.Models
{
    public class SpeakerCreateViewModel
    {
        public required string Username { get; set; }
        public required string ProfileImgUrl { get; set; }
        [NotMapped]
        public IFormFile? ProfileImg { get; set; }
        public bool IsVerified { get; set; }
        public string? InstagramLink { get; set; }
        public string? FacebookLink { get; set; }
        public string? WhatsAppLink { get; set; }
        public string? YoutubeLink { get; set; }
        public string? Email { get; set; }
        public List<int> TopicIds { get; set; } = [];
        public List<SelectListItem> Topics { get; set; } = [];
        public List<int> PodcastIds { get; set; } = [];

        public List<SelectListItem> Podcasts { get; set; } = [];
    }
}
