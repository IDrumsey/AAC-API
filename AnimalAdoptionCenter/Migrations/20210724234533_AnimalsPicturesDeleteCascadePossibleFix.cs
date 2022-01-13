using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalAdoptionCenter.Migrations
{
    public partial class AnimalsPicturesDeleteCascadePossibleFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
