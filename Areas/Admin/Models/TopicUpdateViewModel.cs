namespace AdminPanelPractice.Areas.Admin.Models;

public class TopicUpdateViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? CoverImgUrl { get; set; }
    public IFormFile? CoverImageFile { get; set; }
}

