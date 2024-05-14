using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewUserAvatar2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_StaticFiles_AvatarId",
                table: "UserInfos");

            migrationBuilder.AlterColumn<int>(
                name: "AvatarId",
                table: "UserInfos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_StaticFiles_AvatarId",
                table: "UserInfos",
                column: "AvatarId",
                principalTable: "StaticFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_StaticFiles_AvatarId",
                table: "UserInfos");

            migrationBuilder.AlterColumn<int>(
                name: "AvatarId",
                table: "UserInfos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_StaticFiles_AvatarId",
                table: "UserInfos",
                column: "AvatarId",
                principalTable: "StaticFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
