using Microsoft.AspNetCore.Identity;

namespace AdminPanelPractice.DataContext.Entities
{
    public class Speaker
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string ProfileImgUrl { get; set; }
        public bool IsVerified { get; set; }
        public string? InstagramLink { get; set; }
        public string? FacebookLink { get; set; }
        public string? WhatsAppLink { get; set; }
        public string? YoutubeLink { get; set; }
        public string? Email { get; set; }
        public List<Topic> Topics { get; set; } = new List<Topic>();
        public List<Podcast> Podcasts { get; set; } = [];
    }
}
