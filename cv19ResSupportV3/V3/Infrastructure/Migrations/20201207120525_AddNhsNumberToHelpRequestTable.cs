using Microsoft.EntityFrameworkCore.Migrations;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class AddNhsNumberToHelpRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nhs_number",
                table: "i_need_help_resident_support_v3",
                type: "character varying",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nhs_number",
                table: "i_need_help_resident_support_v3");
        }
    }
}
