using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class AddCallsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "help_request_calls",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    help_request_id = table.Column<int>(nullable: false),
                    call_type = table.Column<string>(type: "character varying", nullable: true),
                    call_outcome = table.Column<string>(type: "character varying", nullable: true),
                    call_date_time = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_help_request_calls", x => x.id);
                    table.ForeignKey(
                        name: "FK_help_request_calls_i_need_help_resident_support_v3_help_req~",
                        column: x => x.help_request_id,
                        principalTable: "i_need_help_resident_support_v3",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_help_request_calls_help_request_id",
                table: "help_request_calls",
                column: "help_request_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "help_request_calls");
        }
    }
}
