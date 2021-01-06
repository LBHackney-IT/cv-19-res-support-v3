using Microsoft.EntityFrameworkCore.Migrations;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class DeduplicateResidents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //TODO:
            // 1. Find duplicate residents in the residents table (how do we identify duplicates)
            // Match on first_name and last_name so far.....
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
