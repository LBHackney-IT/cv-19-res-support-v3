using Microsoft.EntityFrameworkCore.Migrations;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class RenameHelpRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable (
                name: "i_need_help_resident_support_v3",
                newName: "help_requests"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable (
                name: "help_requests",
                newName: "i_need_help_resident_support_v3"
            );
        }
    }
}
