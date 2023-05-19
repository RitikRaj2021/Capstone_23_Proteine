using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone_23_Proteine.Data.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FoodIntake",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AboutMe",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIntake_UserId",
                table: "FoodIntake",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AboutMe_UserId",
                table: "AboutMe",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutMe_AspNetUsers_UserId",
                table: "AboutMe",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodIntake_AspNetUsers_UserId",
                table: "FoodIntake",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutMe_AspNetUsers_UserId",
                table: "AboutMe");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodIntake_AspNetUsers_UserId",
                table: "FoodIntake");

            migrationBuilder.DropIndex(
                name: "IX_FoodIntake_UserId",
                table: "FoodIntake");

            migrationBuilder.DropIndex(
                name: "IX_AboutMe_UserId",
                table: "AboutMe");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FoodIntake");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AboutMe");
        }
    }
}
