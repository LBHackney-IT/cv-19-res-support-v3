using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class CreateResidentCaseNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                            name: "resident_case_notes",
                            columns: table => new
                            {
                                id = table.Column<int>(nullable: false)
                                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                resident_id = table.Column<int>(nullable: false),
                                case_notes = table.Column<string>(type: "character varying", nullable: true),
                                help_request_id = table.Column<int>(nullable: true)
                            },
                            constraints: table =>
                            {
                                table.PrimaryKey("PK_resident_case_notes", x => x.id);
                                table.ForeignKey(
                                    name: "FK_resident_case_notes_residents",
                                    column: x => x.resident_id,
                                    principalTable: "residents",
                                    principalColumn: "id",
                                    onDelete: ReferentialAction.Cascade);

                                table.ForeignKey(
                                    name: "FK_resident_case_notes_i_need_help_resident_support_v3",
                                    column: x => x.help_request_id,
                                    principalTable: "i_need_help_resident_support_v3",
                                    principalColumn: "id");
                            });

            migrationBuilder.CreateIndex(
                name: "IX_resident_case_notes_id",
                table: "resident_case_notes",
                column: "id"
            );

            migrationBuilder.Sql(
                @"
                INSERT INTO resident_case_notes (resident_id, help_request_id, case_notes)
                SELECT residents.id AS resident_id, i_need_help_resident_support_v3.id AS help_request_id, case_notes
                FROM residents
                         JOIN i_need_help_resident_support_v3 ON cast(residents.temp_help_request_id as int) = i_need_help_resident_support_v3.id
                WHERE temp_help_request_id NOT LIKE '% / %';
                "
             );

            migrationBuilder.Sql(
                @"
                INSERT INTO resident_case_notes (resident_id, help_request_id, case_notes)
                SELECT residents.id AS resident_id, i_need_help_resident_support_v3.id AS help_request_id, case_notes
                FROM residents
                JOIN i_need_help_resident_support_v3 ON cast(i_need_help_resident_support_v3.id AS varchar) = ANY (string_to_array(residents.temp_help_request_id, ' / '))
                WHERE temp_help_request_id LIKE '% / %';
                "
             );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "resident_case_notes");

        }
    }
}
