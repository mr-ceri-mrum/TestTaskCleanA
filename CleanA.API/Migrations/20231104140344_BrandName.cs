using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanA.API.Migrations
{
    /// <inheritdoc />
    public partial class BrandName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameCar",
                table: "Cars",
                newName: "ModelName");

            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "Cars",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "ModelName",
                table: "Cars",
                newName: "NameCar");
        }
    }
}
