using Microsoft.EntityFrameworkCore.Migrations;

namespace CafeAdmin.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DS = table.Column<string>(type: "varchar(250)", nullable: false),
                    Summ = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false, comment: "Це тестова сумма"),
                    TestLenght = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTables", x => new { x.Name, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccesLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AccessLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccesLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccesLevels_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "Admin", "12345" },
                    { 2, "Ivan Gardin", "12345" },
                    { 3, "Petro Stepanov", "12345" },
                    { 4, "Mirko Shuher", "12345" },
                    { 5, "Peter Hugert", "12345" },
                    { 6, "Ruzhena Stefanic", "12345" }
                });

            migrationBuilder.InsertData(
                table: "UserAccesLevels",
                columns: new[] { "Id", "AccessLevel", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 2, 3 },
                    { 4, 2, 4 },
                    { 5, 3, 5 },
                    { 6, 3, 6 },
                    { 7, 2, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccesLevels_UserId",
                table: "UserAccesLevels",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientTables");

            migrationBuilder.DropTable(
                name: "UserAccesLevels");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
