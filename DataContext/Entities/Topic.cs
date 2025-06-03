namespace AdminPanelPractice.DataContext.Entities
{
    public class Topic
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string CoverImgUrl { get; set; }
        public List<Speaker> Speakers { get; set; } = [];
        public List<Podcast> Podcasts { get; set; } = [];

    }
}
