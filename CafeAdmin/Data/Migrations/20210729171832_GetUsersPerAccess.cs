using Microsoft.EntityFrameworkCore.Migrations;

namespace CafeAdmin.Data.Migrations
{
    public partial class GetUsersPerAccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //var createProcSql = @"CREATE OR ALTER PROC GetUsersPerAccess(@accessLevel int) AS SELECT U.Id,U.Name FROM UserAccessLevel IN";
            //migrationBuilder.Sql(createProcSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //var dropProcSql = "DROP PROC GetUsersPerAccess";
            //migrationBuilder.Sql(dropProcSql);

        }
    }
}
