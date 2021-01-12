using Microsoft.EntityFrameworkCore.Migrations;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    public partial class AddResidentIdToHelpRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "resident_id",
                table: "help_requests",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_help_request_resident_id",
                "help_requests",
                "resident_id",
                "residents",
                default,
                default,
                "id"
                );

            migrationBuilder.Sql(
                @"
                    UPDATE help_requests
                    SET resident_id=cast(subquery.resident_id as int)
                    FROM (
                             SELECT residents.id AS resident_id, residents.temp_help_request_id
                             FROM residents
                                      JOIN help_requests ON cast(residents.temp_help_request_id as int) = help_requests.id
                             WHERE temp_help_request_id NOT LIKE '% / %'
                    ) AS subquery
                    WHERE help_requests.id=cast(subquery.temp_help_request_id as int);
                "
            );

            migrationBuilder.Sql(
                @"
                    UPDATE help_requests
                    SET resident_id=cast(subquery.resident_id as int)
                    FROM (
                             SELECT residents.id AS resident_id, residents.temp_help_request_id
                             FROM residents
                                       JOIN help_requests ON cast(help_requests.id AS varchar) = ANY (string_to_array(residents.temp_help_request_id, ' / '))
                             WHERE temp_help_request_id LIKE '% / %'
                    ) AS subquery
                    WHERE cast(help_requests.id AS varchar) =  ANY (string_to_array(subquery.temp_help_request_id, ' / '));
                "
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "resident_id",
                table: "help_requests");
        }
    }
}
