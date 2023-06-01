using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone_23_Proteine.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetGoals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SetGoals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SetCalories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SetProtein = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SetFat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetGoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetGoals_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SetGoals_UserId",
                table: "SetGoals",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SetGoals");
        }
    }
}
