using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskydo.Migrations
{
    /// <inheritdoc />
    public partial class SetId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tasks",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Tasks",
                newName: "Id");
        }
    }
}
