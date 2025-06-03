using AdminPanelPractice.DataContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminPanelPractice.DataContext;

public class DataInitializer
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // Seed Speakers
        var speaker1 = new Speaker
        {
            Id = 1,
            Username = "john_doe",
            ProfileImgUrl = "/images/speakers/john.png",
            IsVerified = true,
            InstagramLink = "https://instagram.com/johndoe",
            FacebookLink = null,
            WhatsAppLink = null,
            YoutubeLink = "https://youtube.com/johndoe",
            Email = "john@example.com"
        };

        var speaker2 = new Speaker
        {
            Id = 2,
            Username = "jane_smith",
            ProfileImgUrl = "/images/speakers/jane.png",
            IsVerified = false,
            InstagramLink = null,
            FacebookLink = "https://facebook.com/janesmith",
            WhatsAppLink = "https://wa.me/123456789",
            YoutubeLink = null,
            Email = "jane@example.com"
        };

        // Seed Topics
        var topic1 = new Topic
        {
            Id = 1,
            Title = "Technology",
            CoverImgUrl = "/images/topics/tech.png"
        };

        var topic2 = new Topic
        {
            Id = 2,
            Title = "Health & Wellness",
            CoverImgUrl = "/images/topics/health.png"
        };

        // Seed SpeakerTopics (optional if needed for many-to-many manual junction)
        var speakerTopic1 = new SpeakerTopic
        {
            Id = 1,
            SpeakerId = 1,
            TopicId = 1
        };

        var speakerTopic2 = new SpeakerTopic
        {
            Id = 2,
            SpeakerId = 2,
            TopicId = 2
        };

        // Seed Podcasts
        var podcast1 = new Podcast
        {
            Id = 1,
            Title = "The Future of AI",
            CoverImgUrl = "/images/podcasts/ai.png",
            ShortDescription = "An overview of upcoming trends in artificial intelligence.",
            Description = "In this episode, we explore the possibilities and challenges of AI in modern society...",
            AudioUrl = "/audio/future-of-ai.mp3",
            LikeCount = 120,
            CommentCount = 45,
            ListenCount = 1020,
            DownloadCount = 300,
            Duration = 1800, // seconds
            Episode = 1,
            TopicId = 1,
            SpeakerId = 1
        };

        var podcast2 = new Podcast
        {
            Id = 2,
            Title = "Mental Health Tips",
            CoverImgUrl = "/images/podcasts/mental-health.png",
            ShortDescription = "Tips for improving your mental well-being.",
            Description = "In this talk, Jane shares simple and effective strategies for daily mental health management...",
            AudioUrl = "/audio/mental-health.mp3",
            LikeCount = 80,
            CommentCount = 20,
            ListenCount = 780,
            DownloadCount = 200,
            Duration = 1500,
            Episode = 1,
            TopicId = 2,
            SpeakerId = 2
        };

        // Apply seeding
        modelBuilder.Entity<Speaker>().HasData(speaker1, speaker2);
        modelBuilder.Entity<Topic>().HasData(topic1, topic2);
        modelBuilder.Entity<SpeakerTopic>().HasData(speakerTopic1, speakerTopic2);
        modelBuilder.Entity<Podcast>().HasData(podcast1, podcast2);
    }
}
