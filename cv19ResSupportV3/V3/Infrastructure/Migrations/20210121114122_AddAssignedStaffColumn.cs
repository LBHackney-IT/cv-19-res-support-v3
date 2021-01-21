using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class AddAssignedStaffColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "assigned_staff",
                table: "help_requests",
                type: "character varying",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assigned_staff",
                table: "help_requests");
        }
    }
}
