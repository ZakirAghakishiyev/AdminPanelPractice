using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdminPanelPractice.Migrations
{
    /// <inheritdoc />
    public partial class dataInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Speakers",
                columns: new[] { "Id", "Email", "FacebookLink", "InstagramLink", "IsVerified", "ProfileImgUrl", "Username", "WhatsAppLink", "YoutubeLink" },
                values: new object[,]
                {
                    { 1, "john@example.com", null, "https://instagram.com/johndoe", true, "/images/speakers/john.png", "john_doe", null, "https://youtube.com/johndoe" },
                    { 2, "jane@example.com", "https://facebook.com/janesmith", null, false, "/images/speakers/jane.png", "jane_smith", "https://wa.me/123456789", null }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "CoverImgUrl", "Title" },
                values: new object[,]
                {
                    { 1, "/images/topics/tech.png", "Technology" },
                    { 2, "/images/topics/health.png", "Health & Wellness" }
                });

            migrationBuilder.InsertData(
                table: "Podcasts",
                columns: new[] { "Id", "AudioUrl", "CommentCount", "CoverImgUrl", "Description", "DownloadCount", "Duration", "Episode", "LikeCount", "ListenCount", "ShortDescription", "SpeakerId", "Title", "TopicId" },
                values: new object[,]
                {
                    { 1, "/audio/future-of-ai.mp3", 45, "/images/podcasts/ai.png", "In this episode, we explore the possibilities and challenges of AI in modern society...", 300, 1800, 1, 120, 1020, "An overview of upcoming trends in artificial intelligence.", 1, "The Future of AI", 1 },
                    { 2, "/audio/mental-health.mp3", 20, "/images/podcasts/mental-health.png", "In this talk, Jane shares simple and effective strategies for daily mental health management...", 200, 1500, 1, 80, 780, "Tips for improving your mental well-being.", 2, "Mental Health Tips", 2 }
                });

            migrationBuilder.InsertData(
                table: "SpeakersTopics",
                columns: new[] { "Id", "SpeakerId", "TopicId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Podcasts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Podcasts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SpeakersTopics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SpeakersTopics",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Speakers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Speakers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
