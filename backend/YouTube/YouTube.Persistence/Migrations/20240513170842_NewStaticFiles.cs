using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewStaticFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_Channels_BannerChannelId",
                table: "StaticFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_Channels_MainChannelId",
                table: "StaticFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_Videos_VideoId",
                table: "StaticFiles");

            migrationBuilder.DropIndex(
                name: "IX_StaticFiles_BannerChannelId",
                table: "StaticFiles");

            migrationBuilder.DropIndex(
                name: "IX_StaticFiles_MainChannelId",
                table: "StaticFiles");

            migrationBuilder.DropIndex(
                name: "IX_StaticFiles_VideoId",
                table: "StaticFiles");

            migrationBuilder.DropColumn(
                name: "BannerChannelId",
                table: "StaticFiles");

            migrationBuilder.DropColumn(
                name: "MainChannelId",
                table: "StaticFiles");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "StaticFiles");

            migrationBuilder.AddColumn<int>(
                name: "PreviewImgId",
                table: "Videos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Videos_PreviewImgId",
                table: "Videos",
                column: "PreviewImgId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Channels_MainImgId",
                table: "Channels",
                column: "MainImgId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_StaticFiles_MainImgId",
                table: "Channels",
                column: "MainImgId",
                principalTable: "StaticFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_StaticFiles_PreviewImgId",
                table: "Videos",
                column: "PreviewImgId",
                principalTable: "StaticFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_StaticFiles_MainImgId",
                table: "Channels");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_StaticFiles_PreviewImgId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_PreviewImgId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Channels_MainImgId",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "PreviewImgId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "BannerImgId",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "MainImgId",
                table: "Channels");

            migrationBuilder.AddColumn<int>(
                name: "BannerChannelId",
                table: "StaticFiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MainChannelId",
                table: "StaticFiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VideoId",
                table: "StaticFiles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaticFiles_BannerChannelId",
                table: "StaticFiles",
                column: "BannerChannelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaticFiles_MainChannelId",
                table: "StaticFiles",
                column: "MainChannelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaticFiles_VideoId",
                table: "StaticFiles",
                column: "VideoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StaticFiles_Channels_BannerChannelId",
                table: "StaticFiles",
                column: "BannerChannelId",
                principalTable: "Channels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StaticFiles_Channels_MainChannelId",
                table: "StaticFiles",
                column: "MainChannelId",
                principalTable: "Channels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StaticFiles_Videos_VideoId",
                table: "StaticFiles",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id");
        }
    }
}
