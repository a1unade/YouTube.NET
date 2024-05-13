using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateStaticProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_Channels_ChannelId",
                table: "StaticFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_Channels_ChannelId1",
                table: "StaticFiles");

            migrationBuilder.RenameColumn(
                name: "ChannelId1",
                table: "StaticFiles",
                newName: "MainChannelId");

            migrationBuilder.RenameColumn(
                name: "ChannelId",
                table: "StaticFiles",
                newName: "BannerChannelId");

            migrationBuilder.RenameIndex(
                name: "IX_StaticFiles_ChannelId1",
                table: "StaticFiles",
                newName: "IX_StaticFiles_MainChannelId");

            migrationBuilder.RenameIndex(
                name: "IX_StaticFiles_ChannelId",
                table: "StaticFiles",
                newName: "IX_StaticFiles_BannerChannelId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_Channels_BannerChannelId",
                table: "StaticFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_Channels_MainChannelId",
                table: "StaticFiles");

            migrationBuilder.RenameColumn(
                name: "MainChannelId",
                table: "StaticFiles",
                newName: "ChannelId1");

            migrationBuilder.RenameColumn(
                name: "BannerChannelId",
                table: "StaticFiles",
                newName: "ChannelId");

            migrationBuilder.RenameIndex(
                name: "IX_StaticFiles_MainChannelId",
                table: "StaticFiles",
                newName: "IX_StaticFiles_ChannelId1");

            migrationBuilder.RenameIndex(
                name: "IX_StaticFiles_BannerChannelId",
                table: "StaticFiles",
                newName: "IX_StaticFiles_ChannelId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaticFiles_Channels_ChannelId",
                table: "StaticFiles",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StaticFiles_Channels_ChannelId1",
                table: "StaticFiles",
                column: "ChannelId1",
                principalTable: "Channels",
                principalColumn: "Id");
        }
    }
}
