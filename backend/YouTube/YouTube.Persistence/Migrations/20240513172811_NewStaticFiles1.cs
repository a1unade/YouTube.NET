using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewStaticFiles1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BannerImgFileId",
                table: "Channels",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Channels_BannerImgFileId",
                table: "Channels",
                column: "BannerImgFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_StaticFiles_BannerImgFileId",
                table: "Channels",
                column: "BannerImgFileId",
                principalTable: "StaticFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_StaticFiles_BannerImgFileId",
                table: "Channels");

            migrationBuilder.DropIndex(
                name: "IX_Channels_BannerImgFileId",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "BannerImgFileId",
                table: "Channels");
        }
    }
}
