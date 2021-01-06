using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class AddHelpRequestsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                            name: "help_requests",
                            columns: table => new
                            {
                                id = table.Column<int>(nullable: false)
                                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                resident_id = table.Column<int>(nullable: false),
                                is_on_behalf = table.Column<bool>(type: "bool", nullable: true),
                                consent_to_complete_on_behalf = table.Column<bool>(type: "bool", nullable: true),
                                on_behalf_first_name = table.Column<string>(type: "character varying", nullable: true),
                                on_behalf_last_name = table.Column<string>(type: "character varying", nullable: true),
                                on_behalf_email_address = table.Column<string>(type: "character varying", nullable: true),
                                on_behalf_contact_number = table.Column<string>(type: "character varying", nullable: true),
                                relationship_with_resident = table.Column<string>(type: "character varying", nullable: true),
                                getting_in_touch_reason = table.Column<string>(type: "character varying", nullable: true),
                                help_with_accessing_food = table.Column<bool>(type: "bool", nullable: true),
                                help_with_accessing_medicine = table.Column<bool>(type: "bool", nullable: true),
                                help_with_accessing_other_essentials = table.Column<bool>(type: "bool", nullable: true),
                                help_with_debt_and_money = table.Column<bool>(type: "bool", nullable: true),
                                help_with_health = table.Column<bool>(type: "bool", nullable: true),
                                help_with_mental_health = table.Column<bool>(type: "bool", nullable: true),
                                help_with_accessing_internet = table.Column<bool>(type: "bool", nullable: true),
                                help_with_something_else = table.Column<bool>(type: "bool", nullable: true),
                                help_with_housing = table.Column<bool>(type: "bool", nullable: true),
                                help_with_jobs_or_training = table.Column<bool>(type: "bool", nullable: true),
                                help_with_children_and_schools = table.Column<bool>(type: "bool", nullable: true),
                                help_with_disabilities = table.Column<bool>(type: "bool", nullable: true),
                                medicine_delivery_help_needed = table.Column<bool>(type: "bool", nullable: true),
                                when_is_medicines_delivered = table.Column<string>(type: "character varying", nullable: true),
                                urgent_essentials = table.Column<string>(type: "character varying", nullable: true),
                                urgent_essentials_anything_else = table.Column<string>(type: "character varying", nullable: true),
                                current_support = table.Column<string>(type: "character varying", nullable: true),
                                current_support_feedback = table.Column<string>(type: "character varying", nullable: true),
                                date_time_recorded = table.Column<DateTime>(nullable: true),
                                record_status = table.Column<string>(type: "character varying", nullable: true),
                                callback_required = table.Column<bool>(type: "bool", nullable: true),
                                initial_callback_completed = table.Column<bool>(type: "bool", nullable: true),
                                advice_notes = table.Column<string>(type: "character varying", nullable: true),
                                help_needed = table.Column<string>(type: "character varying", nullable: true)

                            },
                            constraints: table =>
                            {
                                table.PrimaryKey("PK_help_requests", x => x.id);
                                table.ForeignKey(
                                    name: "FK_help_requests_residents",
                                    column: x => x.resident_id,
                                    principalTable: "residents",
                                    principalColumn: "id",
                                    onDelete: ReferentialAction.Cascade);
                            });

            migrationBuilder.CreateIndex(
                name: "IX_help_requests_id",
                table: "residents",
                column: "id");

            migrationBuilder.Sql(
                @"
                    INSERT INTO help_requests (id, resident_id, is_on_behalf, consent_to_complete_on_behalf, on_behalf_first_name, on_behalf_last_name, on_behalf_email_address, on_behalf_contact_number, relationship_with_resident, getting_in_touch_reason, help_with_accessing_food, help_with_accessing_medicine, help_with_accessing_other_essentials, help_with_debt_and_money, help_with_health, help_with_mental_health, help_with_accessing_internet, help_with_something_else, help_with_housing, help_with_jobs_or_training, help_with_children_and_schools, help_with_disabilities, medicine_delivery_help_needed, when_is_medicines_delivered, urgent_essentials, urgent_essentials_anything_else, current_support, current_support_feedback, date_time_recorded, record_status, callback_required, initial_callback_completed, advice_notes, help_needed)
                    SELECT i_need_help_resident_support_v3.id, residents.id AS resident_id, is_on_behalf, consent_to_complete_on_behalf, on_behalf_first_name, on_behalf_last_name, on_behalf_email_address, on_behalf_contact_number, relationship_with_resident, getting_in_touch_reason, help_with_accessing_food, help_with_accessing_medicine, help_with_accessing_other_essentials, help_with_debt_and_money, help_with_health, help_with_mental_health, help_with_accessing_internet, help_with_something_else, help_with_housing, help_with_jobs_or_training, help_with_children_and_schools, help_with_disabilities, medicine_delivery_help_needed, when_is_medicines_delivered, urgent_essentials, urgent_essentials_anything_else, current_support, current_support_feedback, date_time_recorded, i_need_help_resident_support_v3.record_status, callback_required, initial_callback_completed, advice_notes, help_needed
                    FROM i_need_help_resident_support_v3
                    JOIN residents ON residents.temp_help_request_id = i_need_help_resident_support_v3.id;
                 "
             );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "help_requests");

        }
    }
}
