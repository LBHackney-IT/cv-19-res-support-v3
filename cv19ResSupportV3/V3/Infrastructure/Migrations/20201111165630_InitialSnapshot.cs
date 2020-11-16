using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class InitialSnapshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
//            migrationBuilder.CreateTable(
//                name: "i_need_help_resident_support_v3",
//                columns: table => new
//                {
//                    id = table.Column<int>(nullable: false)
//                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                    is_on_behalf = table.Column<bool>(type: "bool", nullable: true),
//                    consent_to_complete_on_behalf = table.Column<bool>(type: "bool", nullable: true),
//                    on_behalf_first_name = table.Column<string>(type: "character varying", nullable: true),
//                    on_behalf_last_name = table.Column<string>(type: "character varying", nullable: true),
//                    on_behalf_email_address = table.Column<string>(type: "character varying", nullable: true),
//                    on_behalf_contact_number = table.Column<string>(type: "character varying", nullable: true),
//                    relationship_with_resident = table.Column<string>(type: "character varying", nullable: true),
//                    postcode = table.Column<string>(type: "character varying", nullable: true),
//                    uprn = table.Column<string>(type: "character varying", nullable: true),
//                    ward = table.Column<string>(type: "character varying", nullable: true),
//                    address_first_line = table.Column<string>(type: "character varying", nullable: true),
//                    address_second_line = table.Column<string>(type: "character varying", nullable: true),
//                    address_third_line = table.Column<string>(type: "character varying", nullable: true),
//                    getting_in_touch_reason = table.Column<string>(type: "character varying", nullable: true),
//                    help_with_accessing_food = table.Column<bool>(type: "bool", nullable: true),
//                    help_with_accessing_medicine = table.Column<bool>(type: "bool", nullable: true),
//                    help_with_accessing_other_essentials = table.Column<bool>(type: "bool", nullable: true),
//                    help_with_debt_and_money = table.Column<bool>(type: "bool", nullable: true),
//                    help_with_health = table.Column<bool>(type: "bool", nullable: true),
//                    help_with_mental_health = table.Column<bool>(type: "bool", nullable: true),
//                    help_with_accessing_internet = table.Column<bool>(type: "bool", nullable: true),
//                    help_with_something_else = table.Column<bool>(type: "bool", nullable: true),
//                    help_with_housing = table.Column<bool>(type: "bool", nullable: true),
//                    help_with_jobs_or_training = table.Column<bool>(type: "bool", nullable: true),
//                    help_with_children_and_schools = table.Column<bool>(type: "bool", nullable: true),
//                    help_with_disabilities = table.Column<bool>(type: "bool", nullable: true),
//                    medicine_delivery_help_needed = table.Column<bool>(type: "bool", nullable: true),
//                    is_pharmacist_able_to_deliver = table.Column<bool>(type: "bool", nullable: true),
//                    when_is_medicines_delivered = table.Column<string>(type: "character varying", nullable: true),
//                    name_address_pharmacist = table.Column<string>(type: "character varying", nullable: true),
//                    urgent_essentials = table.Column<string>(type: "character varying", nullable: true),
//                    urgent_essentials_anything_else = table.Column<string>(type: "character varying", nullable: true),
//                    current_support = table.Column<string>(type: "character varying", nullable: true),
//                    current_support_feedback = table.Column<string>(type: "character varying", nullable: true),
//                    first_name = table.Column<string>(type: "character varying", nullable: true),
//                    last_name = table.Column<string>(type: "character varying", nullable: true),
//                    dob_month = table.Column<string>(type: "character varying", nullable: true),
//                    dob_year = table.Column<string>(type: "character varying", nullable: true),
//                    dob_day = table.Column<string>(type: "character varying", nullable: true),
//                    contact_telephone_number = table.Column<string>(type: "character varying", nullable: true),
//                    contact_mobile_number = table.Column<string>(type: "character varying", nullable: true),
//                    email_address = table.Column<string>(type: "character varying", nullable: true),
//                    gp_surgery_details = table.Column<string>(type: "character varying", nullable: true),
//                    number_of_children_under_18 = table.Column<string>(type: "character varying", nullable: true),
//                    consent_to_share = table.Column<bool>(type: "bool", nullable: true),
//                    date_time_recorded = table.Column<DateTime>(nullable: true),
//                    record_status = table.Column<string>(type: "character varying", nullable: true),
//                    callback_required = table.Column<bool>(type: "bool", nullable: true),
//                    initial_callback_completed = table.Column<bool>(type: "bool", nullable: true),
//                    case_notes = table.Column<string>(type: "character varying", nullable: true),
//                    advice_notes = table.Column<string>(type: "character varying", nullable: true),
//                    help_needed = table.Column<string>(type: "character varying", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_i_need_help_resident_support_v3", x => x.id);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "inh_lookups",
//                columns: table => new
//                {
//                    id = table.Column<int>(nullable: false)
//                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                    lookup_group = table.Column<string>(type: "character varying", nullable: true),
//                    lookup = table.Column<string>(type: "character varying", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_inh_lookups", x => x.id);
//                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
//            migrationBuilder.DropTable(
//                name: "i_need_help_resident_support_v3");
//
//            migrationBuilder.DropTable(
//                name: "inh_lookups");
        }
    }
}
