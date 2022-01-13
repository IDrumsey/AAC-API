using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalAdoptionCenter.Migrations
{
    public partial class UserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileName_Animal_AnimalId1",
                table: "FileName");

            migrationBuilder.DropIndex(
                name: "IX_FileName_AnimalId1",
                table: "FileName");

            migrationBuilder.DropColumn(
                name: "AnimalId1",
                table: "FileName");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "password", "role", "username" },
                values: new object[] { 1, "47QqA1aaTNATDHA8gIheCqqt8RDFIalB28ZkFDpbHK0=", "Employee", "testuser1" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "password", "role", "username" },
                values: new object[] { 2, "AIxlio2QDVosgpFXAJtZfPyhINBkNN+8oz6rj9O+3xQ=", "Manager", "testuser2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.AddColumn<int>(
                name: "AnimalId1",
                table: "FileName",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileName_AnimalId1",
                table: "FileName",
                column: "AnimalId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FileName_Animal_AnimalId1",
                table: "FileName",
                column: "AnimalId1",
                principalTable: "Animal",
                principalColumn: "AnimalId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
