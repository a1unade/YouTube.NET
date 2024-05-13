using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStaticFileProp2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerImg",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "MainImg",
                table: "Channels");

            migrationBuilder.AddColumn<int>(
                name: "BannerImgId",
                table: "Channels",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MainImgId",
                table: "Channels",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerImgId",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "MainImgId",
                table: "Channels");

            migrationBuilder.AddColumn<string>(
                name: "BannerImg",
                table: "Channels",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainImg",
                table: "Channels",
                type: "text",
                nullable: true);
        }
    }
}
