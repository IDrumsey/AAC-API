using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalAdoptionCenter.Migrations
{
    public partial class AnimalPicturesDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileName_Animal_AnimalId",
                table: "FileName");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalId",
                table: "FileName",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileName_Animal_AnimalId",
                table: "FileName",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "AnimalId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileName_Animal_AnimalId",
                table: "FileName");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalId",
                table: "FileName",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_FileName_Animal_AnimalId",
                table: "FileName",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "AnimalId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
