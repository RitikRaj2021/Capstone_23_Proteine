using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone_23_Proteine.Data.Migrations
{
    /// <inheritdoc />
    public partial class foodintakeupdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MealType",
                table: "FoodIntake",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealType",
                table: "FoodIntake");
        }
    }
}
