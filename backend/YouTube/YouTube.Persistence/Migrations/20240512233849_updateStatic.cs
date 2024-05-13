using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateStatic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StaticFiles_ChannelId",
                table: "StaticFiles");

            migrationBuilder.DropColumn(
                name: "BannerImgId",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "MainImgId",
                table: "Channels");

            migrationBuilder.AddColumn<int>(
                name: "ChannelId1",
                table: "StaticFiles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaticFiles_ChannelId",
                table: "StaticFiles",
                column: "ChannelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaticFiles_ChannelId1",
                table: "StaticFiles",
                column: "ChannelId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StaticFiles_Channels_ChannelId1",
                table: "StaticFiles",
                column: "ChannelId1",
                principalTable: "Channels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_Channels_ChannelId1",
                table: "StaticFiles");

            migrationBuilder.DropIndex(
                name: "IX_StaticFiles_ChannelId",
                table: "StaticFiles");

            migrationBuilder.DropIndex(
                name: "IX_StaticFiles_ChannelId1",
                table: "StaticFiles");

            migrationBuilder.DropColumn(
                name: "ChannelId1",
                table: "StaticFiles");

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

            migrationBuilder.CreateIndex(
                name: "IX_StaticFiles_ChannelId",
                table: "StaticFiles",
                column: "ChannelId");
        }
    }
}
