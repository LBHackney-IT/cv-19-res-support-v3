using Microsoft.EntityFrameworkCore.Migrations;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class AddHelpNeededFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "help_with_accessing_supermarket_food",
                table: "i_need_help_resident_support_v3",
                type: "bool",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "help_with_completing_nss_form",
                table: "i_need_help_resident_support_v3",
                type: "bool",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "help_with_no_needs_identified",
                table: "i_need_help_resident_support_v3",
                type: "bool",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "help_with_shielding_guidance",
                table: "i_need_help_resident_support_v3",
                type: "bool",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "help_with_accessing_supermarket_food",
                table: "i_need_help_resident_support_v3");

            migrationBuilder.DropColumn(
                name: "help_with_completing_nss_form",
                table: "i_need_help_resident_support_v3");

            migrationBuilder.DropColumn(
                name: "help_with_no_needs_identified",
                table: "i_need_help_resident_support_v3");

            migrationBuilder.DropColumn(
                name: "help_with_shielding_guidance",
                table: "i_need_help_resident_support_v3");
        }
    }
}
