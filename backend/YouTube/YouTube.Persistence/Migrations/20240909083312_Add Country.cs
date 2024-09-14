using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTube.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Videos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "UserInfos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Channels",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Channels");
        }
    }
}
