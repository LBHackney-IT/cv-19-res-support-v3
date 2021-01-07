using Microsoft.EntityFrameworkCore.Migrations;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class DeduplicateResidents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //TODO:
            // 1. Find duplicate residents in the residents table (how do we identify duplicates)
            // Match on first_name, last_name, dob_day and dob_year (dob_month has some issues)
            // Distinct Records:
            // select DISTINCT first_name, last_name, dob_day, dob_year from "public".i_need_help_resident_support_v3 order by last_name, first_name;
            // Duplicate Matches:
            // select first_name, last_name, dob_day, dob_year, count(*)
            // from "public".i_need_help_resident_support_v3
            // group by first_name, last_name, dob_day, dob_year
            // HAVING count(*) > 1;
            // 2. For every duplicate resident find the master record
            // 3. Assign the help request for that resident to the master record
            // End result: Only records identified as master records will have associated help requests
            // End result: Resident records without help requests should be duplicate residents
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
