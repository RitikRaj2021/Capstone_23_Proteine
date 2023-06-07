using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone_23_Proteine.Data.Migrations
{
    /// <inheritdoc />
    public partial class harrysetgoals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetGoals_AspNetUsers_UserId",
                table: "SetGoals");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SetGoals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SetGoals_AspNetUsers_UserId",
                table: "SetGoals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetGoals_AspNetUsers_UserId",
                table: "SetGoals");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SetGoals",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_SetGoals_AspNetUsers_UserId",
                table: "SetGoals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
