using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YouTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewUserAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Avatars_AvatarId",
                table: "UserInfos");

            migrationBuilder.DropTable(
                name: "Avatars");

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
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Avatars",
                columns: new[] { "Id", "Path" },
                values: new object[,]
                {
                    { 1, "http://localhost:5041/static/avatars/users-1.svg" },
                    { 2, "http://localhost:5041/static/avatars/users-2.svg" },
                    { 3, "http://localhost:5041/static/avatars/users-3.svg" },
                    { 4, "http://localhost:5041/static/avatars/users-4.svg" },
                    { 5, "http://localhost:5041/static/avatars/users-5.svg" },
                    { 6, "http://localhost:5041/static/avatars/users-6.svg" },
                    { 7, "http://localhost:5041/static/avatars/users-7.svg" },
                    { 8, "http://localhost:5041/static/avatars/users-8.svg" },
                    { 9, "http://localhost:5041/static/avatars/users-9.svg" },
                    { 10, "http://localhost:5041/static/avatars/users-10.svg" },
                    { 11, "http://localhost:5041/static/avatars/users-11.svg" },
                    { 12, "http://localhost:5041/static/avatars/users-12.svg" },
                    { 13, "http://localhost:5041/static/avatars/users-13.svg" },
                    { 14, "http://localhost:5041/static/avatars/users-14.svg" },
                    { 15, "http://localhost:5041/static/avatars/users-15.svg" },
                    { 16, "http://localhost:5041/static/avatars/users-16.svg" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Avatars_AvatarId",
                table: "UserInfos",
                column: "AvatarId",
                principalTable: "Avatars",
                principalColumn: "Id");
        }
    }
}
