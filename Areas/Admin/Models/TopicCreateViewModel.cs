namespace AdminPanelPractice.Areas.Admin.Models
{
    public class TopicCreateViewModel
    {
        public required string Title { get; set; }
        public required IFormFile CoverImageFile { get; set; }

    }
}
