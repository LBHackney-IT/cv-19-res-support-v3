using Microsoft.EntityFrameworkCore.Migrations;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class ChangeCallerToCallHandler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "caller",
                table: "help_request_calls",
                newName: "call_handler");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "call_handler",
                table: "help_request_calls",
                newName: "caller");
        }
    }
}
