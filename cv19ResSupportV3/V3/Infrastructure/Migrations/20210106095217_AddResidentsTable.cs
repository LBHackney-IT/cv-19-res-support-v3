using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class AddResidentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                            name: "residents",
                            columns: table => new
                            {
                                id = table.Column<int>(nullable: false)
                                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                first_name = table.Column<string>(type: "character varying", nullable: true),
                                last_name = table.Column<string>(type: "character varying", nullable: true),
                                dob_day = table.Column<string>(type: "character varying", nullable: true),
                                dob_month = table.Column<string>(type: "character varying", nullable: true),
                                dob_year = table.Column<string>(type: "character varying", nullable: true),
                                contact_mobile_number = table.Column<string>(type: "character varying", nullable: true),
                                contact_telephone_number = table.Column<string>(type: "character varying", nullable: true),
                                email_address = table.Column<string>(type: "character varying", nullable: true),
                                address_first_line = table.Column<string>(type: "character varying", nullable: true),
                                address_second_line = table.Column<string>(type: "character varying", nullable: true),
                                address_third_line = table.Column<string>(type: "character varying", nullable: true),
                                postcode = table.Column<string>(type: "character varying", nullable: true),
                                uprn = table.Column<string>(type: "character varying", nullable: true),
                                ward = table.Column<string>(type: "character varying", nullable: true),
                                is_pharmacist_able_to_deliver = table.Column<bool>(type: "bool", nullable: true),
                                name_address_pharmacist = table.Column<string>(type: "character varying", nullable: true),
                                gp_surgery_details = table.Column<string>(type: "character varying", nullable: true),
                                number_of_children_under_18 = table.Column<string>(type: "character varying", nullable: true),
                                consent_to_share = table.Column<bool>(type: "bool", nullable: true),
                                record_status = table.Column<string>(type: "character varying", nullable: true),
                                case_notes = table.Column<string>(type: "character varying", nullable: true),
                                nhs_number = table.Column<string>(type: "character varying", nullable: true)
                            },
                            constraints: table =>
                            {
                                table.PrimaryKey("PK_residents", x => x.id);
                            });

            migrationBuilder.CreateIndex(
                name: "IX_residents_id",
                table: "residents",
                column: "id");

            migrationBuilder.Sql(
                @"
INSERT INTO residents (postcode, uprn, ward, address_first_line, address_second_line, address_third_line, is_pharmacist_able_to_deliver, name_address_pharmacist, first_name, last_name, dob_month, dob_year, dob_day, contact_telephone_number, contact_mobile_number, email_address, gp_surgery_details, number_of_children_under_18, consent_to_share, record_status, case_notes, nhs_number)
SELECT postcode, uprn, ward, address_first_line, address_second_line, address_third_line, is_pharmacist_able_to_deliver, name_address_pharmacist, first_name, last_name, dob_month, dob_year, dob_day, contact_telephone_number, contact_mobile_number, email_address, gp_surgery_details, number_of_children_under_18, consent_to_share, record_status, case_notes, nhs_number
FROM i_need_help_resident_support_v3;
");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "residents");
        }
    }
}
