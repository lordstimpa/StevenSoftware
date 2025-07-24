using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StevenSoftware.Server.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToBlogPostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImage",
                table: "BlogPosts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "BlogPosts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "BlogPosts");
        }
    }
}
