using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStaticFileProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_Channels_ChannelId",
                table: "StaticFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_Videos_VideoId",
                table: "StaticFiles");

            migrationBuilder.AlterColumn<int>(
                name: "VideoId",
                table: "StaticFiles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ChannelId",
                table: "StaticFiles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_StaticFiles_Channels_ChannelId",
                table: "StaticFiles",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StaticFiles_Videos_VideoId",
                table: "StaticFiles",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_Channels_ChannelId",
                table: "StaticFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_Videos_VideoId",
                table: "StaticFiles");

            migrationBuilder.AlterColumn<int>(
                name: "VideoId",
                table: "StaticFiles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChannelId",
                table: "StaticFiles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StaticFiles_Channels_ChannelId",
                table: "StaticFiles",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaticFiles_Videos_VideoId",
                table: "StaticFiles",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
