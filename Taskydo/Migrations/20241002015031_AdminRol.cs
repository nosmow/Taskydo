using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskydo.Migrations
{
    /// <inheritdoc />
    public partial class AdminRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS(SELECT Id FROM AspNetRoles WHERE Id = '8728563d-b702-4860-9620-55d7c0abfc3b')
                BEGIN
	                INSERT AspNetRoles (Id, [Name], [NormalizedName])
	                VALUES ('8728563d-b702-4860-9620-55d7c0abfc3b', 'admin', 'ADMIN')
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE AspNetRoles WHERE Id = '8728563d-b702-4860-9620-55d7c0abfc3b'");
        }
    }
}
