using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalAdoptionCenter.Migrations
{
    public partial class AnimalDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.StoreId);
                });

            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    age = table.Column<int>(type: "int", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    classificationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    species = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    heightInches = table.Column<int>(type: "int", nullable: false),
                    weight = table.Column<int>(type: "int", nullable: false),
                    favoriteToy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    favoriteActivity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    storeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.AnimalId);
                    table.ForeignKey(
                        name: "FK_Animal_Store_storeId",
                        column: x => x.storeId,
                        principalTable: "Store",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DayOperationHours",
                columns: table => new
                {
                    DayOperationHoursId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    day = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOperationHours", x => x.DayOperationHoursId);
                    table.ForeignKey(
                        name: "FK_DayOperationHours_Store_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileName",
                columns: table => new
                {
                    FileNameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileName", x => x.FileNameId);
                    table.ForeignKey(
                        name: "FK_FileName_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "AnimalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SimpleTime",
                columns: table => new
                {
                    SimpleTimeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOperationHoursId = table.Column<int>(type: "int", nullable: false),
                    intervalSide = table.Column<int>(type: "int", nullable: false),
                    hours = table.Column<int>(type: "int", nullable: false),
                    minutes = table.Column<int>(type: "int", nullable: false),
                    seconds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleTime", x => x.SimpleTimeId);
                    table.ForeignKey(
                        name: "FK_SimpleTime_DayOperationHours_DayOperationHoursId",
                        column: x => x.DayOperationHoursId,
                        principalTable: "DayOperationHours",
                        principalColumn: "DayOperationHoursId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Store",
                columns: new[] { "StoreId", "address" },
                values: new object[] { 1, "123 abcd st. NW someCity, FL" });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "AnimalId", "age", "classificationName", "description", "favoriteActivity", "favoriteToy", "gender", "heightInches", "name", "species", "storeId", "weight" },
                values: new object[,]
                {
                    { 1, 5, "Dog", "Charlie was raised as a pup here from a litter of 9 other brothers and sisters.", "Going on long walks", "Rope", "m", 30, "Charlie", "Golden Retriever", 1, 80 },
                    { 2, 2, "Dog", "Bailey was found on the side of a road and brought to the center for rehabilitation and has been here almost a year.", "Going to the park", "Squeaky toys", "f", 15, "Bailey", "West Highland White terrier", 1, 40 }
                });

            migrationBuilder.InsertData(
                table: "DayOperationHours",
                columns: new[] { "DayOperationHoursId", "StoreId", "day" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 2, 1, 1 },
                    { 3, 1, 2 },
                    { 4, 1, 3 },
                    { 5, 1, 4 },
                    { 6, 1, 5 },
                    { 7, 1, 6 }
                });

            migrationBuilder.InsertData(
                table: "SimpleTime",
                columns: new[] { "SimpleTimeId", "DayOperationHoursId", "hours", "intervalSide", "minutes", "seconds" },
                values: new object[,]
                {
                    { 1, 1, 8, 0, 0, 0 },
                    { 2, 1, 17, 1, 0, 0 },
                    { 3, 2, 8, 0, 0, 0 },
                    { 4, 2, 17, 1, 0, 0 },
                    { 5, 3, 8, 0, 0, 0 },
                    { 6, 3, 17, 1, 0, 0 },
                    { 7, 4, 8, 0, 0, 0 },
                    { 8, 4, 17, 1, 0, 0 },
                    { 9, 5, 8, 0, 0, 0 },
                    { 10, 5, 17, 1, 0, 0 },
                    { 11, 6, 7, 0, 0, 0 },
                    { 12, 6, 20, 1, 0, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animal_storeId",
                table: "Animal",
                column: "storeId");

            migrationBuilder.CreateIndex(
                name: "IX_DayOperationHours_StoreId",
                table: "DayOperationHours",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_FileName_AnimalId",
                table: "FileName",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_SimpleTime_DayOperationHoursId",
                table: "SimpleTime",
                column: "DayOperationHoursId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileName");

            migrationBuilder.DropTable(
                name: "SimpleTime");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropTable(
                name: "DayOperationHours");

            migrationBuilder.DropTable(
                name: "Store");
        }
    }
}
