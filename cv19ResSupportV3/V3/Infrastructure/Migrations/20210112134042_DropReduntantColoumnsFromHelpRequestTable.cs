using Microsoft.EntityFrameworkCore.Migrations;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class DropReduntantColoumnsFromHelpRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "first_name", table: "help_requests" );
            migrationBuilder.DropColumn(name: "last_name", table: "help_requests" );
            migrationBuilder.DropColumn(name: "dob_day", table: "help_requests" );
            migrationBuilder.DropColumn(name: "dob_month", table: "help_requests" );
            migrationBuilder.DropColumn(name: "dob_year", table: "help_requests" );
            migrationBuilder.DropColumn(name: "contact_mobile_number", table: "help_requests" );
            migrationBuilder.DropColumn(name: "contact_telephone_number", table: "help_requests" );
            migrationBuilder.DropColumn(name: "email_address", table: "help_requests" );
            migrationBuilder.DropColumn(name: "address_first_line", table: "help_requests" );
            migrationBuilder.DropColumn(name: "address_second_line", table: "help_requests" );
            migrationBuilder.DropColumn(name: "address_third_line", table: "help_requests" );
            migrationBuilder.DropColumn(name: "postcode", table: "help_requests" );
            migrationBuilder.DropColumn(name: "uprn", table: "help_requests" );
            migrationBuilder.DropColumn(name: "ward", table: "help_requests" );
            migrationBuilder.DropColumn(name: "is_pharmacist_able_to_deliver", table: "help_requests" );
            migrationBuilder.DropColumn(name: "name_address_pharmacist", table: "help_requests" );
            migrationBuilder.DropColumn(name: "gp_surgery_details", table: "help_requests" );
            migrationBuilder.DropColumn(name: "number_of_children_under_18", table: "help_requests" );
            migrationBuilder.DropColumn(name: "consent_to_share", table: "help_requests" );
            migrationBuilder.DropColumn(name: "record_status", table: "help_requests" );
            migrationBuilder.DropColumn(name: "nhs_number", table: "help_requests" );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(name: "first_name", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "last_name", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "dob_day", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "dob_month", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "dob_year", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "contact_mobile_number", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "contact_telephone_number", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "email_address", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "address_first_line", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "address_second_line", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "address_third_line", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "postcode", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "uprn", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "ward", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<bool>(name: "is_pharmacist_able_to_deliver", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "name_address_pharmacist", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "gp_surgery_details", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "number_of_children_under_18", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<bool>(name: "consent_to_share", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "record_status", table: "help_requests", nullable: true);
            migrationBuilder.AddColumn<string>(name: "nhs_number", table: "help_requests", nullable: true);
        }
    }
}
