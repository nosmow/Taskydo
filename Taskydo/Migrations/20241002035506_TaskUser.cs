using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskydo.Migrations
{
    /// <inheritdoc />
    public partial class TaskUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userCreationId",
                table: "tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tasks_userCreationId",
                table: "tasks",
                column: "userCreationId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_AspNetUsers_userCreationId",
                table: "tasks",
                column: "userCreationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_AspNetUsers_userCreationId",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_userCreationId",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "userCreationId",
                table: "tasks");
        }
    }
}
