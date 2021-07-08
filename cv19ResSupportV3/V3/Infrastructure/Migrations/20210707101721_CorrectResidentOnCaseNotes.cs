using Microsoft.EntityFrameworkCore.Migrations;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class CorrectResidentOnCaseNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Due to duplicate residents being created, we have multiple case note records
            // where the help_request_id is associated with a resident_id other than the one
            // in the case notes record.

            // This will set the resident_id in case notes to the resident_id associated with
            // the help_request_id in that case note record.
            migrationBuilder.Sql(
              @"UPDATE  resident_case_notes r
                SET     resident_id = h.resident_id
                FROM    resident_case_notes c JOIN help_requests h
                ON      c.help_request_id = h.id
                WHERE   c.help_request_id = r.help_request_id
                AND     r.resident_id <> h.resident_id;"
            );
        }
    }
}
