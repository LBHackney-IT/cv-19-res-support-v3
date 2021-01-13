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
                                nhs_number = table.Column<string>(type: "character varying", nullable: true),
                                temp_help_request_id = table.Column<string>(type: "character varying", nullable: true)
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
INSERT INTO residents (
first_name,
last_name,
dob_day,
dob_month,
dob_year,
contact_mobile_number,
contact_telephone_number,
email_address,
address_first_line,
address_second_line,
address_third_line,
postcode,
uprn,
ward,
is_pharmacist_able_to_deliver,
name_address_pharmacist,
gp_surgery_details,
number_of_children_under_18,
consent_to_share,
record_status,
nhs_number,
temp_help_request_id
)
SELECT
    trim(first_name) as first_name,
    trim(last_name) as last_name,
    trim(dob_day) as dob_day,
    trim(dob_month) as dob_month,
    trim(dob_year) as dob_year,
    string_agg(contact_mobile_number, ' / ') as contact_mobile_number,
    string_agg(contact_telephone_number, ' / ') as contact_telephone_number,
    string_agg(email_address, ' / ') as email_address,
    string_agg(address_first_line, ' / ') as address_first_line,
    string_agg(address_second_line, ' / ') as address_second_line,
    string_agg(address_third_line, ' / ') as address_third_line,
    string_agg(postcode, ' / ') as postcode,
    string_agg(uprn, ' / ') as uprn,
    string_agg(ward, ' / ') as ward,
    bool_or(is_pharmacist_able_to_deliver) as is_pharmacist_able_to_deliver,
    string_agg(name_address_pharmacist, ' / ') as name_address_pharmacist,
    string_agg(gp_surgery_details, ' / ') as gp_surgery_details,
    string_agg(number_of_children_under_18, ' / ') as number_of_children_under_18,
    bool_or(consent_to_share) as consent_to_share,
    string_agg(record_status, ' / ') as record_status,
    string_agg(nhs_number, ' / ') as nhs_number,
    string_agg(cast(temp_help_request_id as varchar), ' / ') AS temp_help_request_id
FROM (
     SELECT postcode, uprn, ward, address_first_line, address_second_line, address_third_line, is_pharmacist_able_to_deliver, name_address_pharmacist, first_name, last_name, dob_month, dob_year, dob_day, contact_telephone_number, contact_mobile_number, email_address, gp_surgery_details, number_of_children_under_18, consent_to_share, record_status, nhs_number, id AS temp_help_request_id
FROM i_need_help_resident_support_v3
         ) as new_res
GROUP BY
    trim(first_name),
    trim(last_name),
    trim(dob_day),
    trim(dob_month),
    trim(dob_year);
");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "residents");
        }
    }
}
