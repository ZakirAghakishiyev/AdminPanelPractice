using AdminPanelPractice.DataContext.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminPanelPractice.DataContext;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Podcast> Podcasts { get; set; }
    public DbSet<Speaker> Speakers { get; set; }
    public DbSet<SpeakerTopic> SpeakersTopics { get; set; }
    public DbSet<Topic> Topics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        DataInitializer.Seed(modelBuilder);
    }
}
