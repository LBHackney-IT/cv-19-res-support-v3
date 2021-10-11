using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class AddCallHandlersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "call_handler_id",
                table: "help_requests",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "call_handlers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    email = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_call_handlers", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_help_requests_call_handler_id",
                table: "help_requests",
                column: "call_handler_id");

            migrationBuilder.AddForeignKey(
                name: "FK_help_requests_call_handlers_call_handler_id",
                table: "help_requests",
                column: "call_handler_id",
                principalTable: "call_handlers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_help_requests_call_handlers_call_handler_id",
                table: "help_requests");

            migrationBuilder.DropTable(
                name: "call_handlers");

            migrationBuilder.DropIndex(
                name: "IX_help_requests_call_handler_id",
                table: "help_requests");

            migrationBuilder.DropColumn(
                name: "call_handler_id",
                table: "help_requests");
        }
    }
}

