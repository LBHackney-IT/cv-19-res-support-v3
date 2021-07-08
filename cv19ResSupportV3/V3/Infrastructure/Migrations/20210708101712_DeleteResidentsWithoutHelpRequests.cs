using Microsoft.EntityFrameworkCore.Migrations;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class DeleteResidentsWithoutHelpRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
              @"DELETE FROM residents
                WHERE id NOT IN (select resident_id FROM help_requests)
                AND id NOT IN (select resident_id FROM resident_case_notes);"
            );
        }
    }
}
